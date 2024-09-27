using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Cinaed.GOAP.Complex
{
    public static class AgentIds
    {
        public const string Human = "Human";
    }

    public class AgentSpawner : MonoBehaviour
    {
        private static readonly Vector2 Bounds = new Vector2(15, 8);
        public GridManager grid;
        public Vector2 customBounds;
        public bool useCustomBounds = false;
        public int sets = 2;

        private IGoapRunner goapRunner;

        [SerializeField]
        private GameObject agentPrefab;

        private void Awake()
        {
            this.grid = FindObjectOfType<GridManager>();
            this.goapRunner = FindObjectOfType<GoapRunnerBehaviour>();
            this.agentPrefab.SetActive(false);
        }

        private void Start()
        {
            for (int i = 0; i < sets; i++)
            {
                this.SpawnAgent(AgentIds.Human, Random.ColorHSV());
            }

        }

        public void CreateNewHuman(string setId, Color color, Vector3 position)
        {
            var agent = Instantiate(this.agentPrefab, position, Quaternion.identity, gameObject.transform).GetComponent<AgentBehaviour>();

            agent.GoapSet = this.goapRunner.GetGoapSet(setId);
            agent.gameObject.SetActive(true);

            agent.gameObject.transform.name = $"Agent - {agent.GetInstanceID()}";

            var brain = agent.GetComponent<AgentBrain>();

            var spriteRenderer = agent.GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.color = color;
        }

        private void SpawnAgent(string setId, Color color)
        {
            var agent = Instantiate(this.agentPrefab, this.GetRandomPosition(), Quaternion.identity, gameObject.transform).GetComponent<AgentBehaviour>();

            agent.GoapSet = this.goapRunner.GetGoapSet(setId);
            agent.gameObject.SetActive(true);

            agent.gameObject.transform.name = $"Agent - {agent.GetInstanceID()}";

            var brain = agent.GetComponent<AgentBrain>();

            var spriteRenderer = agent.GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.color = color;
        }

        private Vector3 GetRandomPosition()
        {
            var randomX = Random.Range(-Bounds.x, Bounds.x);
            var randomY = Random.Range(-Bounds.y, Bounds.y);

            if (useCustomBounds)
            {
                if (customBounds.x > grid.GetWidth() - 1)
                    customBounds.x = grid.GetWidth() - 1;
                if (customBounds.y > grid.GetHeight() - 1)
                    customBounds.y = grid.GetHeight() - 1;

                randomX = Random.Range(0, customBounds.x);
                randomY = Random.Range(0, customBounds.y);
                //Debug.Log($"CUSTOM X: {randomX}, Y: {randomY}");
                Tile tile = grid.GetTileAtPos(new Vector2(Mathf.Round(randomX), Mathf.Round(randomY)));
                //Debug.Log($"Tile Pos: {tile.transform.position}");
                return tile.gameObject.transform.position;
            }

            Debug.Log($"X: {randomX}, Y: {randomY}");
            return new Vector3(randomX, 0f, randomY);
        }
    }
}