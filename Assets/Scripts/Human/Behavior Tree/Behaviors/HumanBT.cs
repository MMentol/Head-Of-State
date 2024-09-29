using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using Scripts;

public class HumanBT : Tree
{
    public UnityEngine.GameObject[] foodSources;
    public UnityEngine.GameObject[] waterSources;
    public UnityEngine.GameObject[] woodSources;
    public UnityEngine.GameObject[] stoneSources;
    public UnityEngine.GameObject[] metalSources;

    public UnityEngine.GameObject[] foodStorage;
    public UnityEngine.GameObject[] waterStorage;
    public UnityEngine.GameObject[] stoneStorage;
    public UnityEngine.GameObject[] metalStorage;

    public HumanStats _hStats;
    public MaterialDataStorage storages;
    public Inventory inventory;
    //public MaterialPercentage MaterialPercentage;


    private void Awake()
    {
        //foodSources = UnityEngine.GameObject.FindGameObjectsWithTag("WoodSource");
        //waterSources = UnityEngine.GameObject.FindGameObjectsWithTag("WaterSource");
        stoneSources = UnityEngine.GameObject.FindGameObjectsWithTag("StoneSource");
        metalSources = UnityEngine.GameObject.FindGameObjectsWithTag("MetalSource");

        stoneStorage = UnityEngine.GameObject.FindGameObjectsWithTag("StoneStorage");
        metalStorage = UnityEngine.GameObject.FindGameObjectsWithTag("MetalStorage");

        this._hStats = GetComponent<HumanStats>();
        this.storages = FindObjectOfType<MaterialDataStorage>();
        this.inventory = this.GetComponent<Inventory>();




    }

   

    protected override Node SetupTree()
    {

        Node root = new Selector(new List<Node>
        {
            new Selector(new List<Node>
            {
                new getFoodTask(transform),
                new Sequence(new List<Node>
                {
                    new checkFoodTask(transform),
                    new lookFoodTask(transform),
                    new walkToFoodTask(transform),

                }),

            }),

            new Selector(new List<Node>
            {
                new getWaterTask(transform),
                new Sequence(new List<Node>
                {
                    new checkWaterTask(transform),
                    new lookWaterTask(transform),
                    new walkToWaterTask(transform),

                }),

            }),
            
            new Selector(new List<Node>
            {
                new Selector(new List<Node>  {

                    new depositWoodTask(transform),
                    new Sequence(new List<Node>
                    {
                        new getWoodResourceTask(transform),
                        new walkToWoodStorageTask(transform),

                    }),
                new Sequence(new List<Node>
                    {
                        //new checkWoodResourceTask(transform),
                        new checkForWoodStorageTask(transform),
                        new lookForWoodResourceTask(transform),
                        new walkToWoodResourceTask(transform),

                    }),
                }),
                new Selector(new List<Node>  {

                    new depositStoneTask(transform),
                    new Sequence(new List<Node>
                    {
                        new getStoneResourceTask(transform),
                        new walkToStoneStorageTask(transform),

                    }),
                new Sequence(new List<Node>
                    {
                        //new checkStoneResourceTask(transform),
                        new checkForStoneStorageTask(transform),
                        new lookForStoneResourceTask(transform),
                        new walkToStoneResourceTask(transform),

                    }),
                }),
                new Selector(new List<Node>  {

                    new depositMetalTask(transform),
                    new Sequence(new List<Node>
                    {
                        new getMetalResourceTask(transform),
                        new walkToMetalStorageTask(transform),

                    }),
                new Sequence(new List<Node>
                    {
                        //new checkMetalResourceTask(transform),
                        new checkForMetalStorageTask(transform),
                        new lookForMetalResourceTask(transform),
                        new walkToMetalResourceTask(transform),

                    }),
                }),
            }),
            new Selector(new List<Node>
            {
                new Sequence(new List<Node>
            {
                new statsFulfilledTask(transform),
                new reproduceTask(transform)
            }),
                new sleepTask(transform),
                new Sequence(new List<Node>
                {
                    new checkHeatAndEnergyTask(transform),
                    new lookForHomeTask(transform),
                    new walkToHomeTask(transform),

                }),

            


            new WanderBT(transform)

            }),


        });

        return root;
    }
}
