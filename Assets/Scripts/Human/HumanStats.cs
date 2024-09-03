using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanStats : MonoBehaviour
{
    public float _age;
    public float _hunger;
    public float _thirst;
    public float _heat;
    public float _energy;
    public float _happiness;

    public void getHungry()
    {
        _hunger--;
    }
    public void getThirsty()
    {
        _thirst--;
    }
    public void getHot()
    {
        _heat--;
    }
    public void getSleepy(float energyUsed)
    {
        _energy -= energyUsed;
    }

    public void getOlder()
    {
        _age++;
    }






}
