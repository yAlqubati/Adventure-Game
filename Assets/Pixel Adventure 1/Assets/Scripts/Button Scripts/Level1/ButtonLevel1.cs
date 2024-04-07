using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLevel1 : MonoBehaviour
{
    public GameObject block;
    public GameObject goldCage;
    public bool isPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D col)
{
    if (col.gameObject.tag == "Player")
    {
        block.SetActive(true);
        if(!isPressed) goldCage.transform.position = new Vector3(goldCage.transform.position.x, goldCage.transform.position.y - 1.5f, goldCage.transform.position.z);
        isPressed = true;
        
    }
}

}
