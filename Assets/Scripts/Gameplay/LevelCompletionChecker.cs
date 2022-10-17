using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompletionChecker : MonoBehaviour
{
	private void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Grass")
        {
			string currentSceneName = SceneManager.GetActiveScene().name;
			string currentSceneNumber = currentSceneName.Substring(currentSceneName.Length - 1);
			int nextSceneNumber = int.Parse(currentSceneNumber) + 1;
			int buildIndex = SceneUtility.GetBuildIndexByScenePath("Level" + nextSceneNumber.ToString());
			if(buildIndex > -1)
            {
				SceneManager.LoadScene("Level" + nextSceneNumber.ToString(), LoadSceneMode.Single);
			} else
            {
				SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
			}
        }
	}
}