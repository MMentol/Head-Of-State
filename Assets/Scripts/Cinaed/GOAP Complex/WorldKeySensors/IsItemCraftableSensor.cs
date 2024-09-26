using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using Items;
using Scripts;
using System;
using UnityEngine;

namespace Cinaed.GOAP.Complex.WorldKeys
{
    public class IsItemCraftableSensor<TCraftable> : LocalWorldSensorBase
        where TCraftable : ItemBase
    {
        public override void Created() { }

        public override void Update() { }

        public override SenseValue Sense(IMonoAgent agent, IComponentReference references)
        {
            ItemBase item = GameObject.FindObjectOfType<TCraftable>();
            if (item.IsCraftable(agent.GetComponent<Inventory>()))
                return true;
            return false;
        }
    }

}