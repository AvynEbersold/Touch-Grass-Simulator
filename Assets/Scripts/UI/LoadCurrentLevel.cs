using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadCurrentLevel : MonoBehaviour
{

    // Change this later once you have enough levels (and saving implemented so that you can get data and determine the current level)
    public int currentLevelNumber = 1;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(LoadCurrentLevelFunction);
    }

    private void LoadCurrentLevelFunction()
    {
        SceneManager.LoadScene("Level" + currentLevelNumber.ToString(), LoadSceneMode.Single);
    }

}

