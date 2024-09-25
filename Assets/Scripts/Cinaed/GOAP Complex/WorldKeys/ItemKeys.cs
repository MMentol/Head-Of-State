using CrashKonijn.Goap.Behaviours;
using Items;

namespace Cinaed.GOAP.Complex.WorldKeys
{
    public class ItemAmount<TCraftable> : WorldKeyBase
        where TCraftable : ItemBase { }
    public class IsItemCraftable<TCraftable> : WorldKeyBase 
        where TCraftable : ItemBase { }
}
