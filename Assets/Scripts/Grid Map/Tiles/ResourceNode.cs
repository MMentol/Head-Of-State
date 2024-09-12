using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [Header("Resource Node Properties")]
    [SerializeField] public BaseTile _baseType;
    [SerializeField] public Resource resourceType;
    [Range(0,1)]
    [SerializeField] public float nodeChance;
    [SerializeField] public Inventory inventory;

    void Init()
    {
        inventory = gameObject.AddComponent<Inventory>();
        inventory.Init(resourceType.ToString(), Random.Range(1200,2500), true);            
    }
}
