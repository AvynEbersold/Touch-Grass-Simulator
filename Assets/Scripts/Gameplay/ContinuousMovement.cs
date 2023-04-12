using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;

// This script handles the movement, using the joystick, of the player, as well as the following:
// - Gravity
// - Ground interaction (eg. not falling through the ground)
// - Jumping
// - "Super" jumping
// - Turning (using joystick)

public class ContinuousMovement : MonoBehaviour
{

    public XRNode movementJoystickInputSource;
    public XRNode jumpButtonInputSource;
    public GameObject cameraGameObject;
    public float speed = 1;
    public float jumpPower = 8;
    public float superJumpPowerMultiplier = 2;
    public float gravity = -9.81f;
    public LayerMask groundLayer;
    public LayerMask superJumpLayer;
    public LayerMask movingObjectLayer;
    public float additionalHeight = 0.2f;
    public GameObject audioManager;
    public AudioClip jumpSound;
    public bool jumpSoundEnabled;
    public AudioClip superJumpSound;

    private XROrigin rig;
    private Vector2 movementInputAxis;
    private bool inputButtonPressed;
    private CharacterController character;
    private float fallingSpeed = 0;
    private AudioSource audioSourceComponent;

    bool CheckIfOnGround()
    {
        Vector3 rayStartPoint = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.02f;
        bool hasHitGround = Physics.SphereCast(rayStartPoint, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        bool hasHitMovingObject = Physics.SphereCast(rayStartPoint, character.radius, Vector3.down, out RaycastHit hitInfo2, rayLength, groundLayer);
        // returns true if the player is on top or something with the layer Ground OR MovingObject
        if (hasHitGround || hasHitMovingObject)
        {
            return true;
        } else { return false; }
    }

    bool CheckIfOnMovingObject()
    {
        Vector3 rayStartPoint = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.5f;
        bool hasHit = Physics.SphereCast(rayStartPoint, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, movingObjectLayer);
        if (hasHit)
        {
            Debug.Log("On moving object!");
            transform.parent = hitInfo.collider.gameObject.transform;
            return true;
        }
        else
        {
            //Debug.Log("NOT on moving object!");
            transform.parent = null;
            return false;
        }
    }

    bool CheckIfOnSuperJump()
    {
        Vector3 rayStartPoint = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.02f;
        bool hasHit = Physics.SphereCast(rayStartPoint, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, superJumpLayer);
        return hasHit;
    }

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XROrigin>();
        audioSourceComponent = audioManager.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(movementJoystickInputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out movementInputAxis);

        InputDevice buttonDevice = InputDevices.GetDeviceAtXRNode(jumpButtonInputSource);
        buttonDevice.TryGetFeatureValue(CommonUsages.primaryButton, out inputButtonPressed);
    }

    private void FixedUpdate()
    {
        ColliderFollowHeadset();

        Quaternion headYaw = Quaternion.Euler(0, cameraGameObject.transform.eulerAngles.y, 0);

        Vector3 direction = headYaw * new Vector3(movementInputAxis.x, 0, movementInputAxis.y);
        character.Move(direction * Time.fixedDeltaTime * speed);

        // Apply gravity
        bool onMovingObjectCheck = CheckIfOnMovingObject();
        bool onGroundCheck = CheckIfOnGround();
        bool onGround = onGroundCheck || onMovingObjectCheck;
        if (onGround == true)
        {
            fallingSpeed = 0;
            if (inputButtonPressed)
            {
                if (CheckIfOnSuperJump())
                {
                    fallingSpeed = jumpPower * superJumpPowerMultiplier;
                    audioSourceComponent.PlayOneShot(superJumpSound);
                } else
                {
                    fallingSpeed = jumpPower;
                    if (jumpSoundEnabled)
                    {
                        audioSourceComponent.PlayOneShot(jumpSound);
                    }
                }
            }
        } else
        {
            fallingSpeed += gravity * Time.fixedDeltaTime;
        }

        CheckIfOnMovingObject();

        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

    void ColliderFollowHeadset()
    {
        character.height = rig.CameraInOriginSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.Camera.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height/2 + character.skinWidth, capsuleCenter.z);
    }

    public void ResetMomentum()
    {
        fallingSpeed = 0;
    }
}
