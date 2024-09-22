using Cinaed.GOAP.Simple.Goals;
using CrashKonijn.Goap.Behaviours;
using Scripts;
using UnityEngine;

namespace Cinaed.GOAP.Behaviors
{
    public class AgentBrain : MonoBehaviour
    {
        private AgentBehaviour agent;
        private Inventory inventory;

        private void Awake()
        {
            this.agent = this.GetComponent<AgentBehaviour>();
            this.inventory = this.GetComponent<Inventory>();
        }

        private void Update()
        {
            int woodPercentage = (this.inventory.GetResourceCount("wood") / inventory.size * 100);
            if (woodPercentage < 75)
            {
                //Debug.Log("Set Goal to Wood");
                this.agent.SetGoal<GatherWoodGoal>(true);
            }
            else { this.agent.SetGoal<GatherWoodGoal>(false); }
        }
    }
}