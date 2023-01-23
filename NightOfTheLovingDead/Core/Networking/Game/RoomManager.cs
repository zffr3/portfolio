using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviour
{
	public static RoomManager Instance;

	[SerializeField]
	private GameObject _playerRootPrefab;

	void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

    private void OnEnable()
    {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

    private void OnDisable()
    {
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
	{
        if (scene.buildIndex != 0)
        {
			Instantiate(this._playerRootPrefab, Vector3.zero, Quaternion.identity);
		}
	}
}
