using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResidenceBuilding : MonoBehaviour
{
    public string _id;
    public int capacity = 5;
    public GameObject HumanPrefab;

    

    public void resetTemp(HumanStats human)
    {
        human._heat = 100;
    }

    public void sleep(HumanStats human)
    {
        human._energy = 100;
    }

    public void createNewHuman()
    {
        Instantiate(HumanPrefab, transform.position, Quaternion.identity);
    }

    

}
