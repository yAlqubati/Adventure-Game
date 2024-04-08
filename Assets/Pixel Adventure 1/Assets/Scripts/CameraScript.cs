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
        public float minZoom = 5f;
        public float maxZoom = 10f;
        public float zoomSpeed = 5f;

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
            }

            else if(player1 != null || player2 != null)
            {
                // make the follow to null  
                vcam.Follow = null;
                vcam.LookAt = null;

                // and set the lens orthographic 10
                vcam.m_Lens.OrthographicSize = 10f;
            }
        }

        // Set the second player and start following both
        public void SetSecondPlayer(GameObject player)
        {
            player2 = player;
            isPlayer2Active = true;
        }


    }