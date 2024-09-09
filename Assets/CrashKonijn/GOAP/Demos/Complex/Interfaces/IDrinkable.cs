namespace Demos.Complex.Interfaces
{
    public interface IDrinkable : IHoldable
    {
        public float ThirstValue { get; set; }
    }
}