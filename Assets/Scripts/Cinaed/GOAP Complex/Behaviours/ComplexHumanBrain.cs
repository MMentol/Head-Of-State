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
        public float MaterialPercentage = 75.0f;

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

        private void Start() { DetermineGoal(); }

        private void Update() {}

        private void DetermineGoal()
        {
            string[] materialsToDetermine = { "wood", "stone", "metal", /*"food",*/ "water" };
            Dictionary<string, float> resourcesNeeded = new Dictionary<string, float>();

            for (int i = 0; i < materialsToDetermine.Length; i++)
            {
                float resourcePercentage = (float) this.inventory.GetResourceCount(materialsToDetermine[i]) / (float) inventory.size * 100;
                if (resourcePercentage < this.MaterialPercentage)
                    resourcesNeeded.Add(materialsToDetermine[i], resourcePercentage);
                else 
                {
                    switch (materialsToDetermine[i])
                    {
                        case "wood":
                            this.agent.SetGoal<GatherMaterialGoal<Wood>>(false);
                            break;
                        case "stone":
                            this.agent.SetGoal<GatherMaterialGoal<Stone>>(false);
                            break;
                        case "metal":
                            this.agent.SetGoal<GatherMaterialGoal<Metal>>(false);
                            break;
                        case "food":
                            //this.agent.SetGoal<GatherMaterialGoal<Food>>(false);
                            break;
                        case "water":
                            this.agent.SetGoal<GatherMaterialGoal<Water>>(false);
                            break;
                    }
                }
            }

            string lowest = resourcesNeeded.OrderBy(x => x.Value).FirstOrDefault().Key;
            switch (lowest)
            {
                case "wood":
                    this.agent.SetGoal<GatherMaterialGoal<Wood>>(true);
                    break;
                case "stone":
                    this.agent.SetGoal<GatherMaterialGoal<Stone>>(true);
                    break;
                case "metal":
                    this.agent.SetGoal<GatherMaterialGoal<Metal>>(true);
                    break;
                case "food":
                    this.agent.SetGoal<GatherMaterialGoal<Food>>(true);
                    break;
                case "water":
                    this.agent.SetGoal<GatherMaterialGoal<Water>>(true);
                    break;
            }

        }
    }
}
