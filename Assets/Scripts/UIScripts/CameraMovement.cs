using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Camera currCam;
    float ScrollSpeed = 10f;
    float moveSpeed = 20f;
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
            // Debug.Log(currCam.orthographicSize);
        }
        else {
            currCam.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
            Debug.Log(currCam.fieldOfView);
        }
        float x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float y = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float xPos = transform.localPosition.x + x;
        float yPos = transform.localPosition.y + y;
        float xBound = Mathf.Clamp(xPos, -140, 50);
        float yBound = Mathf.Clamp(yPos, 35, 115);
        gameObject.transform.position = new Vector3(xBound, yBound, transform.position.z);
    }
}
