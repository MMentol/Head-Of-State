using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanStats : MonoBehaviour
{
    public float _age = 0 ;
    public float _hunger = 0;
    public float _thirst = 0;
    public float _heat = 100;
    public float _energy = 100;
    public float _happiness;
    public float partnerExists = 0;
    public float insideHouse = 0;
    public float inBabyPhase = 1;
    public float hasPickAxe = 0;
    public float hasAxe = 0;

    private void FixedUpdate()
    {
        getHungry();
        getThirsty();
        getHot();
        getSleepy(1);
        updateHappiness();
        if (this._age > 1) inBabyPhase = 0;
    }

    public void getHungry()
    {
        this._hunger += Time.fixedDeltaTime * 2f;
        if (this._hunger >= 100) this._hunger = 100;
    }
    public void getThirsty()
    {
        this._thirst += Time.fixedDeltaTime * 2f ;
        if (this._thirst >= 100) this._thirst = 100;
    }
    public void getHot()
    {
        this._heat -= Time.fixedDeltaTime * 2f ;
        if (this._heat <= 0) this._heat = 0;
    }
    public void getSleepy(float energyUsed)
    {
        this._energy -= Time.fixedDeltaTime * energyUsed;
        if (this._energy <= 0) this._energy = 0;
    }

    public void getOlder()
    {
        _age++;
    }

    public void updateHappiness()
    {
        this._happiness = ((100f - this._hunger) + (100f - this._thirst) + (this._heat) + (this._energy))/4f;
    }

    public void updatePickaxe()
    {
        if (hasPickAxe == 0) hasPickAxe = 1;
        else hasPickAxe = 0;
    }

    public void updateAxe()
    {
        if (hasAxe == 0) hasAxe = 1;
        else hasAxe = 0;
    }








}
