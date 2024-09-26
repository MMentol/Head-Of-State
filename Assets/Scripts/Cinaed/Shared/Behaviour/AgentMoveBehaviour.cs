using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Interfaces;
using UnityEngine;

namespace Cinaed.GOAP.Behaviours
{
    public class AgentMoveBehaviour : MonoBehaviour
    {
        [SerializeField] private AgentBehaviour agent;
        [SerializeField] private ITarget currentTarget;
        [SerializeField] private bool shouldMove;
        [SerializeField] private HumanController humanController;
        [SerializeField] private bool logDebug = false;

        private void Awake()
        {
            this.agent = this.GetComponent<AgentBehaviour>();
            this.humanController = this.GetComponent<HumanController>();
        }

        private void OnEnable()
        {
            this.agent.Events.OnTargetInRange += this.OnTargetInRange;
            this.agent.Events.OnTargetChanged += this.OnTargetChanged;
            this.agent.Events.OnTargetOutOfRange += this.OnTargetOutOfRange;
        }

        private void OnDisable()
        {
            this.agent.Events.OnTargetInRange -= this.OnTargetInRange;
            this.agent.Events.OnTargetChanged -= this.OnTargetChanged;
            this.agent.Events.OnTargetOutOfRange -= this.OnTargetOutOfRange;
        }

        private void OnTargetInRange(ITarget target)
        {
            this.shouldMove = false;
        }

        private void OnTargetChanged(ITarget target, bool inRange)
        {
            this.currentTarget = target;
            this.shouldMove = !inRange;
        }

        private void OnTargetOutOfRange(ITarget target)
        {
            this.shouldMove = true;
        }

        public void Update()
        {

            if (!this.shouldMove)
                return;

            if (this.currentTarget == null)
                return;

            //Debug.Log("Moving");
            //humanController.SetTargetPosition(new Vector3(this.currentTarget.Position.x, this.transform.position.y));
            if (this.currentTarget.Position == Vector3.zero)
                if (logDebug) { Debug.Log("Target was destroyed."); }

            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.currentTarget.Position.x, this.transform.position.y, this.currentTarget.Position.z), Time.deltaTime);
        }

        private void OnDrawGizmos()
        {
            if (this.currentTarget == null)
                return;

            Gizmos.DrawLine(this.transform.position, this.currentTarget.Position);
        }
    }
}