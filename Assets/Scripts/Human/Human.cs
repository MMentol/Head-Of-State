using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{

    private string _name;
    private HumanStats stats;
    public float ageLimit;


    // Start is called before the first frame update
    void Start()
    {
        stats = gameObject.GetComponent<HumanStats>();
    }

    // Update is called once per frame
    void Update()
    {
        Die();
    }

    public void Die()
    {
        //dies
        if(stats._age >= ageLimit)
        {
            Destroy(gameObject);
            MaterialDataStorage.Instance.Census();
            Debug.Log(_name + " died rip in peace");
        }

    }
}
