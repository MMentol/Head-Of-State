using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes.Validators;
using CrashKonijn.Goap.Enums;
using TMPro;
using System.IO;
using UnityEngine;

namespace Cinaed
{
    public class TextUpdate : MonoBehaviour
    {
        private TextMeshProUGUI text;
        private AgentBehaviour agent;
        public string currState;
        string filename = "";
        public bool goap;
        public bool bt;
        public bool idle;
        float timer = 0;

        private void Awake()
        {
            this.text = this.GetComponentInChildren<TextMeshProUGUI>();
            this.agent = this.GetComponent<AgentBehaviour>();
        }

        void Start() {
            //Directory.CreateDirectory(Application.dataPath + "/Log");
            if(goap) {
                filename = Application.dataPath + "/Log/GOAPAgents.csv";
                TextWriter tw = new StreamWriter(filename, false);
                tw.WriteLine("Agent Name, Action, Time in Act (In Seconds)");
                tw.Close();
                Debug.Log("GOAPAgent CSV CREATED");
            }
            if(bt) {
                filename = Application.dataPath + "/Log/BTAgents.csv";
                TextWriter tw = new StreamWriter(filename, false);
                tw.WriteLine("Agent Name, Action, Time in Act (In Seconds)");
                tw.Close();
                Debug.Log("BTAgent CSV CREATED");
            }
            string name = agent.transform.name;
        }

        private void Update()
        {
            this.text.text = this.GetText();
            if(goap){
                if(currState == null) currState = this.agent.CurrentGoal.GetType().GetGenericTypeName();
                if(currState.Equals(this.agent.CurrentGoal.GetType().GetGenericTypeName())) {
                    timer += Time.deltaTime;
                } else {
                    WriteCSV(name, timer, currState);
                    timer = 0.0f;
                    currState = this.agent.CurrentGoal.GetType().GetGenericTypeName();
                }
            }
        }

        private string GetText()
        {
            if (this.agent.CurrentAction is null)
                return "Idle";

            return $"{this.agent.CurrentGoal.GetType().GetGenericTypeName()}\n{this.agent.CurrentAction.GetType().GetGenericTypeName()}\n{this.agent.State}";
        }



        public void WriteCSV(string agent, float totalTime, string state) {
            TextWriter tw = new StreamWriter(filename, true);
            tw.WriteLine(agent + " ," + state + " ," + totalTime);
            tw.Close();
            Debug.Log(agent + " " + totalTime + " " + state);
            return;
        }
    }
}