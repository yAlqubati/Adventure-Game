using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CheckPoint[] checkPoints;

    public GameObject firstFlag;
    public GameObject secondFlag;

    private bool levelIsCompleting = false; // To prevent the coroutine from being called multiple times

    void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep the GameManager across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        checkPoints = FindObjectsOfType<CheckPoint>();
    }

    void Update()
    {
        if (!levelIsCompleting) // Check if the level is not already completing
        {
            isLevelComplete();
        }
    }

    void isLevelComplete()
    {
        if (firstFlag.GetComponent<EndFlag>().isReached && secondFlag.GetComponent<EndFlag>().isReached)
        {
            if (!levelIsCompleting) // Prevent multiple calls to the coroutine
            {
                StartCoroutine(CompleteLevel());
                levelIsCompleting = true; // Mark that level completion is in process
            }
        }
    }

    IEnumerator CompleteLevel()
    {

                AudioManager.instance.PlayAudio(5);

        // Wait for 3 seconds before playing the winning sound and loading the next level
        yield return new WaitForSeconds(3);
        
        // Play the winning sound

        // Log level completion and load the next level from MySceneManager
        Debug.Log("Level Complete");
        MySceneManager.instance.LoadNextScene();
    }

    public void DeactivateCheckPoints()
    {
        foreach (CheckPoint cp in checkPoints)
        {
            cp.sr.sprite = cp.checkPointOff;
        }
    }

    public void RespawnPlayer(string playerName)
    {
        Debug.Log("Respawning player: " + playerName);
        StartCoroutine(Respawn(playerName));
    }

    public IEnumerator Respawn(string playerName)
    {
        yield return new WaitForSeconds(0.5f);
        // Unfreeze the player
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
