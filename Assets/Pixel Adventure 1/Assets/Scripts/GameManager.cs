using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CheckPoint[] checkPoints;
    
    public GameObject firstFlag;
    public GameObject secondFlag;
    void Awake()
    {
        // singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        checkPoints = FindObjectsOfType<CheckPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        isLevelComplete();
    }

    void isLevelComplete()
    {
        if (firstFlag.GetComponent<EndFlag>().isReached && secondFlag.GetComponent<EndFlag>().isReached)
        {
            // go to the next level
            Debug.Log("Level Complete");
            // load the next level from the MySceneManager
            MySceneManager.instance.LoadNextScene();
        }
    }

    public void DeactivateCheckPoints()
    {
        foreach(CheckPoint cp in checkPoints)
        {
            cp.sr.sprite = cp.checkPointOff;
        }
    }

    // we'll use this method whenever we want to respawn the player
    // we'll pass the player name so we can know which player to respawn
    // still need to be tested
    public void RespawnPlayer(string playerName)
    {
        Debug.Log("Respawning player:"+playerName);
        StartCoroutine(Respawn(playerName));
    }

    public IEnumerator Respawn(string playerName)
    {
        yield return new WaitForSeconds(0.5f);
        // unfreeze the player
        GameObject.Find(playerName).GetComponent<PlayerController>().UnFreezePlayer();
        GameObject.Find(playerName).transform.position = GameObject.Find(playerName).GetComponent<PlayerController>().lastCheckpoint.position;
        GameObject.Find(playerName).GetComponent<PlayerHealth>().Respawn();
    }
}
