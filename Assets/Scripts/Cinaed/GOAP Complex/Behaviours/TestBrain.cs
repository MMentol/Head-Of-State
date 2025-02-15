﻿using Cinaed.GOAP.Complex.Factories;
using Cinaed.GOAP.Complex.Goals;
using Cinaed.GOAP.Complex.Interfaces;
using Cinaed.GOAP.Simple.Goals;
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Interfaces;
using Demos.Shared.Goals;
using GridMap.Resources;
using Items;
using Scripts;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UtilityAI;

namespace Cinaed.GOAP.Complex.Behaviours
{
    public class TestBrain : MonoBehaviour
    {
        private AgentBehaviour agent;
        private Inventory inventory;
        public MaterialDataStorage MaterialDataStorage;
        public MaterialPercentage MaterialPercentage;
        public HouseActivitySettings HouseActivitySettings;
        public Human human;
        public HumanStats humanStats;
        public VillageStats villageStats;
        public bool logDebug = false;

        private void Awake()
        {
            this.agent = this.GetComponent<AgentBehaviour>();
            this.inventory = this.GetComponent<Inventory>();
            this.humanStats = this.GetComponent<HumanStats>();
            this.MaterialDataStorage = GameObject.FindObjectOfType<MaterialDataStorage>();
            this.MaterialPercentage = GameObject.FindObjectOfType<MaterialPercentage>();
            this.HouseActivitySettings = GameObject.FindObjectOfType<HouseActivitySettings>();
        }

        private void OnEnable()
        {
            this.agent.Events.OnActionStop += this.OnActionStop;
            this.agent.Events.OnNoActionFound += this.OnNoActionFound;
            this.agent.Events.OnGoalCompleted += this.OnGoalCompleted;
        }

        private void OnDisable()
        {
            this.agent.Events.OnActionStop -= this.OnActionStop;
            this.agent.Events.OnNoActionFound -= this.OnNoActionFound;
            this.agent.Events.OnGoalCompleted -= this.OnGoalCompleted;
        }

        private void OnNoActionFound(IGoalBase goal)
        {
            this.agent.SetGoal<WanderGoal>(false);
        }

        private void OnGoalCompleted(IGoalBase goal)
        {
            this.agent.SetGoal<WanderGoal>(false);
        }

        private void OnActionStop(IActionBase action)
        {
            this.DetermineGoal();
        }

        private void Start()
        {
            //this.agent.SetGoal<WanderGoal>(false);
            this.DetermineGoal();
        }

        private void Update() { }
        private void DetermineGoal()
        {
            //THIRST AND HUNGER
            if (this.humanStats._thirst > 50 && this.agent.CurrentGoal is not EatGoal)
            {
                this.agent.SetGoal<DrinkGoal>(false);
                return;
            }
            if (this.humanStats._hunger > 50 && this.agent.CurrentGoal is not DrinkGoal)
            {
                this.agent.SetGoal<EatGoal>(false);
                return;
            }
            //BREEDER SETUP
            if (this.humanStats._happiness > HouseActivitySettings.RequiredHappiness && MaterialDataStorage.Population < MaterialDataStorage.MaxPopulation)
            {
                this.agent.SetGoal<BreedingGoal>(false);
                return;
            }                
            //Items in Inventory
            if (inventory.items.Where(item => item is Pickaxe).ToArray().Length < 1)
            {
                this.agent.SetGoal<CraftItemGoal<Pickaxe>>(false);
                //Debug.Log("Get pick");
                return;
            }

            if (inventory.items.Where(item => item is Bucket).ToArray().Length < 1)
            {
                this.agent.SetGoal<CraftItemGoal<Bucket>>(false);
                //Debug.Log("Get bucket");
                return;
            }
            //Resources in Inventory
            float resourcePercentage = (float)this.MaterialDataStorage.Wood / (float)this.MaterialDataStorage.WoodCapacity * 100;
            if (resourcePercentage < this.MaterialPercentage.NPCWoodThreshold)
            {
                this.agent.SetGoal<GatherMaterialGoal<Wood>>(false);
                return;
            }

            resourcePercentage = (float)this.MaterialDataStorage.Stone / (float)this.MaterialDataStorage.StoneCapacity * 100;
            if (resourcePercentage < this.MaterialPercentage.NPCStoneThreshold)
            {
                this.agent.SetGoal<GatherMaterialGoal<Stone>>(false);
                return;
            }

            resourcePercentage = (float)this.MaterialDataStorage.Metal / (float)this.MaterialDataStorage.MetalCapacity * 100;
            if (resourcePercentage < this.MaterialPercentage.NPCMetalThreshold)
            {
                this.agent.SetGoal<GatherMaterialGoal<Metal>>(false);
                return;
            }

            resourcePercentage = (float)this.MaterialDataStorage.Water / (float)this.MaterialDataStorage.WaterCapacity * 100;
            if (resourcePercentage < this.MaterialPercentage.NPCWaterThreshold)
            {
                this.agent.SetGoal<GatherMaterialGoal<Water>>(false);
                return;
            }
            resourcePercentage = (float)this.MaterialDataStorage.Food / (float)this.MaterialDataStorage.FoodCapacity * 100;
            if (resourcePercentage < this.MaterialPercentage.NPCFoodThreshold)
            {
                this.agent.SetGoal<GatherMaterialGoal<Food>>(false);
                return;
            }

            //Try to empty inventory
            /*
            if (inventory.used > 0)
            {
                if (this.inventory.GetResourceCount("wood") > 0)
                {
                    this.agent.SetGoal<DepositMaterialGoal<Wood>>(false);
                    return;
                }
                if (this.inventory.GetResourceCount("stone") > 0)
                {
                    this.agent.SetGoal<DepositMaterialGoal<Stone>>(false);
                    return;
                }
                if (this.inventory.GetResourceCount("metal") > 0)
                {
                    this.agent.SetGoal<DepositMaterialGoal<Metal>>(false);
                    return;
                }
                if (this.inventory.GetResourceCount("food") > 0)
                {
                    this.agent.SetGoal<DepositMaterialGoal<Food>>(false);
                    return;
                }
                if (this.inventory.GetResourceCount("water") > 0)
                {
                    this.agent.SetGoal<DepositMaterialGoal<Water>>(false);
                    return;
                }
            }*/

            this.agent.SetGoal<WanderGoal>(false);
        }           

    }
}

