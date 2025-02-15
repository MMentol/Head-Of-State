﻿using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using UnityEngine;

namespace Cinaed.GOAP.Complex.WorldKeySensors
{
    public class ThirstSensor : LocalWorldSensorBase
    {
        public override void Created() { }
        public override void Update() { }

        public override SenseValue Sense(IMonoAgent agent, IComponentReference references)
        {
            return Mathf.RoundToInt(agent.GetComponent<HumanStats>()._thirst);
        }

    }

    public class HungerSensor : LocalWorldSensorBase
    {
        public override void Created() { }
        public override void Update() { }

        public override SenseValue Sense(IMonoAgent agent, IComponentReference references)
        {
            return Mathf.RoundToInt(agent.GetComponent<HumanStats>()._hunger);
        }

    }

    public class EnergySensor : LocalWorldSensorBase
    {
        public override void Created() { }
        public override void Update() { }

        public override SenseValue Sense(IMonoAgent agent, IComponentReference references)
        {
            return Mathf.RoundToInt(agent.GetComponent<HumanStats>()._energy);
        }

    }

    public class HappinessSensor : LocalWorldSensorBase
    {
        public override void Created() { }
        public override void Update() { }

        public override SenseValue Sense(IMonoAgent agent, IComponentReference references)
        {
            return Mathf.RoundToInt(agent.GetComponent<HumanStats>()._happiness);
        }

    }
    
    public class BreedCooldownSensor : LocalWorldSensorBase
    {
        public override void Created() { }
        public override void Update() { }

        public override SenseValue Sense(IMonoAgent agent, IComponentReference references)
        {
            return Mathf.RoundToInt(agent.GetComponent<HumanStats>().breedCooldown);
        }

    }
}