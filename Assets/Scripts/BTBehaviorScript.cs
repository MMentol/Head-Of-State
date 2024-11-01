using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTBehaviorScript : MonoBehaviour
{
    public Animator anim;
    private HumanBT agent;
    public string currState;
    string[] acts = {"isEating", "hasWater", 
    "hasPickaxe", "hasAxe", 
    "hasFood", "hasWater"};

    void Awake() {
        this.agent = this.agent = this.GetComponent<HumanBT>();
    }
    
    void Update()
    {
        if(currState == null) currState = this.agent.currentAction;
        if(!(this.agent.currentAction is null)) {
            if(currState.Equals(this.agent.currentAction)) {} 
            else {
                currState = this.agent.currentAction;
                foreach(string goal in acts) {
                    anim.SetBool(goal, false);
                }
            }
        }
        switch(currState) {
            case "getFood":
                anim.SetBool("isEating", true);
                break;
            case "getWater":
                anim.SetBool("hasWater", true);
                break;
            case "getMetal":
                anim.SetBool("hasPickaxe", true);
                break;
            case "depositMetal":
                anim.SetBool("hasPickaxe", true);
                break;
            case "walkToMetalResource":
                anim.SetBool("hasPickaxe", true);
                break;
            case "walkToMetalStorage":
                anim.SetBool("hasPickaxe", true);
                break;
            case "getStone":
                anim.SetBool("hasPickaxe", true);
                break;
            case "depositStone":
                anim.SetBool("hasPickaxe", true);
                break;
            case "walkToStoneResource":
                anim.SetBool("hasPickaxe", true);
                break;
            case "walkToStoneStorage":
                anim.SetBool("hasPickaxe", true);
                break;
            case "getWood":
                anim.SetBool("hasAxe", true);
                break;
            case "depositWood":
                anim.SetBool("hasAxe", true);
                break;
            case "walkToWoodResource":
                anim.SetBool("hasAxe", true);
                break;
            case "walkToWoodStorage":
                anim.SetBool("hasAxe", true);
                break;
            case "walkToFood":
                anim.SetBool("hasFood", true);
                break;
            case "walkToWater":
                anim.SetBool("hasWater", true);
                break;
            default:
                break;
        }
    }
}
