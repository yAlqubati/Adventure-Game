using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script will update the checkpoint for the player and turn off all other checkpoints
public class CheckPoint : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite checkPointOn;
    public Sprite checkPointOff;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Checkpoint Reached");
            collision.GetComponent<PlayerController>().lastCheckpoint = this.transform;
            GameManager.instance.DeactivateCheckPoints();
            sr.sprite = checkPointOn;
            // you need to set all other checkpoints to off through the game manager
        }
    }
}
