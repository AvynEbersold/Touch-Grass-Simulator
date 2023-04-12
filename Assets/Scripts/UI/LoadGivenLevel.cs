using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGivenLevel : MonoBehaviour
{

    public bool isLevel = true;
    public int levelNumber = 1;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(LoadGivenLevelFunction);
    }

    private void LoadGivenLevelFunction()
    {
        if (isLevel == true)
        {
            SceneManager.LoadScene("Level" + levelNumber.ToString(), LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("Stage" + levelNumber.ToString(), LoadSceneMode.Single);

        }
    }

}

