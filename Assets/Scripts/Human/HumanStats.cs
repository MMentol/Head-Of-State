using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts;

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
    public float hasBucket = 0;
    public float breedCooldown = 0;
    public float canBreed = 1;

    public Animator anim;

    public Inventory inventory;

    private void Awake()
    {
        inventory = gameObject.GetComponent<Inventory>();
    }

    private void FixedUpdate()
    {
        getHungry();
        getThirsty();
        getHot();
        getSleepy(0.1f);
        updateHappiness();
        updatePickaxe();
        updateBucket();
        RefractoryPeriod();
        if (this._age > 1) {
            inBabyPhase = 0;
            anim.SetBool("isBaby", false);
        } else {
            anim.SetBool("isBaby", true);
        }
    }

    public void getHungry()
    {
        this._hunger += Time.fixedDeltaTime * 0.5f;
        if (this._hunger >= 100) this._hunger = 100;
    }
    public void getThirsty()
    {
        this._thirst += Time.fixedDeltaTime * 0.5f ;
        if (this._thirst >= 100) this._thirst = 100;
    }
    public void getHot()
    {
        this._heat -= Time.fixedDeltaTime * 0.1f ;
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

    public void RefractoryPeriod()
    {
        breedCooldown -= Time.fixedDeltaTime;
        if (breedCooldown == 0) canBreed = 1;
        else canBreed = 0;
    }

    public void updateHappiness()
    {
        this._happiness = ((100f - this._hunger) + (100f - this._thirst) + (this._heat) + (this._energy))/4f;
    }

    public void updatePickaxe()
    {
        if(inventory.items == null)
        {
            anim.SetBool("hasPickaxe", false);
            return;
        }

        foreach(var item in this.inventory.items)
        {
            if(item.ItemName == "Pickaxe")
            {
                anim.SetBool("hasPickaxe", true);
                hasPickAxe = 1;
                return;
            }
        }
        
        hasPickAxe = 0;
        return;
    }
    
    public void updateBucket()
    {
        if (inventory.items == null)
        {
            anim.SetBool("hasWater", false);
            return;
        }
        foreach (var item in this.inventory.items)
        {
            if(item.ItemName == "Bucket")
            {
                anim.SetBool("hasWater", true);
                hasBucket = 1;
                return;
            }
        }
        
        hasBucket = 0;
        return;
    }

    








}
