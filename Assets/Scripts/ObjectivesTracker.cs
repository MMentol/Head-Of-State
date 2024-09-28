using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ObjectivesTracker : MonoBehaviour
{
    string filename = "";
    [Header ("Algorithm Used")]
    [SerializeField] bool goap = true;
    [SerializeField] bool bt = false;

    [Header("Material Data Storage")]
    [SerializeField] MaterialDataStorage mds = null;
    [SerializeField] UIScript uis = null;

    float Pop, Wood, Stone, Metal;
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
        if(bt) {
            filename = Application.dataPath + "/BTRun.csv";
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("Objective No., Completion Time");
            tw.Close();
        }
        Pop = mds.Population;
        Wood = mds.totalWood;
        Stone = mds.totalStone;
        Metal = mds.totalMetal;
    }

    // Update is called once per frame
    void Update()
    {
        int objCount = 0;
        // Objective 1
        if(Wood == 10) {
            WriteCSV(1, uis.getTime(uis.time));
            objCount++;
        }
        // Objective 2
        if(Pop >= 5 && Wood >= 30 && Stone >= 10) { 
            WriteCSV(2, uis.getTime(uis.time));
            objCount++;
        }
        // Objective 3
        if(Pop >= 10 && mds.Metal >= 5) {
            WriteCSV(3, uis.getTime(uis.time));
            objCount++;
        }
        // Objective 4
        if(Pop >= 14 && Wood >= 60 && Stone >= 20) {
            WriteCSV(4, uis.getTime(uis.time));
            objCount++;
        }
        // Objective 5
        if(Stone >= 40 && Metal >= 30){
            WriteCSV(5, uis.getTime(uis.time));
            objCount++;
        }
        // Objective 6
        if(Wood >= 100 && Metal >= 50){
            WriteCSV(6, uis.getTime(uis.time));
            objCount++;
        }
        // Objective 7
        if(Stone >= 100 && Metal >= 70){
            WriteCSV(7, uis.getTime(uis.time));
            objCount++;
        }
        // Objective 8
        if(Wood >= 150 && Stone >= 120) {
            WriteCSV(8, uis.getTime(uis.time));
            objCount++;
        }
        // Objective 9
        if(Wood >= 150 && Stone >= 120 && Metal >= 100) {
            WriteCSV(9, uis.getTime(uis.time));
            objCount++;
        }
        // Objective 10
        if(Pop >= 60) {
            WriteCSV(10, uis.getTime(uis.time));
            objCount++;
        }
        // When Times Up
        if(uis.time == 2700) {
            TextWriter tw = new StreamWriter(filename, true);
            tw.WriteLine("Out of Time" + "Completed " + objCount + " objectives");
            tw.Close();
        }
    }

    public void WriteCSV(int objNo, string time) {
        TextWriter tw = new StreamWriter(filename, true);
            tw.WriteLine(objNo + " ," + time);
            tw.Close();
    }

}
