using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class BackMenu : MonoBehaviour
{

    public XRNode inputSource;
    public GameObject backMenu;
    private bool buttonPressed;
    private bool buttonHeld = false;
    private bool menuShowing = false;

    // Start is called before the first frame update
    void Start()
    {
        backMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.menuButton, out buttonPressed);
        if(buttonPressed == true)
        {
            if(buttonHeld == false)
            {
                if(menuShowing == false)
                {
                    backMenu.SetActive(true);
                    menuShowing = true;
                } else {
                    backMenu.SetActive(false);
                    menuShowing = false;
                }
                buttonHeld = true;
            }
            buttonHeld = true;
        } else {
            buttonHeld = false;
        }
    } 
}
