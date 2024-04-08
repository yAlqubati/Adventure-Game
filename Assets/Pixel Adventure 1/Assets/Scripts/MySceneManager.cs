using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager instance;

    void Awake()
    {
        instance = this;
    }

    public void LoadNextScene()
    {
        Debug.Log("current scene index: " + SceneManager.GetActiveScene().buildIndex);
        // get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // load the next scene
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
