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
			SceneManager.LoadScene(scenes[currentScene].sceneName, LoadSceneMode.Additive);
			if (previousScene >= 0)
				SceneManager.UnloadSceneAsync(scenes[previousScene].sceneName);
		}
	}
}
