using UnityEngine;

public class Storage : MonoBehaviour
{
    [SerializeField] public int Wood = 0;
    [SerializeField] public int Stone = 0;
    [SerializeField] public int Metal = 0;
    [SerializeField] public int Food = 0;
    [SerializeField] public int Water = 0;
    [SerializeField] public int WoodCapacity= 0;
    [SerializeField] public int StoneCapacity = 0;
    [SerializeField] public int MetalCapacity = 0;
    [SerializeField] public int FoodCapacity= 0;
    [SerializeField] public int WaterCapacity = 0;

    public int DepositMaterial(string type, int toDeposit)
    {
        int remainingCapacity;

        switch (type)
        {
            case "wood":
                remainingCapacity = WoodCapacity - Wood;
                break;
            case "stone":
                remainingCapacity = StoneCapacity - Stone;
                break;
            case "metal":
                remainingCapacity = MetalCapacity - Metal;
                break;
            case "food":
                remainingCapacity = FoodCapacity - Food;
                break;
            case "water":
                remainingCapacity = WaterCapacity - Water;
                break;
            default:
                Debug.LogError("Invalid material type: " + type);
                return toDeposit;
        }

        int deposited = Mathf.Min(toDeposit, remainingCapacity);
        switch (type)
        {
            case "wood":
                Wood += deposited;
                break;
            case "stone":
                Stone += deposited;
                break;
            case "metal":
                Metal += deposited;
                break;
            case "food":
                Food += deposited;
                break;
            case "water":
                Water += deposited;
                break;
            default:
                Debug.LogError("Unexpected material type: " + type);
                break;
        }

        return toDeposit - deposited;
    }


    public int GetCapacity(string type)
    {
        switch (type)
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
                return 0;
        }
    }
}
