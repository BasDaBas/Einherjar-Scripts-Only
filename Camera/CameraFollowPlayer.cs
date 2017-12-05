using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{

    public Transform player;
    public float camerSmoothingSpeed;
    Camera characterCam;

    // Use this for initialization
    void Start()
    {
        characterCam = GetComponent<Camera>();
    }

    // LateUpdate is called after Update each frame
    void Update()
    {
        characterCam.orthographicSize = (Screen.height / 100f) / 1.75f;

        if (player)
        {
            transform.position = Vector3.Lerp(transform.position, player.position, camerSmoothingSpeed) + new Vector3(0, 0, -10);

        }
    }
}
