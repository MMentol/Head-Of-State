using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureChooser : MonoBehaviour
{
    [SerializeField] public GameObject mouseIndicator;
    [SerializeField] public GameObject[] placeableObjects;
    [SerializeField] public Vector3 currentPos;

    void Update() 
    {
        if(currentPos == null)
        {
            currentPos = Input.mousePosition;
        }

        if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            DestroyCurrent();
            Debug.Log($"Select: {placeableObjects[0].name}");
            mouseIndicator = Instantiate(placeableObjects[0], currentPos, Quaternion.identity, gameObject.transform);
            mouseIndicator.name = $"Temp {placeableObjects[0].name}";
        }
        if(Input.GetKeyUp(KeyCode.Alpha2))
        {
            DestroyCurrent();
            Debug.Log($"Select: {placeableObjects[1].name}");
            mouseIndicator = Instantiate(placeableObjects[1], currentPos, Quaternion.identity, gameObject.transform);
            mouseIndicator.name = $"Temp {placeableObjects[1].name}";
        }


        if(Input.GetKeyUp(KeyCode.X))
        {
            DestroyCurrent();
            Debug.Log("Cancel Select");
        }
    }

    void DestroyCurrent()
    {
        if (mouseIndicator != null)
        {
            Destroy(mouseIndicator);
            mouseIndicator = null;
        }
    }
}
