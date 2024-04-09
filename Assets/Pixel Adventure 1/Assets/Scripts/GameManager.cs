using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CheckPoint[] checkPoints;

    public GameObject firstFlag;
    public GameObject secondFlag;

    // Flag to prevent level completion from being triggered multiple times
    private bool isLevelCompleted = false;

    void Awake()
    {
        instance = this;
    }


    void Start()
    {
        // Subscribe to the scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset level completion flag when a new level starts
        isLevelCompleted = false;

        // Find all checkpoints in the scene
        checkPoints = FindObjectsOfType<CheckPoint>();
    }

    void Update()
    {
        // Check if both flags are reached and the level is not already completed
        if (!isLevelCompleted && firstFlag != null && secondFlag != null &&
            firstFlag.GetComponent<EndFlag>().isReached && secondFlag.GetComponent<EndFlag>().isReached)
        {
            Debug.Log("Both flags reached");
            isLevelCompleted = true;
            StartCoroutine(CompleteLevel());
        }
    }

    IEnumerator CompleteLevel()
    {
        AudioManager.instance.PlayAudio(5);

        // Wait for 3 seconds before playing the winning sound and loading the next level
        yield return new WaitForSeconds(3);

        // Log level completion and load the next level from MySceneManager
        Debug.Log("Level Complete");
        MySceneManager.instance.LoadNextScene();
    }

    // Deactivate all checkpoints in the scene
    public void DeactivateCheckPoints()
    {
        foreach (CheckPoint cp in checkPoints)
        {
            cp.sr.sprite = cp.checkPointOff;
        }
    }

    // Respawn the player at the last checkpoint
    public void RespawnPlayer(string playerName)
    {
        Debug.Log("Respawning player: " + playerName);
        StartCoroutine(Respawn(playerName));
    }

    IEnumerator Respawn(string playerName)
    {
        yield return new WaitForSeconds(0.5f);
        // Unfreeze the player and respawn at the last checkpoint
        GameObject player = GameObject.Find(playerName);
        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.UnFreezePlayer();
                player.transform.position = playerController.lastCheckpoint.position;
            }

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Respawn();
            }
        }
    }
}
