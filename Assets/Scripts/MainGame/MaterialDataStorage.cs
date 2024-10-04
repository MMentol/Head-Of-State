using UnityEngine;
using TMPro;
using GridMap.Structures.Storage;
using System;
using System.Linq;
using System.Collections.Generic;
using GridMap.Resources;
public class MaterialDataStorage : MonoBehaviour
{
    public static MaterialDataStorage Instance;

    //Storage list holders
    public WoodStorage[] WoodStorages;
    public StoneStorage[] StoneStorages;
    public MetalStorage[] MetalStorages;
    public WaterStorage[] WaterStorages;
    public FoodStorage[] FoodStorages;

    public TreeResource[] WoodResources;
    public StoneResource[] StoneResources;
    public MetalResource[] MetalResources;
    public WaterResource[] WaterResources;
    public FoodResource[] FoodResources;

    public House[] Houses;

    //Data holders
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
    public int totalWood = 0;
    public int totalStone = 0;
    public int totalMetal = 0;

    //Text boxes
    public TMP_Text WoodTxt;
    public TMP_Text StoneTxt;
    public TMP_Text MetalTxt;
    public TMP_Text FoodTxt;
    public TMP_Text WaterTxt;
    public TMP_Text PopulationTxt;

    public TMP_Text WoodTxtCurr;
    public TMP_Text StoneTxtCurr;
    public TMP_Text MetalTxtCurr;
    public TMP_Text FoodTxtCurr;
    public TMP_Text WaterTxtCurr;
    public TMP_Text PopulationTxtCurr;

    private void Awake()
    {
        //Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        UpdateStorageLists();
        UpdateSourcesLists();
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
        UpdateStorageLists();
        foreach (WoodStorage storage in WoodStorages)
        {
            this.WoodCapacity += storage.Capacity;
            this.Wood += storage.Count;
            this.totalWood += storage.totalCount;
        }
        foreach (StoneStorage storage in StoneStorages)
        {
            this.StoneCapacity += storage.Capacity;
            this.Stone += storage.Count;
            this.totalStone += storage.totalCount;
        }
        foreach (MetalStorage storage in MetalStorages)
        {
            this.MetalCapacity += storage.Capacity;
            this.Metal += storage.Count;
            this.totalMetal += storage.totalCount;
        }
        foreach (FoodStorage storage in FoodStorages)
        {
            this.FoodCapacity += storage.Capacity;
            this.Food += storage.Count;
        }
        foreach (WaterStorage storage in WaterStorages)
        {
            this.WaterCapacity += storage.Capacity;
            this.Water += storage.Count;
        }
        Population = FindObjectsOfType<HumanStats>().Length;
        Houses = FindObjectsOfType<House>();
        foreach (House house in Houses)
        {
            this.MaxPopulation += house.Capacity;
        }
        UpdateText();
    }

    public void TallyWood()
    {
        //WoodStorage[] woodStorages = GameObject.FindObjectsOfType<WoodStorage>();
        foreach (WoodStorage storage in WoodStorages)
        {
            this.WoodCapacity += storage.Capacity;
            this.Wood += storage.Count;
            this.totalWood += storage.Count;
        }
    }

    public void TallyStone()
    {
        //StoneStorage[] stoneStorages = GameObject.FindObjectsOfType<StoneStorage>();
        foreach (StoneStorage storage in StoneStorages)
        {
            this.StoneCapacity += storage.Capacity;
            this.Stone += storage.Count;
            this.totalStone += storage.Count;
        }
    }

    public void TallyMetal()
    {
        //MetalStorage[] metalStorages = GameObject.FindObjectsOfType<MetalStorage>();
        foreach (MetalStorage storage in MetalStorages)
        {
            this.MetalCapacity += storage.Capacity;
            this.Metal += storage.Count;
            this.totalMetal += storage.Count;
        }
    }

    public void TallyFood()
    {
        //FoodStorage[] foodStorages = GameObject.FindObjectsOfType<FoodStorage>();
        foreach (FoodStorage storage in FoodStorages)
        {
            this.FoodCapacity += storage.Capacity;
            this.Food += storage.Count;
        }
    }

    public void TallyWater()
    {
        //WaterStorage[] waterStorages = GameObject.FindObjectsOfType<WaterStorage>();
        foreach (WaterStorage storage in WaterStorages)
        {
            this.WaterCapacity += storage.Capacity;
            this.Water += storage.Count;
        }
    }
    
    public void Census()
    {
        this.Population = GameObject.FindObjectsOfType<HumanStats>().Length;
        foreach (House house in Houses)
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
        totalWood = 0;
        totalStone = 0;
        totalMetal = 0;
        Population = 0;
        WoodCapacity = 0;
        StoneCapacity = 0;
        MetalCapacity = 0;
        FoodCapacity = 0;
        WaterCapacity = 0;
        MaxPopulation = 0;
    }

    public void UpdateStorageLists()
    {
        WoodStorages = FindObjectsOfType<WoodStorage>();
        StoneStorages = FindObjectsOfType<StoneStorage>();
        MetalStorages = FindObjectsOfType<MetalStorage>();
        WaterStorages = FindObjectsOfType<WaterStorage>();
        FoodStorages = FindObjectsOfType<FoodStorage>();
    }

    public void UpdateSourcesLists()
    {
        WoodResources = FindObjectsOfType<TreeResource>();
        StoneResources = FindObjectsOfType<StoneResource>();
        MetalResources = FindObjectsOfType<MetalResource>();
        WaterResources = FindObjectsOfType<WaterResource>();
        FoodResources = FindObjectsOfType<FoodResource>();
    }

    public void UpdateText()
    {
        if (WoodTxt == null || StoneTxt == null || MetalTxt == null || FoodTxt == null || WaterTxt == null || PopulationTxt == null || WoodTxtCurr == null || StoneTxtCurr == null || MetalTxtCurr == null || FoodTxtCurr == null || WaterTxtCurr == null)
            return;
        WoodTxt.text = $"{WoodCapacity}";
        StoneTxt.text = $"{StoneCapacity}";
        MetalTxt.text = $"{MetalCapacity}";
        FoodTxt.text = $"{FoodCapacity}";
        WaterTxt.text = $"{WaterCapacity}";
        PopulationTxt.text = $"{MaxPopulation}";

        WoodTxtCurr.text = $"{Wood}";
        StoneTxtCurr.text = $"{Stone}";
        MetalTxtCurr.text = $"{Metal}";
        FoodTxtCurr.text = $"{Food}";
        WaterTxtCurr.text = $"{Water}";
        PopulationTxtCurr.text = $"{Population}";
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

    //List optimization to reduce findobject usage
    public TStorage[] GetStoragesOfType<TStorage>()
        where TStorage : MaterialStorageBase
    {
        switch (typeof(TStorage).Name)
        {
            case "WoodStorage":
                return WoodStorages.OfType<TStorage>().ToArray();
            case "StoneStorage":
                return StoneStorages.OfType<TStorage>().ToArray();
            case "MetalStorage":
                return MetalStorages.OfType<TStorage>().ToArray();
            case "WaterStorage":
                return WaterStorages.OfType<TStorage>().ToArray();
            case "FoodStorage":
                return FoodStorages.OfType<TStorage>().ToArray();
            default:
                Debug.LogError("Unsupported storage type: " + typeof(TStorage).Name);
                return new TStorage[0];
        }
    }
    //List optimization to reduce findobject usage
    public TSource[] GetSourceOfType<TSource>()
        where TSource : ResourceSourceBase
    {
        switch (typeof(TSource).Name)
        {
            case "TreeResource":
                return WoodResources.OfType<TSource>().ToArray();
            case "StoneResource":
                return StoneResources.OfType<TSource>().ToArray();
            case "MetalResource":
                return MetalResources.OfType<TSource>().ToArray();
            case "WaterResource":
                return WaterResources.OfType<TSource>().ToArray();
            case "FoodResource":
                return FoodResources.OfType<TSource>().ToArray();
            default:
                Debug.LogError("Unsupported storage type: " + typeof(TSource).Name);
                return new TSource[0];
        }
    }
    public House[] GetHouses()
    {
        return Houses;
    }

    public void DeRegisterSource(Type type, ResourceSourceBase source )
    {
        switch (type.Name)
        {
            case "TreeResource":
                List<TreeResource> woodList = WoodResources.ToList();
                woodList.Remove((TreeResource)source);
                WoodResources = woodList.ToArray();                
                break;
            case "StoneResource":
                List<StoneResource> stoneList = StoneResources.ToList();
                stoneList.Remove((StoneResource)source);
                StoneResources = stoneList.ToArray();
                break;
            case "MetalResource":
                List<MetalResource> metalList = MetalResources.ToList();
                metalList.Remove((MetalResource)source);
                MetalResources = metalList.ToArray();
                break;
            case "WaterResource":
                List<WaterResource> waterList = WaterResources.ToList();
                waterList.Remove((WaterResource)source);
                WaterResources = waterList.ToArray();
                break;
            case "FoodResource":
                List<FoodResource> foodList = FoodResources.ToList();
                foodList.Remove((FoodResource)source);
                FoodResources = foodList.ToArray();
                break;
            default:
                //Unexpected resource types
                break;
        }
        Debug.Log($"Removed {source.name} from {type.Name}");
    }
}
