using UnityEngine;

namespace Demos.Shared.Behaviours
{
    public class ThirstBehaviour : MonoBehaviour
    {
        public float thirst = 50;

        private void Awake()
        {
            this.thirst = Random.Range(0, 100f);
            // this.hunger = 80f;
        }

        private void FixedUpdate()
        {
            this.thirst += Time.fixedDeltaTime * 2f;
        }
    }
}