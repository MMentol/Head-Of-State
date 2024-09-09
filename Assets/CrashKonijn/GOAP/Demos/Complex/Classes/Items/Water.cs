using Demos.Complex.Behaviours;
using Demos.Complex.Interfaces;

namespace Demos.Complex.Classes.Items
{
    public class Water : ItemBase, IDrinkable
    {
        public float ThirstValue { get; set; } = 200f;
    }
}