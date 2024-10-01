using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Camera currCam;
    float ScrollSpeed = 10f;
    float RectangleScrollSpeed = 2f;
    float moveSpeed = 2.4f;
    float maxRect = 40f;
    Transform spriteTransform = null;

    void Awake()
    {
        currCam = Camera.main;
        spriteTransform = this.gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currCam.orthographic) {
            zoomRect();
            zoomCam();
        }
        else {
            currCam.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
            Debug.Log(currCam.fieldOfView);
        }
        float x = Input.GetAxis("Horizontal") * moveSpeed;
        float y = Input.GetAxis("Vertical") * moveSpeed;
        float xPos = transform.localPosition.x + x;
        float yPos = transform.localPosition.y + y;
        float xBound = Mathf.Clamp(xPos, -140, 50);
        float yBound = Mathf.Clamp(yPos, 35, 115);
        gameObject.transform.position = new Vector3(xBound, yBound, transform.position.z);
    }

    void zoomCam(){
        float maxOrtho = currCam.orthographicSize;
        maxOrtho -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
        currCam.orthographicSize = Mathf.Clamp(maxOrtho, 5, 40);
    }
    void zoomRect(){
        float maxRect = currCam.orthographicSize;
        maxRect -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
        float clampedRect = Mathf.Clamp(maxRect, 5, 40);
        spriteTransform.localScale = new Vector3(clampedRect, clampedRect, 1);
    }
}
