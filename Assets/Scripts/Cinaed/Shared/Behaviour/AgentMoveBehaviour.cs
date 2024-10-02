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
        [SerializeField] public Animator anim;
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

            int x = Mathf.RoundToInt(Mathf.Clamp(this.currentTarget.Position.x, 0, 100));
            int y = (int)this.currentTarget.Position.y;
            int z = Mathf.RoundToInt(Mathf.Clamp(this.currentTarget.Position.z, 0, 100));
            Vector3 targetPos = new Vector3(x, y, z);
            Debug.Log("targetPos:" + targetPos);
            humanController.SetTargetPosition(targetPos);
        }

        private void OnTargetOutOfRange(ITarget target)
        {
            this.shouldMove = true;
        }

        public void Update()
        {

            if (!this.shouldMove) {
                anim.SetBool("isWalking", false);
                return;
            }

            if (this.currentTarget == null)  {
                anim.SetBool("isWalking", false);
                return;
            }   

            //Debug.Log("Moving");
            
            if (this.currentTarget.Position == Vector3.zero) {
                anim.SetBool("isWalking", false);
                if (logDebug) {   Debug.Log("Target was destroyed.");   }
            }
            humanController.HandleMovement();
            //this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.currentTarget.Position.x, this.transform.position.y, this.currentTarget.Position.z), Time.deltaTime);
            anim.SetBool("isWalking", true);
        }

        private void OnDrawGizmos()
        {
            if (this.currentTarget == null)
                return;

            Gizmos.DrawLine(this.transform.position, this.currentTarget.Position);
        }
    }
}