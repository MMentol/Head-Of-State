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
            if (this.inventory.GetResourceCount("Wood") == 0)
            {
                this.agent.SetGoal<GatherWoodGoal>(true);
            }
        }
    }
}