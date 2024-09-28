using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Camera currCam;
    float ScrollSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        currCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(currCam.orthographic) {
            float maxOrtho = currCam.orthographicSize;
            maxOrtho -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
            currCam.orthographicSize = Mathf.Clamp(maxOrtho, 5, 40);
            Debug.Log(currCam.orthographicSize);
        }
        else {
            currCam.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
            Debug.Log(currCam.fieldOfView);
        }
    }
}
