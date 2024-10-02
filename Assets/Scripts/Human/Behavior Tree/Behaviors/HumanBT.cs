using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using Scripts;

public class HumanBT : Tree
{

   

    protected override Node SetupTree()
    {

        Node root = new Selector(new List<Node>
        {
            new Selector(new List<Node>
            {
                new getFoodTask(transform),
                new Sequence(new List<Node>
                {
                    new checkFoodTask(transform),
                    new lookFoodTask(transform),
                    new walkToFoodTask(transform),

                }),

            }),

            new Selector(new List<Node>
            {
                new getWaterTask(transform),
                new Sequence(new List<Node>
                {
                    new checkWaterTask(transform),
                    new lookWaterTask(transform),
                    new walkToWaterTask(transform),

                }),

            }),
            
            new Selector(new List<Node>
            {
                
                
                new Selector(new List<Node>  {

                    new depositMetalTask(transform),
                    new Sequence(new List<Node>
                    {
                        new checkAgeTask(transform),
                        new getMetalResourceTask(transform),
                        new walkToMetalStorageTask(transform),

                    }),
                new Sequence(new List<Node>
                    {
                        //new checkMetalResourceTask(transform),
                        new checkAgeTask(transform),
                        new checkForMetalStorageTask(transform),
                        new lookForMetalResourceTask(transform),
                        new walkToMetalResourceTask(transform),

                    }),
                }),
                new Selector(new List<Node>  {

                    new depositStoneTask(transform),
                    new Sequence(new List<Node>
                    {
                        new checkAgeTask(transform),
                        new getStoneResourceTask(transform),
                        new walkToStoneStorageTask(transform),

                    }),
                new Sequence(new List<Node>
                    {
                        //new checkStoneResourceTask(transform),
                        new checkAgeTask(transform),
                        new checkForStoneStorageTask(transform),
                        new lookForStoneResourceTask(transform),
                        new walkToStoneResourceTask(transform),

                    }),
                }),
                new Selector(new List<Node>  {

                    new depositWoodTask(transform),
                    new Sequence(new List<Node>
                    {
                        new checkAgeTask(transform),
                        new getWoodResourceTask(transform),
                        new walkToWoodStorageTask(transform),

                    }),
                new Sequence(new List<Node>
                    {
                        //new checkWoodResourceTask(transform),
                        new checkAgeTask(transform),
                        new checkForWoodStorageTask(transform),
                        new lookForWoodResourceTask(transform),
                        new walkToWoodResourceTask(transform),

                    }),
                }),
            }),
            new Selector(new List<Node>
            {
                new Sequence(new List<Node>
            {
                    new checkAgeTask(transform),
                new statsFulfilledTask(transform),
                new reproduceTask(transform)
            }),
                new sleepTask(transform),
                new Sequence(new List<Node>
                {
                    new checkHeatAndEnergyTask(transform),
                    new lookForHomeTask(transform),
                    new walkToHomeTask(transform),

                }),

            


            new WanderBT(transform)

            }),


        });

        return root;
    }
}
