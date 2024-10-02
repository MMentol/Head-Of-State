using Cinaed.GOAP.Complex;
using Cinaed.GOAP.Complex.Factories;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public List<HumanStats> PeopleInside;
    public int Capacity = 2;
    public AgentSpawner Spawner;
    public HouseActivitySettings HouseSettings;

    private void Awake()
    {
        this.Spawner = FindObjectOfType<AgentSpawner>();
        this.HouseSettings = FindObjectOfType<HouseActivitySettings>();
    }

    public void UpdateCurrentHouse(HumanStats human)
    {
        //Add to List
        Debug.Log("Added to list");
        human.currentHouse = this;
        PeopleInside.Add(human);
    }

    public bool EnterHouse(HumanStats human)
    {
        if (IsInside(human))
            return false; //Already inside

        if (PeopleInside.Count >= Capacity)
            return false; //Full House

        //Enter house
        human.insideHouse = 1;
        human.GetComponentInChildren<SpriteRenderer>().enabled = false;

        return true;
    }

    public bool LeaveHouse(HumanStats human)
    {
        if (!IsInside(human))
            return false; //Not inside

        //Leave house
        human.currentHouse = null;
        PeopleInside.Remove(human);
        human.insideHouse = 0;
        human.GetComponentInChildren<SpriteRenderer>().enabled = true;
        return true;
    }

    public bool IsInside(HumanStats human)
    {
        return PeopleInside.Contains(human);
    }

    public bool IsHappy(HumanStats human)
    {
        return human._happiness >= HouseSettings.RequiredHappiness;
    }

    public bool MakeNewHuman(HumanStats h1, HumanStats h2)
    {
        //No one inside
        if (this.PeopleInside.Count < 2)
            return false;

        if (!IsInside(h1) || !IsInside(h2))
            return false;

        //Check if both are happy
        float happinessRequired = HouseSettings.RequiredHappiness; //JOSEP SET VALUE
        if (h1._happiness >= happinessRequired && h2._happiness >= happinessRequired)
            Spawner.CreateNewHuman(AgentIds.Human, Random.ColorHSV(), h1.transform.position);

        return true;
    }

    public void NapTime(HumanStats human)
    {
        if (!IsInside(human))
            return; //Not inside

        human._energy += HouseSettings.RestingEnergyBenefit; //JOSEP SET VALUE
        if (human._energy > 100) { human._energy = 100; }
    }
}
