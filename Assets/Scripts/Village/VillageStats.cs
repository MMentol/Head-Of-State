using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageStats : MonoBehaviour
{
    //Resourcee Trackers
    public float _metal;
    public float _stone;
    public float _wood;
    public float _totalHunger;
    public float _totalThirst;
    public float _totalHeat;
    public float _happiness;

    public HumanStats[] experiments;
    

    private void Update()
    {
        experiments = FindObjectsByType<HumanStats>(FindObjectsSortMode.None);
        

    }

    public void updateHunger(HumanStats[] humans)
    {
        foreach(HumanStats h in humans)
        {
            _totalHunger += h._hunger;
        }
    }
    public void updateWater(Human[] humans)
    {

    }
    public void updateTemp(Human[] humans)
    {

    }
    public void updateHappiness(Human[] humans)
    {

    }
    public void updateStone(Human[] humans)
    {

    }
    public void updateMetal(Human[] humans)
    {

    }
    public void updateWood(Human[] humans)
    {

    }

}
