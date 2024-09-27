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
    public UnityEngine.GameObject[] woodStorage;
    public UnityEngine.GameObject[] stoneStorage;
    public UnityEngine.GameObject[] metalStorage;

    public HumanStats _hStats;
    public MaterialDataStorage storages;
    public Inventory inventory;
    public MaterialPercentage MaterialPercentage;


    private void Awake()
    {
        //foodSources = UnityEngine.GameObject.FindGameObjectsWithTag("WoodSource");
        //waterSources = UnityEngine.GameObject.FindGameObjectsWithTag("WaterSource");
        woodSources = UnityEngine.GameObject.FindGameObjectsWithTag("WoodSource");
        stoneSources = UnityEngine.GameObject.FindGameObjectsWithTag("StoneSource");
        metalSources = UnityEngine.GameObject.FindGameObjectsWithTag("MetalSource");

        woodStorage = UnityEngine.GameObject.FindGameObjectsWithTag("WoodStorage");
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

            //new Sequence(new List<Node>
            //{
            //    new checkFoodTask(transform,this._hStats),
            //    new lookFoodTask(transform, foodSources),
            //    new walkToFoodTask(transform),
            //    new getFoodTask(transform,this.inventory)
            //}),

    
            //new Sequence(new List<Node>
            //{
            //    new checkWaterTask(transform,this._hStats),
            //    new lookWaterTask(transform, waterSources),
            //    new walkToWaterTask(transform),
            //    new getWaterTask(transform,this.inventory)
            //}),
            
            new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new checkForWoodStorageTask(transform, woodStorage),
                    new checkWoodResourceTask(transform,this.storages),
                    new lookForWoodResourceTask(transform,woodSources),
                    new walkToWoodResourceTask(transform),
                    new getWoodResourceTask(transform,this.inventory),
                    new walkToWoodStorageTask(transform),
                    new depositWoodTask(transform, this.inventory)
                }),

                new Sequence(new List<Node>
                {
                    new checkForStoneStorageTask(transform, stoneStorage),
                    new checkStoneResourceTask(transform,this.storages),
                    new lookForStoneResourceTask(transform, waterSources),
                    new walkToStoneResourceTask(transform),
                    new getStoneResourceTask(transform,this.inventory),
                    new walkToStoneStorageTask(transform),
                    new depositStoneTask(transform, this.inventory)
                }),

                new Sequence(new List<Node>
                {
                    new checkForMetalStorageTask(transform, metalStorage),
                    new checkMetalResourceTask(transform,this.storages),
                    new lookForMetalResourceTask(transform, metalSources),
                    new walkToMetalResourceTask(transform),
                    new getMetalResourceTask(transform,this.inventory),
                    new walkToMetalStorageTask(transform),
                    new depositMetalTask(transform, this.inventory)
                })
            }),

            //new Sequence(new List<Node>
            //{
            //    new checkHeatAndEnergyTask(transform,this._hStats),
            //    new lookForHomeTask(transform,this._hStats),
            //    new walkToHomeTask(transform),
            //    new sleepTask(transform,this._hStats)
            //}),

            //new Sequence(new List<Node>
            //{
            //    new isInHomeTask(transform,this._hStats),
            //    new statsFulfilledTask(transform,this._hStats),
            //    new reproduceTask(transform)
            //}),
            

            new WanderBT(transform)
            //new Sequence(new List<Node>
            //{
            //    new CheckEnemyInAttackRange(transform),
            //    new TaskAttack(transform),
            //}),
            //new Sequence(new List<Node>
            //{
            //    new CheckEnemyInFOVRange(transform),
            //    new TaskGoToTarget(transform),
            //}),
            //new TaskPatrol(transform, waypoints),


        });

        return root;
    }
}
