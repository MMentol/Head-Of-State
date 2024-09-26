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
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UtilityAI;

namespace Cinaed.GOAP.Complex.Behaviours
{

    [RequireComponent(typeof(NavMeshAgent), typeof(Sensor))]
    public class ComplexHumanBrain : MonoBehaviour
    {
        private AgentBehaviour agent;
        private Inventory inventory;
        public MaterialDataStorage MaterialDataStorage;
        public MaterialPercentage MaterialPercentage;
        public Human human;
        public HumanStats humanStats;
        public VillageStats villageStats;
        public bool logDebug = false;

        //Utility AI
        public List<AIAction> actions;
        public Context context;

        private void Awake()
        {
            this.agent = this.GetComponent<AgentBehaviour>();
            this.inventory = this.GetComponent<Inventory>();
            this.humanStats = this.GetComponent<HumanStats>();
            this.MaterialDataStorage = GameObject.FindObjectOfType<MaterialDataStorage>();
            this.MaterialPercentage = GameObject.FindObjectOfType<MaterialPercentage>();

            context = new Context(this);

            foreach (var action in actions)
            {
                action.Initialize(context);
        }
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

        private void Update()
        {

            UpdateContext();
            AIAction bestAction = null;
            float highestUtility = float.MinValue;

            foreach (var action in actions)

            {
                float utility = action.CalculateUtility(context);
                if (utility > highestUtility)
                {
                    highestUtility = utility;
                    bestAction = action;
                }
            }

            if (bestAction != null)
            {
                bestAction.Execute(context);

            }
        }   
        private void DetermineGoal() 
            {
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
                float resourcePercentage = (float) this.MaterialDataStorage.Wood / (float) this.MaterialDataStorage.WoodCapacity * 100;
            if (resourcePercentage < this.MaterialPercentage.NPCWoodThreshold)
            {
                this.agent.SetGoal<GatherMaterialGoal<Wood>>(false);
                return;
            }

            resourcePercentage = (float) this.MaterialDataStorage.Stone / (float)this.MaterialDataStorage.StoneCapacity * 100;
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

        public void DetermineGoal(int forced)//UtilityAI Forced Goal
        {
            float resourcePercentage = (float)this.MaterialDataStorage.Wood / (float)this.MaterialDataStorage.WoodCapacity * 100;
            if (forced == 4) resourcePercentage = 1;
            if (resourcePercentage < this.MaterialPercentage.NPCWoodThreshold)
            {
                this.agent.SetGoal<GatherMaterialGoal<Wood>>(false);
                return;
            }
            
            resourcePercentage = (float)this.MaterialDataStorage.Stone / (float)this.MaterialDataStorage.StoneCapacity * 100;
            if (forced == 3) resourcePercentage = 1;
            if (resourcePercentage < this.MaterialPercentage.NPCStoneThreshold)
            {
                this.agent.SetGoal<GatherMaterialGoal<Stone>>(false);
                return;
            }

            resourcePercentage = (float)this.MaterialDataStorage.Metal / (float)this.MaterialDataStorage.MetalCapacity * 100;
            if (forced == 2) resourcePercentage = 1;
            if (resourcePercentage < this.MaterialPercentage.NPCMetalThreshold)
            {
                this.agent.SetGoal<GatherMaterialGoal<Metal>>(false);
                return;
            }

            resourcePercentage = (float)this.MaterialDataStorage.Water / (float)this.MaterialDataStorage.WaterCapacity * 100;
            if (forced == 1) resourcePercentage = 1;
            if (resourcePercentage < this.MaterialPercentage.NPCWaterThreshold)
            {
                this.agent.SetGoal<GatherMaterialGoal<Water>>(false);
                return;
            }

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
            if (forced == 9) resourcePercentage = 1;
            this.agent.SetGoal<WanderGoal>(false);
        }

        #region UtilitySide
        private void UpdateContext()
        {
            context.SetData("foodAmount", (float)MaterialDataStorage.Food);
            context.SetData("waterAmount", (float)this.MaterialDataStorage.Water / (float)this.MaterialDataStorage.WaterCapacity);
            context.SetData("woodAmount", (float)this.MaterialDataStorage.Wood / (float)this.MaterialDataStorage.WoodCapacity);
            context.SetData("stoneAmount", (float)this.MaterialDataStorage.Stone / (float)this.MaterialDataStorage.StoneCapacity);
            context.SetData("metalAmount", (float)this.MaterialDataStorage.Metal / (float)this.MaterialDataStorage.MetalCapacity);
            context.SetData("hunger", humanStats._hunger/100f);
            context.SetData("thirst", humanStats._thirst/100f);
            context.SetData("happiness", humanStats._happiness/100f);
            context.SetData("heat", humanStats._heat/100f);
            context.SetData("energy", humanStats._energy/100f);

            context.SetData("partnerExists", humanStats.partnerExists/100f);
            context.SetData("house", humanStats.insideHouse/100f);
        }

        #endregion
    }
}
