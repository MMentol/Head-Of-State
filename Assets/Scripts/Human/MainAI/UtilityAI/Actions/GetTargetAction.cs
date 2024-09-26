using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityAI
{
    [CreateAssetMenu( menuName = "UtilityAI/Actions/GetTargetAction")]
    public class GetTargetAction : AIAction
    {
        

        public override void Execute(Context context)
        {
            var targetGoal = targetTag;

            context.brain.DetermineGoal(int.Parse(targetGoal));
            
        }
    }
}
