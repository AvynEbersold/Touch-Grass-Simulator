using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinuousMovement : MonoBehaviour
{

    public XRNode joystickInputSource;
    public XRNode buttonInputSource;
    public GameObject cameraGameObject;
    public float speed = 1;
    public float jumpPower = 8;
    public float superJumpPowerMultiplier = 2;
    public float gravity = -9.81f;
    public LayerMask groundLayer;
    public LayerMask superJumpLayer;
    public float additionalHeight = 0.2f;
    public GameObject audioManager;
    public AudioClip jumpSound;
    public bool jumpSoundEnabled;
    public AudioClip superJumpSound;

    private XROrigin rig;
    private Vector2 inputAxis;
    private bool inputButtonPressed;
    private CharacterController character;
    private float fallingSpeed = 0;
    private AudioSource audioSourceComponent;

    bool CheckIfOnGround()
    {
        Vector3 rayStartPoint = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.02f;
        bool hasHit = Physics.SphereCast(rayStartPoint, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;
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
        InputDevice device = InputDevices.GetDeviceAtXRNode(joystickInputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
        InputDevice buttonDevice = InputDevices.GetDeviceAtXRNode(buttonInputSource);
        buttonDevice.TryGetFeatureValue(CommonUsages.primaryButton, out inputButtonPressed);
    }

    private void FixedUpdate()
    {
        ColliderFollowHeadset();

        Quaternion headYaw = Quaternion.Euler(0, cameraGameObject.transform.eulerAngles.y, 0);

        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);
        character.Move(direction * Time.fixedDeltaTime * speed);

        // Apply gravity
        bool onGround = CheckIfOnGround();
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
