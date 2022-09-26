using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevelSelect : MonoBehaviour
{

    public GameObject mainMenuCanvas;
    public GameObject levelSelectCanvas;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(LoadLevelSelectFunction);
        levelSelectCanvas.SetActive(false);
    }

    private void LoadLevelSelectFunction()
    {
        mainMenuCanvas.SetActive(false);
        levelSelectCanvas.SetActive(true);
    }

}

