
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoinController : MonoBehaviour
{
    public GameObject playerController;
    public int playerNumber = 0;
    public int maxPlayers = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current != null && Gamepad.current.leftStick.ReadValue().x != 0 && playerNumber < maxPlayers)
        {
            Debug.Log("Player 1 Joined");
            Instantiate(playerController, transform.position, transform.rotation);
            playerNumber++;
        }
    }
}
