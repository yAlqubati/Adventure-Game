using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLevel1_2 : MonoBehaviour
{
    // this thing for the gold cage
    public GameObject goldCage;
    public Transform startTransform;
    public bool isPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        // disable the gravity of the gold cage
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
            if(!isPressed)goldCage.transform.position = new Vector3(goldCage.transform.position.x, goldCage.transform.position.y + 1.5f, goldCage.transform.position.z);
            isPressed = true;
        }
    }
}
