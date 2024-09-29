using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityAI
{
    [CreateAssetMenu(fileName = "IdleAction", menuName = "UtilityAI/Actions/IdleAction",order = 0)]
    public class IdleAction : AIAction
    {
        public override void Execute(Context context)
        {
            context.brain.DetermineGoal(9);
            
        }
    }
}
