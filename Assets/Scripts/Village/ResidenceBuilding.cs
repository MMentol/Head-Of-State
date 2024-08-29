using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResidenceBuilding
{
    public string _id;
    public int capacity;

    public ResidenceBuilding(string id)
    {
        this._id = id;
        capacity = 5;
    }

    public void resetTemp(HumanStats human)
    {
        human._heat = 100;
    }

    public void sleep(HumanStats human)
    {

    }

    public void createNewHuman()
    {

    }

    

}
