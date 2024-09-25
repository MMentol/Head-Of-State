using Cinaed.GOAP.Complex.Interfaces;
using CrashKonijn.Goap.Behaviours;

namespace Cinaed.GOAP.Complex.WorldKeys
{
    public class AgentMaterialAmount<T> : WorldKeyBase
        where T : MaterialBase { }

    public class PlayerMaterialAmount<T> : WorldKeyBase
        where T : MaterialBase
    { }

    public class PlayerMaterialPercentage<T> : WorldKeyBase
        where T : MaterialBase
    { }

    public class WoodAmount : WorldKeyBase { }
    public class StoneAmount : WorldKeyBase { }
    public class MetalAmount : WorldKeyBase { }
    public class WaterAmount : WorldKeyBase { }
    public class FoodAmount : WorldKeyBase { }
}