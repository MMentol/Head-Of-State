using Cinaed.GOAP.Goals;
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes.Builders;
using CrashKonijn.Goap.Configs.Interfaces;
using Demos.Shared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cinaed.GOAP.Factories.NPC
{
    public class ColonistGoapSetConfigFactory
        : GoapSetFactoryBase
    {
        public override IGoapSetConfig Create()
        {
            var builder = new GoapSetBuilder("Colonist");

            //Debugger
            builder.SetAgentDebugger<AgentDebugger>();

            //Goals

            return builder.Build();
        }
    }
}
