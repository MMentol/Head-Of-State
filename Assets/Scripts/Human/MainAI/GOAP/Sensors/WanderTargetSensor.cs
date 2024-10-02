using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using UnityEngine;

public class WanderTargetSensor : LocalTargetSensorBase
{
    // Called when the class is created.
    public override void Created()
    {
    }

    // Called each frame. This can be used to gather data from the world before the sense method is called.
    // This can be used to gather 'base data' that is the same for all agents, and otherwise would be performed multiple times during the Sense method.
    public override void Update()
    {
    }

    // Called when the sensor needs to sense a target for a specific agent.
    public override ITarget Sense(IMonoAgent agent, IComponentReference references)
    {
        var random = this.GetRandomPosition(agent);

        return new PositionTarget(random);
    }

    private Vector3 GetRandomPosition(IMonoAgent agent)
    {
        var random = Random.insideUnitCircle * 5f;
        var position = agent.transform.position + new Vector3(Mathf.Clamp(Mathf.RoundToInt(random.x), 0, 99), 0f, Mathf.Clamp(Mathf.RoundToInt(random.y), 0, 99));

        return position;
    }
}