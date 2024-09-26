using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityAI
{
    [CreateAssetMenu(menuName = "UtilityAI/Consideration/Constant")]
    public class ConstantConsideration : Consideration
    {
        public float value;

        public override float Evaluate(Context context) => value;

    }
}
