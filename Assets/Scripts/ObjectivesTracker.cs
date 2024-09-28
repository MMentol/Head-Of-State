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
    }

    // Update is called once per frame
    void Update()
    {
        int obj = 0;
        if(mds.Wood == 11) {
            WriteCSV(1, uis.getTime(uis.time));
        }
    }

    public void WriteCSV(int objNo, string time) {
        TextWriter tw = new StreamWriter(filename, true);
            tw.WriteLine(objNo + " ," + time);
            tw.Close();
    }

}
