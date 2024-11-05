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
        public bool useBT = false;

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
            MaterialDataStorage.Instance.Census();
            MaterialDataStorage.Instance.UpdateText();
        }

        public void CreateNewHuman(string setId, Color color, Vector3 position)
        {
            Debug.Log("spawnPos:" + position);
            var agent = Instantiate(this.agentPrefab, position, Quaternion.identity, gameObject.transform);
            
            if (!useBT){ 
                agent.GetComponent<AgentBehaviour>().GoapSet = this.goapRunner.GetGoapSet(setId);
                agent.GetComponent<TextUpdate>().goap = true;
            } else {
                agent.GetComponent<TextUpdate>().bt = true;
            }
            agent.SetActive(true);

            agent.transform.name = $"Agent - {agent.GetInstanceID()}";

            var brain = agent.GetComponent<AgentBrain>();

            var spriteRenderer = agent.GetComponentInChildren<SpriteRenderer>();
            //spriteRenderer.color = color;
        }

        private void SpawnAgent(string setId, Color color)
        {
            var agent = Instantiate(this.agentPrefab, this.GetRandomPosition(), Quaternion.identity, gameObject.transform);
            
            if (!useBT){ 
                agent.GetComponent<AgentBehaviour>().GoapSet = this.goapRunner.GetGoapSet(setId);
                agent.GetComponent<TextUpdate>().goap = true;
            } else {
                agent.GetComponent<TextUpdate>().bt = true;
            }
            agent.SetActive(true);

            agent.transform.name = $"Agent - {agent.GetInstanceID()}";
            agent.GetComponent<HumanStats>().inBabyPhase = 0;

            var brain = agent.GetComponent<AgentBrain>();

            var spriteRenderer = agent.GetComponentInChildren<SpriteRenderer>();
            //spriteRenderer.color = color;
        }

        private Vector3 GetRandomPosition()
        {
            var randomX = Random.Range(-Bounds.x, Bounds.x);
            var randomY = Random.Range(-Bounds.y, Bounds.y);

            if (useCustomBounds)
            {
                Tile tile;
                do
                {
                    if (customBounds.x > grid.GetWidth() - 1)
                        customBounds.x = grid.GetWidth() - 1;
                    if (customBounds.y > grid.GetHeight() - 1)
                        customBounds.y = grid.GetHeight() - 1;

                    randomX = Random.Range(40, customBounds.x + 40);
                    randomY = Random.Range(40, customBounds.y + 40);
                    //Debug.Log($"CUSTOM X: {randomX}, Y: {randomY}");
                    tile = grid.GetTileAtPos(new Vector2(Mathf.Round(randomX), Mathf.Round(randomY)));
                } while (!tile.isWalkable);
                //Debug.Log($"Tile Pos: {tile.transform.position}");
                return tile.gameObject.transform.position;
            }

            Debug.Log($"X: {randomX}, Y: {randomY}");
            return new Vector3(randomX, 0f, randomY);
        }
    }
}