using System.Collections;
using System.Collections.Generic;
using BehaviorTree;

public class HumanBT : Tree
{
    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {

            new Sequence(new List<Node>
            {
                new checkFoodTask(transform),
                new lookFoodTask(transform),
                new walkToFoodTask(transform),
                new getFoodTask(transform)
            }),


            new Sequence(new List<Node>
            {
                new checkWaterTask(transform),
                new lookWaterTask(transform),
                new walkToWaterTask(transform),
                new getWaterTask(transform)
            }),

            new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new checkForWoodStorageTask(transform),
                    new checkWoodResourceTask(transform),
                    new lookForWoodResourceTask(transform),
                    new walkToWoodResourceTask(transform),
                    new getWoodResourceTask(transform)
                }),

                new Sequence(new List<Node>
                {
                    new checkForStoneStorageTask(transform),
                    new checkStoneResourceTask(transform),
                    new lookForStoneResourceTask(transform),
                    new walkToStoneResourceTask(transform),
                    new getStoneResourceTask(transform)
                }),

                new Sequence(new List<Node>
                {
                    new checkForMetalStorageTask(transform),
                    new checkMetalResourceTask(transform),
                    new lookForMetalResourceTask(transform),
                    new walkToMetalResourceTask(transform),
                    new getMetalResourceTask(transform)
                })
            }),

            new Sequence(new List<Node>
            {
                new checkHeatTask(transform),
                new lookForHomeTask(transform),
                new walkToHomeTask(transform),
                new sleepTask(transform)
            }),

            new Sequence(new List<Node>
            {
                new isInHomeTask(transform),
                new statsFulfilledTask(transform),
                new reproduceTask(transform)
            }),
            

            new WanderBT(transform)
            //new Sequence(new List<Node>
            //{
            //    new CheckEnemyInAttackRange(transform),
            //    new TaskAttack(transform),
            //}),
            //new Sequence(new List<Node>
            //{
            //    new CheckEnemyInFOVRange(transform),
            //    new TaskGoToTarget(transform),
            //}),
            //new TaskPatrol(transform, waypoints),


        });

        return root;
    }
}
