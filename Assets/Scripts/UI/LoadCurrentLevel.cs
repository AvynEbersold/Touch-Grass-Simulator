using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadCurrentLevel : MonoBehaviour
{

    // Change this later once you have enough levels (and saving implemented so that you can get data and determine the current level)

    //string currentSceneName = SceneManager.GetActiveScene().name;
    
    // If true, scene will be named Level<number>. If false, scene will be named Stage<number>
    bool isLevel = true;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(LoadCurrentLevelFunction);
    }

    private void LoadCurrentLevelFunction()
    {
        //string currentLevelNumber = currentSceneName.Substring(currentSceneName.Length - 1);
        string currentLevelNumber = "1";
        if (isLevel == true)
        {
            SceneManager.LoadScene("Level" + currentLevelNumber.ToString(), LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("Stage" + currentLevelNumber.ToString(), LoadSceneMode.Single);

        }
    }

}

