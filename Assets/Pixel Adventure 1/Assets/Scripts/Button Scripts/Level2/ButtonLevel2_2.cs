using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLevel2_2 : MonoBehaviour
{
    public GameObject goldBar;
    
    public bool isPressed = false;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Player has entered the button 2");

            // Check if the button is not pressed, then move the gold bar up
            if (!isPressed)
            {
                goldBar.transform.position = new Vector3(goldBar.transform.position.x, goldBar.transform.position.y + 4f, goldBar.transform.position.z);
                isPressed = true;
            }
            // If the button is pressed, move the gold bar down
            else
            {
                goldBar.transform.position = new Vector3(goldBar.transform.position.x, goldBar.transform.position.y - 4f, goldBar.transform.position.z);
                isPressed = false;
            }
        }
    }
}
