using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraScript : MonoBehaviour
{
    public static CameraScript instance;
    public CinemachineVirtualCamera vcam;
    public static GameObject player1;
    public static GameObject player2;
    public bool isPlayer2Active = false;
    public float minZoom = 8f;
    public float maxZoom = 15f; // Increased maxZoom
    public float zoomSpeed = 2f; // Decreased zoomSpeed for smoother movement

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        if (player1 != null && player2 != null)
        {
            // Calculate the distance between the two players
            float distance = Vector2.Distance(player1.transform.position, player2.transform.position);

            // Calculate the midpoint between the two players
            Vector3 midpoint = (player1.transform.position + player2.transform.position) / 2f;

            // Set the camera's position to the midpoint
            transform.position = new Vector3(midpoint.x, midpoint.y, transform.position.z);

            // Calculate the zoom level based on the distance between the two players
            float zoom = Mathf.Clamp(distance, minZoom, maxZoom);

            // Smoothly zoom in or out based on the zoom level
            vcam.m_Lens.OrthographicSize = Mathf.Lerp(vcam.m_Lens.OrthographicSize, zoom, Time.deltaTime * zoomSpeed);

            // Set maximum orthographic size to 12 if both players are active
            maxZoom = 11.5f;
        }
        else
        {
            // Reset the camera if only one player is active
            vcam.Follow = null;
            vcam.LookAt = null;
            vcam.m_Lens.OrthographicSize = 10f; // Or use any other desired minimum zoom value

            // Reset maximum orthographic size to default value if only one player is active
            maxZoom = 15f;
        }
    }

    // Set the second player and start following both
    public void SetSecondPlayer(GameObject player)
    {
        player2 = player;
        isPlayer2Active = true;
    }
}
