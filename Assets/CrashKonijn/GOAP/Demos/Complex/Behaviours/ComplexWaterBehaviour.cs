using System;
using System.Collections;
using System.Linq;
using Demos.Complex.Classes.Items;
using Demos.Complex.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Demos.Complex.Behaviours
{
    public class ComplexWaterBehaviour : MonoBehaviour
    {
        private Water water;
        private ItemCollection itemCollection;
        private ItemFactory itemFactory;

        private void Awake()
        {
            this.itemCollection = FindObjectOfType<ItemCollection>();
            this.itemFactory = FindObjectOfType<ItemFactory>();
        }

        private void Start()
        {
            this.DropWater();
        }

        private void Update()
        {
            if (this.water == null)
                return;
            
            if (!this.water.IsHeld)
                return;

            var count = this.itemCollection.All().Count(x => x is IDrinkable);

            if (count > 4)
                return;
            
            this.water = null;
            this.StartCoroutine(this.GrowWater());
        }

        private IEnumerator GrowWater()
        {
            yield return new WaitForSeconds(10f);
            this.DropWater();
        }

        private void DropWater()
        {
            this.water = this.itemFactory.Instantiate<Water>();
            this.water.transform.position = this.GetRandomPosition();
        }
        
        private Vector3 GetRandomPosition()
        {
            var pos = Random.insideUnitCircle.normalized * Random.Range(1f, 2f);

            return this.transform.position + new Vector3(pos.x, 0f, pos.y);
        }
    }
}