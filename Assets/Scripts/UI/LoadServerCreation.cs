using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadServerCreation : MonoBehaviour
{

    public GameObject serverCreationCanvas;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(LoadServerCreationFunction);
        serverCreationCanvas.SetActive(false);
    }

    private void LoadServerCreationFunction()
    {
        serverCreationCanvas.SetActive(true);
    }

}

