using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadMainMenuScene : MonoBehaviour
{

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(LoadCurrentLevelFunction);
    }

    private void LoadCurrentLevelFunction()
    {
        GameData.gameCompleted = false;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

}
