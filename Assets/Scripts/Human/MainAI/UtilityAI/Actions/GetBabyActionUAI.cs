using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityAI
{
    [CreateAssetMenu( menuName = "UtilityAI/Actions/GetBabyAction")]
    public class GetBabyActionUAI : AIAction
    {
        public override void Execute(Context context)
        {
            context.brain.DetermineGoal(0);
            
        }
    }
}
