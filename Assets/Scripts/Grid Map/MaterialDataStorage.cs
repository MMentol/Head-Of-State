using UnityEngine;
using TMPro;
using GridMap.Structures.Storage;
public class MaterialDataStorage : MonoBehaviour
{
    public int Wood = 0;
    public int Stone = 0;
    public int Metal = 0;
    public int Food = 0;
    public int Water = 0;
    public int WoodCapacity = 0;
    public int StoneCapacity = 0;
    public int MetalCapacity = 0;
    public int FoodCapacity = 0;
    public int WaterCapacity = 0;

    public TMP_Text WoodTxt;
    public TMP_Text StoneTxt;
    public TMP_Text MetalTxt;
    public TMP_Text FoodTxt;
    public TMP_Text WaterTxt;

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

    public void Clear()
    {
        Wood = 0;
        Stone = 0;
        Metal = 0;
        Food = 0;
        Water = 0;
        WoodCapacity = 0;
        StoneCapacity = 0;
        MetalCapacity = 0;
        FoodCapacity = 0;
        WaterCapacity = 0;
    }

    public void UpdateText()
    {
        WoodTxt.text = $"Wood: {Wood} / {WoodCapacity}";
        StoneTxt.text = $"Stone: {Stone} / {StoneCapacity}";
        MetalTxt.text = $"Metal: {Metal} / {MetalCapacity}";
        FoodTxt.text = $"Food: {Food} / {FoodCapacity}";
        WaterTxt.text = $"Water: {Water} / {WaterCapacity}";
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
            default:
                Debug.LogError("Invalid material type: " + materialType);
                return 0;
        }

    }

}
