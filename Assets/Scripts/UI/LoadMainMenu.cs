using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadMainMenu : MonoBehaviour
{

    public GameObject mainMenuCanvas;
    public GameObject levelSelectCanvas;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(LoadLevelSelectFunction);
    }

    private void LoadLevelSelectFunction()
    {
        mainMenuCanvas.SetActive(true);
        levelSelectCanvas.SetActive(false);
    }

}

