using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ObjectivesTracker : MonoBehaviour
{
    string filename = "";
    [Header ("Algorithm Used")]
    [SerializeField] bool goap;
    [SerializeField] bool bt;

    [Header("Material Data Storage")]
    [SerializeField] MaterialDataStorage mds = null;
    [SerializeField] UIScript uis = null;

    public float Pop, Wood, Stone, Metal;
    public int objCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(goap) {
            filename = Application.dataPath + "/Log/GOAPRun.csv";
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("Objective No., Completion Time");
            tw.Close();
            Debug.Log("GOAP CSV CREATED");
        }
        else if(bt) {
            filename = Application.dataPath + "/Log/BTRun.csv";
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("Objective No., Completion Time");
            tw.Close();
            Debug.Log("BT CSV CREATED");
        }
        Pop = mds.Population;
        Wood = mds.totalWood;
        Stone = mds.totalStone;
        Metal = mds.totalMetal;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateValues();
        bool printed = false;
        // Objective 1
        if(Wood >= 15 && objCount == 0) {
            WriteCSV(1, uis.getTime(uis.time));
            objCount++;
        }
        // Objective 2
        if(Pop >= 5 && Wood >= 30 && Stone >= 10 && objCount == 1) { 
            WriteCSV(2, uis.getTime(uis.time));
            objCount++;
        }
        // Objective 3
        if(Pop >= 10 && mds.Metal >= 5 && objCount == 2) {
            WriteCSV(3, uis.getTime(uis.time));
            objCount++;
        }
        // Objective 4
        if(Pop >= 14 && Wood >= 60 && Stone >= 20 && objCount == 3) {
            WriteCSV(4, uis.getTime(uis.time));
            objCount++;
        }
        // Objective 5
        if(Stone >= 80 && Metal >= 30 && objCount == 4){
            WriteCSV(5, uis.getTime(uis.time));
            objCount++;
        }
        // Objective 6
        if(Wood >= 130 && Metal >= 50 && objCount == 5){
            WriteCSV(6, uis.getTime(uis.time));
            objCount++;
        }
        // Objective 7
        if(Stone >= 120 && Metal >= 70 && objCount == 6){
            WriteCSV(7, uis.getTime(uis.time));
            objCount++;
        }
        // Objective 8
        if(Wood >= 150 && Stone >= 150 && objCount == 7) {
            WriteCSV(8, uis.getTime(uis.time));
            objCount++;
        }
        // Objective 9
        if(Wood >= 200 && Stone >= 200 && Metal >= 100 && objCount == 8) {
            WriteCSV(9, uis.getTime(uis.time));
            objCount++;
        }
        // Objective 10
        if(Pop >= 60 && objCount == 9) {
            WriteCSV(10, uis.getTime(uis.time));
            objCount++;
        }
        // When Times Up
        if(uis.time >= 2700) {
            TextWriter tw = new StreamWriter(filename, true);
            tw.WriteLine("Time is Up" + "Completed " + objCount + " objectives");
            tw.Close();
        }
    }

    public void WriteCSV(int objNo, string time) {
        TextWriter tw = new StreamWriter(filename, true);
        tw.WriteLine(objNo + " ," + time);
        tw.Close();
        Debug.Log(objNo + " ," + time);
        return;
    }
    void UpdateValues() {
        Pop = mds.Population;
        Wood = mds.totalWood;
        Stone = mds.totalStone;
        Metal = mds.totalMetal;
    }

}
