using Cinaed.GOAP.Complex.Interfaces;
using CrashKonijn.Goap.Behaviours;

namespace Cinaed.GOAP.Complex.WorldKeys
{
    public class InventorySpaceAmount : WorldKeyBase { }
    public class StorageSpaceAmount<TMaterial> : WorldKeyBase
        where TMaterial : MaterialBase { }
}