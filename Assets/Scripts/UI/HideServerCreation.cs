using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HideServerCreation : MonoBehaviour
{

    public GameObject serverCreationCanvas;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(HideServerCreationFunction);
    }

    private void HideServerCreationFunction()
    {
        serverCreationCanvas.SetActive(false);
    }

}

