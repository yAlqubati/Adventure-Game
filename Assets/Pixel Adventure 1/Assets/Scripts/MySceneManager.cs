using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager instance;

    void Awake()
    {
        // make it a singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadNextScene()
    {
        // get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // load the next scene
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
