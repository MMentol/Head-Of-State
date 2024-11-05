using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class ObjectivesTracker : MonoBehaviour
{
    string filename = "";
    [Header ("Algorithm Used")]
    [SerializeField] bool goap;
    [SerializeField] bool bt;

    [Header("Material Data Storage")]
    [SerializeField] MaterialDataStorage mds = null;
    [SerializeField] UIScript uis = null;

    [Header("Objectives Window")]
    [SerializeField] TMP_Text MaterialObj;
    [SerializeField] TMP_Text PopulationObj;
    [SerializeField] TMP_Text ObjCountText;


    [Header("Victory Screen")]
    [SerializeField] TMP_Text victoryText;

    public float Pop, Wood, Stone, Metal;
    public int objCount = 0;

    private float goalPop, goalWood, goalStone, goalMetal;

    void Awake() {
        MaterialObj.text = "";
        PopulationObj.text = "";
        ObjCountText.text = "";
    }
    void Start()
    {
        Directory.CreateDirectory(Application.dataPath + "/Log");
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
        UpdateObjectiveText();
        bool printed = false;
        ObjCountText.text = "Objective " + (Mathf.Clamp(1 + objCount, 0, 30));
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
        if(Pop >= 10 && Metal >= 5 && objCount == 2) {
            WriteCSV(3, uis.getTime(uis.time));
            objCount++;
        }
        // Objective 4
        if(Pop >= 25 && Wood >= 60 && Stone >= 50 && objCount == 3) {
            WriteCSV(4, uis.getTime(uis.time));
            objCount++;
        }
        // Objective 5
        if(Pop >= 50 && Stone >= 80 && Metal >= 30 && objCount == 4){
            WriteCSV(5, uis.getTime(uis.time));
            objCount++;
        }
        // Objective 6
        if(Wood >= 300 && Stone >= 150 && Metal >= 80 && objCount == 5){
            WriteCSV(6, uis.getTime(uis.time));
            objCount++;
        }
        // Objective 7
        if(Pop >= 70 && Stone >= 210 && Metal >= 100 && objCount == 6){
            WriteCSV(7, uis.getTime(uis.time));
            objCount++;
        }
        // Objective 8
        if(Pop >= 80 && Wood >= 600 && Stone >= 300 && Metal >= 130 && objCount == 7) {
            WriteCSV(8, uis.getTime(uis.time));
            objCount++;
        }
        // Objective 9
        if(Wood >= 800 && Stone >= 750 && Metal >= 180 && objCount == 8) {
            WriteCSV(9, uis.getTime(uis.time));
            objCount++;
        }
        // Objective 10
        if(Pop >= 100 && objCount == 9) {
            WriteCSV(10, uis.getTime(uis.time));
            objCount++;
        }
        // When Times Up
        if(uis.time >= 2700) {
            TextWriter tw = new StreamWriter(filename, true);
            tw.WriteLine("Time is Up,Completed " + objCount + " objectives");
            tw.Close();
            victoryText.text ="Congratulations! You completed a total of " + objCount + " objectives.";
            uis.time = 0;
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
    void UpdateObjectiveText() {
        switch(objCount){
            case 0:
            MaterialObj.text = "Collect\n" + (15-Wood) + " More Wood";
            break;

            case 1:
            MaterialObj.text = "Collect\n" + (Mathf.Clamp(30-Wood, 0, 30)) + " More Wood\n" 
                                + (Mathf.Clamp(10-Stone, 0, 10)) + " More Stone";
            PopulationObj.text = "Reach\n5 Population";
            break;

            case 2:
            MaterialObj.text = "Collect\n" + ((Mathf.Clamp(5-Metal, 0, 5))) + " Metal";
            PopulationObj.text = "Reach\n10 Population";
            break;

            case 3:
            MaterialObj.text = "Collect\n" + ((Mathf.Clamp(60-Wood, 0, 60))) + " Wood\n"
                                + ((Mathf.Clamp(50-Stone, 0, 50))) + " Stone";
            PopulationObj.text = "Reach\n25 Population";
            break;

            case 4:
            MaterialObj.text = "Collect\n" + ((Mathf.Clamp(80-Stone, 0, 80))) + " Stone\n"
                                + ((Mathf.Clamp(30-Metal, 0, 30))) + " Metal";
            PopulationObj.text = "Reach\n50 Population";
            break;

            case 5:
            MaterialObj.text = "Collect\n" + ((Mathf.Clamp(300-Wood, 0, 300))) + " Wood\n"
                                + ((Mathf.Clamp(150-Stone, 0, 150))) + " Stone\n"
                                + ((Mathf.Clamp(80-Metal, 0, 80))) + " Metal";
            PopulationObj.text = "";
            break;

            case 6:
            MaterialObj.text = "Collect\n" + ((Mathf.Clamp(210-Stone, 0, 210))) + " Stone\n"
                                + ((Mathf.Clamp(100-Metal, 0, 100))) + " Metal";
            PopulationObj.text = "Reach\n70 Population";
            break;

            case 7:
            MaterialObj.text = "Collect\n" + ((Mathf.Clamp(600-Wood, 0, 600))) + " Wood\n"
                                + ((Mathf.Clamp(300-Stone, 0, 300))) + " Stone\n"
                                + ((Mathf.Clamp(130-Metal, 0, 130))) + " Metal";
            PopulationObj.text = "Reach\n80 Population";
            break;

            case 8:
            MaterialObj.text = "Collect\n" + ((Mathf.Clamp(800-Wood, 0, 800))) + " Wood\n"
                                + ((Mathf.Clamp(750-Stone, 0, 750))) + " Stone\n"
                                + ((Mathf.Clamp(180-Metal, 0, 180))) + " Metal";
            PopulationObj.text = "";
            break;

            case 9:
            MaterialObj.text = "";
            PopulationObj.text = "Reach\n100 Population";
            break;

            default:
            MaterialObj.text = "";
            PopulationObj.text = "";
            break;
        }
    }
}
