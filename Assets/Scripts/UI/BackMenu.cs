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
        Debug.Log(buttonPressed);
        //if(buttonPressed == true)
        //{
        //if(buttonHeld == false)
        //{
        //Debug.Log("Menu should switch states now.");
        //if(menuShowing = false)
        //{
        //backMenu.SetActive(true);
        //menuShowing = true;
        //} else
        //{
        //backMenu.SetActive(false);
        //menuShowing = false;
        //}
        //buttonHeld = true;
        //}
        //buttonHeld = true;
        //} else
        //{
        //buttonHeld = false;
        //}
        if (buttonPressed)
        {
            Debug.Log("Menu should switch states now.");
            if (menuShowing = false)
            {
                backMenu.SetActive(true);
                menuShowing = true;
            }
            else
            {
                backMenu.SetActive(false);
                menuShowing = false;
            }
        }
    } 
}
