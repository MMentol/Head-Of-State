using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private AgentMono agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GameObject.Find("Agent").GetComponent<AgentMono>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.ActivateNextAction();
    }
}
