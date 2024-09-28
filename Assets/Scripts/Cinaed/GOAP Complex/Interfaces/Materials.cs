namespace Cinaed.GOAP.Complex.Interfaces
{
    public abstract class MaterialBase { public string name; }

    public class Wood : MaterialBase { public string name = "wood"; }
    public class Water : MaterialBase { public string name = "water"; }
    public class Metal : MaterialBase { public string name = "metal"; }
    public class Stone : MaterialBase { public string name = "stone"; }
    public class Food : MaterialBase { public string name = "food"; }
    public class Population : MaterialBase { public string name = "population"; }
}