using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompletionChecker : MonoBehaviour
{
	private void CheckForSceneExistenceAndLoad(string sceneName, bool isLevel)
    {
		int buildIndex = SceneUtility.GetBuildIndexByScenePath(sceneName);
		Debug.Log("Attempted to load:" + sceneName);
		if (buildIndex > -1)
		{
			SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
		}
		else
		{
            if (isLevel)
            {
				CheckForSceneExistenceAndLoad("Stage1", false);
            } else
            {
				GameData.gameCompleted = true;
				SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
			}
		}
	}
	private void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Grass")
        {
			string currentSceneName = SceneManager.GetActiveScene().name;
			Debug.Log(currentSceneName);
			int currentSceneNameLength = currentSceneName.Length;
			Debug.Log(currentSceneNameLength);
			string currentSceneNumber = currentSceneName.Substring(currentSceneNameLength - 1);
			Debug.Log(currentSceneNumber);
			string sceneType = currentSceneName.Substring(0, 5);
			Debug.Log(sceneType);
			bool sceneIsLevel = sceneType == "Level" ? true : false;
			Debug.Log(sceneIsLevel);
			int nextSceneNumber = int.Parse(currentSceneNumber) + 1;
			Debug.Log(nextSceneNumber);
			string newSceneName = (sceneIsLevel ? "Level" : "Stage") + nextSceneNumber.ToString();
			Debug.Log(newSceneName);  

			CheckForSceneExistenceAndLoad(newSceneName, sceneIsLevel);
        }
	}
}