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
    public float _totalHappiness;

    public HumanStats[] experiments;
    

    private void Update()
    {
        experiments = FindObjectsByType<HumanStats>(FindObjectsSortMode.None);
        

    }

    public void updateHunger(HumanStats[] humans)
    {
        float _currentHunger = 0;
        foreach(HumanStats h in humans)
        {
            _currentHunger += h._hunger;
        }

        if (_currentHunger != _totalHunger) _totalHunger = _currentHunger;
    }
    public void updateWater(HumanStats[] humans)
    {
        float _currentThirst = 0;
        foreach (HumanStats h in humans)
        {
            _currentThirst += h._thirst;
        }

        if (_currentThirst != _totalThirst) _totalThirst = _currentThirst;
    }
    public void updateTemp(HumanStats[] humans)
    {
        float _currentTemp = 0;
        foreach (HumanStats h in humans)
        {
            _currentTemp += h._heat;
        }

        if (_currentTemp != _totalHeat) _totalHeat = _currentTemp;
    }
    public void updateHappiness(HumanStats[] humans)
    {
        float _currentHappiness = 0;
        foreach (HumanStats h in humans)
        {
            _currentHappiness += h._happiness;
        }

        if (_currentHappiness != _totalHappiness) _totalHappiness = _currentHappiness;
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
