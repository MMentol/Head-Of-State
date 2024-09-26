using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts;

public enum Resource
{
    Wood,
    Stone,
    Metal,
    Food,
    Water
}
public class ResourceNode : MonoBehaviour
{
    [SerializeField] public bool logDebug;
    [Header("Resource Node Properties")]
    [SerializeField] public BaseTile _baseType;
    [SerializeField] public Resource resourceType;
    [Range(0,1)]
    [SerializeField] public float nodeChance;
    [SerializeField] public int minAmount;
    [SerializeField] public int maxAmount;
    [SerializeField] public Inventory inventory;

    public void Init()
    {
        //inventory = gameObject.AddComponent<Inventory>();
        //inventory.Init(resourceType.ToString(), Mathf.CeilToInt(Random.Range(minAmount, maxAmount)), logDebug);            
    }
}
