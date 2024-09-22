using Cinaed.GOAP.Complex.Goals;
using Cinaed.GOAP.Complex.Interfaces;
using Cinaed.GOAP.Simple.Goals;
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Interfaces;
using Demos.Shared.Goals;
using GridMap.Resources;
using Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cinaed.GOAP.Complex.Behaviours
{
    public class ComplexHumanBrain : MonoBehaviour
    {
        private AgentBehaviour agent;
        private Inventory inventory;
        public float MaterialPercentage = 5.0f;
        public bool logDebug = false;

        private void Awake()
        {
            this.agent = this.GetComponent<AgentBehaviour>();
            this.inventory = this.GetComponent<Inventory>();
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
            float resourcePercentage = (float)this.inventory.GetResourceCount("wood") / (float)inventory.size * 100;
            if (resourcePercentage < this.MaterialPercentage)
            {
                this.agent.SetGoal<GatherMaterialGoal<Wood>>(false);
                return;
            }

            resourcePercentage = (float)this.inventory.GetResourceCount("stone") / (float)inventory.size * 100;
            if (resourcePercentage < this.MaterialPercentage)
            {
                this.agent.SetGoal<GatherMaterialGoal<Stone>>(false);
                return;
            }

            resourcePercentage = (float)this.inventory.GetResourceCount("metal") / (float)inventory.size * 100;
            if (resourcePercentage < this.MaterialPercentage)
            {
                this.agent.SetGoal<GatherMaterialGoal<Metal>>(false);
                return;
            }

            resourcePercentage = (float)this.inventory.GetResourceCount("water") / (float)inventory.size * 100;
            if (resourcePercentage < this.MaterialPercentage)
            {
                this.agent.SetGoal<GatherMaterialGoal<Water>>(false);
                return;
            }

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
            }

            this.agent.SetGoal<WanderGoal>(false);
        }
    }
}
