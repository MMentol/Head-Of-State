using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using Items;
using Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cinaed.GOAP.Complex.WorldKeySensors
{
    public class ItemAmountSensor<TCraftable> : LocalWorldSensorBase
        where TCraftable : ItemBase
    {
        public override void Created() { }

        public override void Update() { }

        public override SenseValue Sense(IMonoAgent agent, IComponentReference references)
        {
            List<ItemBase> list = agent.GetComponent<Inventory>().items;
            ItemBase itemType = CraftableItemCollection.Instance.gameObject.GetComponent<TCraftable>();
            int count = list.Where(item => item.ItemName == itemType.ItemName).Count();
            //Debug.Log($"{itemType.ItemName} : {count}");
            return count;
        }


    }
}