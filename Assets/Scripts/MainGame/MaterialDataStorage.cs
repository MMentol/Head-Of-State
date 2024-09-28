using UnityEngine;
using TMPro;
using GridMap.Structures.Storage;
using System;
using System.Linq;
using System.Collections.Generic;
public class MaterialDataStorage : MonoBehaviour
{
    public int Wood = 0;
    public int Stone = 0;
    public int Metal = 0;
    public int Food = 0;
    public int Water = 0;
    public int Population = 0;
    public int WoodCapacity = 0;
    public int StoneCapacity = 0;
    public int MetalCapacity = 0;
    public int FoodCapacity = 0;
    public int WaterCapacity = 0;
    public int MaxPopulation = 0;

    public TMP_Text WoodTxt;
    public TMP_Text StoneTxt;
    public TMP_Text MetalTxt;
    public TMP_Text FoodTxt;
    public TMP_Text WaterTxt;
    public TMP_Text PopulationTxt;

    private void Awake()
    {
        TallyMaterials();
        UpdateText();
    }

    private void OnValidate()
    {
        UpdateText();
    }

    public void TallyMaterials()
    {
        Clear();
        TallyWood();
        TallyStone();
        TallyMetal();
        TallyFood();
        TallyWater();
        Census();
        UpdateText();
    }

    public void TallyWood()
    {
        WoodStorage[] woodStorages = GameObject.FindObjectsOfType<WoodStorage>();
        foreach (WoodStorage storage in woodStorages)
        {
            this.WoodCapacity += storage.Capacity;
            this.Wood += storage.Count;
        }
    }

    public void TallyStone()
    {
        StoneStorage[] stoneStorages = GameObject.FindObjectsOfType<StoneStorage>();
        foreach (StoneStorage storage in stoneStorages)
        {
            this.StoneCapacity += storage.Capacity;
            this.Stone += storage.Count;
        }
    }

    public void TallyMetal()
    {
        MetalStorage[] metalStorages = GameObject.FindObjectsOfType<MetalStorage>();
        foreach (MetalStorage storage in metalStorages)
        {
            this.MetalCapacity += storage.Capacity;
            this.Metal += storage.Count;
        }
    }

    public void TallyFood()
    {
        FoodStorage[] foodStorages = GameObject.FindObjectsOfType<FoodStorage>();
        foreach (FoodStorage storage in foodStorages)
        {
            this.FoodCapacity += storage.Capacity;
            this.Food += storage.Count;
        }
    }

    public void TallyWater()
    {
        WaterStorage[] waterStorages = GameObject.FindObjectsOfType<WaterStorage>();
        foreach (WaterStorage storage in waterStorages)
        {
            this.WaterCapacity += storage.Capacity;
            this.Water += storage.Count;
        }
    }
    
    public void Census()
    {
        this.Population = GameObject.FindObjectsOfType<HumanStats>().Length;
        House[] houses = GameObject.FindObjectsOfType<House>();
        foreach (House house in houses)
        {
            this.MaxPopulation += house.Capacity;
        }
    }


    public void Clear()
    {
        Wood = 0;
        Stone = 0;
        Metal = 0;
        Food = 0;
        Water = 0;
        Population = 0;
        WoodCapacity = 0;
        StoneCapacity = 0;
        MetalCapacity = 0;
        FoodCapacity = 0;
        WaterCapacity = 0;
        MaxPopulation = 0;
    }

    public void UpdateText()
    {
        if (WoodTxt == null || StoneTxt == null || MetalTxt == null || FoodTxt == null || WaterTxt == null || PopulationTxt == null)
            return;
        WoodTxt.text = $": {Wood} / {WoodCapacity}";
        StoneTxt.text = $": {Stone} / {StoneCapacity}";
        MetalTxt.text = $": {Metal} / {MetalCapacity}";
        FoodTxt.text = $": {Food} / {FoodCapacity}";
        WaterTxt.text = $": {Water} / {WaterCapacity}";
    }

    public int GetRemainingCapacity(string materialType)
    {
        switch (materialType.ToLower())
        {
            case "wood":
                return WoodCapacity - Wood;
            case "stone":
                return StoneCapacity - Stone;
            case "metal":
                return MetalCapacity - Metal;
            case "food":
                return FoodCapacity - Food;
            case "water":
                return WaterCapacity - Water;
            case "population":
                return MaxPopulation - Population;
            default:
                Debug.LogError("Invalid material type: " + materialType);
                return 0;
        }
    }

    public int GetAmount(string materialType)
    {
        switch (materialType.ToLower())
        {
            case "wood":
                return Wood;
            case "stone":
                return Stone;
            case "metal":
                return Metal;
            case "food":
                return Food;
            case "water":
                return Water;
            case "population":
                return Population;
            default:
                Debug.LogError("Invalid material type: " + materialType);
                return 0;
        }

    }

    public int GetMaxCapacity(string materialType)
    {
        switch (materialType.ToLower())
        {
            case "wood":
                return WoodCapacity;
            case "stone":
                return StoneCapacity;
            case "metal":
                return MetalCapacity;
            case "food":
                return FoodCapacity;
            case "water":
                return WaterCapacity;
            case "population":
                return MaxPopulation;
            default:
                Debug.LogError("Invalid material type: " + materialType);
                return 0;
        }

    }

    public bool CanAfford(int woodCost, int stoneCost, int metalCost, int foodCost, int waterCost)
    {
        //If cost is higher
        if (Wood < woodCost || Stone < stoneCost || Metal < metalCost || Food < foodCost || Water < waterCost)
            return false;
        //If no cost is higher
        return true;
    }

    public bool DeductCosts(int woodCost, int stoneCost, int metalCost, int foodCost, int waterCost)
    {
        if (!CanAfford(woodCost, stoneCost, metalCost, foodCost, waterCost))
            return false;
        Debug.Log("Can afford");
        MaterialStorageBase[] storages;
        List<MaterialStorageBase> storageList;
        MaterialStorageBase current;
        int deduction;

        // Deduct wood
        if(woodCost > 0)
        {
            storages = FindObjectsOfType<WoodStorage>();
            storageList = storages.OrderBy(x => x.Count).ToList();
            current = storageList.FirstOrDefault();
            if (current == null)
                return false;
            while (woodCost > 0)
            {
                deduction = Math.Min(woodCost, current.Count);
                current.Count -= deduction;
                woodCost -= deduction;
                storageList.Remove(current);
                current = storageList.FirstOrDefault();
            }
        }
        // Deduct stone
        if (stoneCost > 0)
        {
            storages = FindObjectsOfType<StoneStorage>();
            storageList = storages.OrderBy(x => x.Count).ToList();
            current = storageList.FirstOrDefault();
            if (current == null)
                return false;
            while (stoneCost > 0)
            {
                deduction = Math.Min(stoneCost, current.Count);
                current.Count -= deduction;
                stoneCost -= deduction;
                storageList.Remove(current);
                current = storageList.FirstOrDefault();
            }
        }
        // Deduct metal
        if (metalCost > 0)
        {
            storages = FindObjectsOfType<MetalStorage>();
            storageList = storages.OrderBy(x => x.Count).ToList();
            current = storageList.FirstOrDefault();
            if (current == null)
                return false;
            while (metalCost > 0)
            {
                deduction = Math.Min(metalCost, current.Count);
                current.Count -= deduction;
                metalCost -= deduction;
                storageList.Remove(current);
                current = storageList.FirstOrDefault();
            }
        }
        // Deduct food
        if (foodCost > 0)
        {
            storages = FindObjectsOfType<FoodStorage>();
            storageList = storages.OrderBy(x => x.Count).ToList();
            current = storageList.FirstOrDefault();
            if (current == null)
                return false;
            while (foodCost > 0)
            {
                deduction = Math.Min(foodCost, current.Count);
                current.Count -= deduction;
                foodCost -= deduction;
                storageList.Remove(current);
                current = storageList.FirstOrDefault();
            }
        }
        // Deduct water
        if (waterCost > 0)
        {
            storages = FindObjectsOfType<WaterStorage>();
            storageList = storages.OrderBy(x => x.Count).ToList();
            current = storageList.FirstOrDefault();
            while (waterCost > 0)
            {
                deduction = Math.Min(waterCost, current.Count);
                current.Count -= deduction;
                waterCost -= deduction;
                storageList.Remove(current);
                current = storageList.FirstOrDefault();
            }
        }
        TallyMaterials();
        return true;
    }
}
