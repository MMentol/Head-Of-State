using System.Collections.Generic;

namespace BehaviorTree
{
    public class RandomizedSelector : Node
    {
        public RandomizedSelector() : base() { }
        public RandomizedSelector(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            int randomSelected = UnityEngine.Random.Range(0, children.Count);

            
            
           
                switch (children[randomSelected].Evaluate())
                {
                    case NodeState.FAILURE:
                    break;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;

                        return state;
                    default:
                        break;
                }
            

            state = NodeState.FAILURE;
            return state;
        }

    }

}
