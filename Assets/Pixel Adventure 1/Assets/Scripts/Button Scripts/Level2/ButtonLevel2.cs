using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLevel2 : MonoBehaviour
{

    public GameObject goldBar;
    public bool isPressed = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
            if (col.gameObject.tag == "Player")
            {
                Debug.Log("Player has entered the button");
                if (!isPressed)
                {
                    // move the gold bar to the right
                    goldBar.transform.position = new Vector3(goldBar.transform.position.x + 8f, goldBar.transform.position.y, goldBar.transform.position.z);
                    isPressed = true;
                    // cause we'll need to move the gold bar that this button is connected to, the button will float in the air,we'll destroy it
                    Destroy(this.gameObject);
                }
            }
    
    }

}
