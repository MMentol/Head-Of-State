using UnityEngine;

public class PrintTxt : AgentAction
{
    public PrintTxt()
    {
        // Add parameters in the constructor
        AddParameter("Text to print", "");
    }

    public override void OnStart(IAiContext context)
    {
        var agent = context.Agent as AgentMono;

        // Printing to the console
        Debug.Log(agent.name + ": Hi");
    }
    // The OnGoing method is called on every consecutive selection
    public override void OnGoing(IAiContext context)
    {
        // Read the value of the parameter defined above. The parameterName is case sensitive
        var textFromParameter = ParameterContainer.GetParamString("Text to print").Value;

        // Get the agent from the Context and cast him as AgentMono
        // Don't think to much about the casting to AgentMono, just do it ;)
        var agent = context.Agent as AgentMono;

        // Printing to the console
        Debug.Log(agent.name + ": " + textFromParameter);
    }

    // The OnEnd method is called, when the decision was selected last evaluation, but not this evaluation
    public override void OnEnd(IAiContext context)
    {
        var agent = context.Agent as AgentMono;

        // Printing to the console
        Debug.Log(agent.name + ": Bye");
    }
}
