using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	[SerializeField] SceneField[] scenes;
	[SerializeField] VoidEvent onSceneFinished;

	int currentScene = -1;

	private void Awake()
	{
		LoadNextScene();
	}

	private void OnEnable()
	{
		onSceneFinished.OnEventRaised += LoadNextScene;
	}

	private void OnDisable()
	{
		onSceneFinished.OnEventRaised -= LoadNextScene;
	}

	void LoadNextScene()
	{
		int previousScene = currentScene;
		currentScene++;
		if (currentScene < scenes.Length)
		{
			if (previousScene >= 0)
            {
				StartCoroutine(UnloadScene(previousScene));
			}
			else
				SceneManager.LoadScene(scenes[currentScene].sceneName, LoadSceneMode.Additive);

		}
	}

	IEnumerator UnloadScene(int scene)
    {
		AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(scenes[scene].sceneName);
        while (!unloadOperation.isDone)
        {
			yield return null;
        }
		SceneManager.LoadScene(scenes[currentScene].sceneName, LoadSceneMode.Additive);
	}

    private void UnloadOperation_completed(AsyncOperation obj)
    {
		SceneManager.LoadScene(scenes[currentScene].sceneName, LoadSceneMode.Additive);
	}
}
