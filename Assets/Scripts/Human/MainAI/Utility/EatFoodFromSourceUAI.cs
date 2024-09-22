using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFoodFromSourceUAI : AgentAction
{
    public EatFoodFromSourceUAI()
    { 
    }

    public override void OnStart(IAiContext context)
    {
        var agent = context.Agent as AgentMono;
    }
    // The OnGoing method is called on every consecutive selection
    public override void OnGoing(IAiContext context)
    {
        var agent = context.Agent as AgentMono;

    }

    // The OnEnd method is called, when the decision was selected last evaluation, but not this evaluation
    public override void OnEnd(IAiContext context)
    {
        var agent = context.Agent as AgentMono;
    }
}
