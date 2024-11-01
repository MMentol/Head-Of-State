using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes.Validators;
using CrashKonijn.Goap.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAPBehaveScript : MonoBehaviour
{
    public Animator anim;
    private AgentBehaviour agent;
    public string currState;
    string[] acts = {"isEating", "hasWater", 
    "hasPickaxe", "hasAxe", 
    "hasFood", "hasWater"};

    void Awake() {
        this.agent = this.GetComponent<AgentBehaviour>();
    }
    
    void Update()
    {
        if(currState == null) currState = this.agent.CurrentAction.GetType().GetGenericTypeName();
        if(!(this.agent.CurrentAction is null)) {
            if(currState.Equals(this.agent.CurrentGoal.GetType().GetGenericTypeName())) {} 
            else {
                currState = this.agent.CurrentGoal.GetType().GetGenericTypeName();
                foreach(string goal in acts) {
                    anim.SetBool(goal, false);
                }
            }
        }
        switch(currState) {
            case "EatGoal":
                anim.SetBool("isEating", true);
                break;
            case "DrinkGoal":
                anim.SetBool("hasWater", true);
                break;
            case "GatherMaterialGoal<Metal>":
                anim.SetBool("hasPickaxe", true);
                break;
            case "GatherMaterialGoal<Stone>":
                anim.SetBool("hasPickaxe", true);
                break;
            case "GatherMaterialGoal<Wood>":
                anim.SetBool("hasAxe", true);
                break;
            case "GatherMaterialGoal<Food>":
                anim.SetBool("hasFood", true);
                break;
            case "GatherMaterialGoal<Water>":
                anim.SetBool("hasWater", true);
                break;
            default:
                break;
        }
    }
}
