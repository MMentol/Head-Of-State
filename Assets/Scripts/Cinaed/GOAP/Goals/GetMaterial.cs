using CrashKonijn.Goap.Behaviours;
using Cinaed.GOAP.Interfaces;

namespace Cinaed.GOAP.Goals
{
    public class GetMaterial<TMaterial> : GoalBase 
    where TMaterial : IMaterial
    {}
}

