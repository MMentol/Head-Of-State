using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes.Validators;
using TMPro;
using UnityEngine;

namespace Cinaed
{
    public class TextUpdate : MonoBehaviour
    {
        private TextMeshProUGUI text;
        private AgentBehaviour agent;

        private void Awake()
        {
            this.text = this.GetComponentInChildren<TextMeshProUGUI>();
            this.agent = this.GetComponent<AgentBehaviour>();
        }

        private void Update()
        {
            this.text.text = this.GetText();
        }

        private string GetText()
        {
            if (this.agent.CurrentAction is null)
                return "Idle";

            return $"{this.agent.CurrentGoal.GetType().GetGenericTypeName()}\n{this.agent.CurrentAction.GetType().GetGenericTypeName()}\n{this.agent.State}";
        }
    }
}