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
        private HumanBT hBT;
        string filename = "";
        public bool goap;
        public bool bt;
        public  float timer = 0;
        public string name;
        public string currState;

        private void Awake()
        {
            this.text = this.GetComponentInChildren<TextMeshProUGUI>();
            this.agent = this.GetComponent<AgentBehaviour>();
            if(bt){this.hBT = this.GetComponent<HumanBT>();}
        }

        void Start() {
            //Directory.CreateDirectory(Application.dataPath + "/Log");
            if(goap) {
                filename = Application.dataPath + "/Log/GOAPAgents.csv";
            }
            if(bt) {
                filename = Application.dataPath + "/Log/BTAgents.csv";
            }
            this.name = transform.name;
        }

        private void Update()
        {
            if(goap){
                if(currState == null) currState = this.agent.CurrentAction.GetType().GetGenericTypeName();
                if(!(this.agent.CurrentAction is null)){
                    if(currState.Equals(this.agent.CurrentAction.GetType().GetGenericTypeName())) {
                        timer += Time.deltaTime;
                    } else {
                        WriteCSV(name, timer, currState);
                        timer = 0.0f;
                        currState = this.agent.CurrentAction.GetType().GetGenericTypeName();
                    }
                }
            }
            if(bt) {
                if(currState == null) {currState = hBT.currentAction;}
                if(currState.Equals(hBT.currentAction)) {
                    timer += Time.deltaTime;
                } else {
                    WriteCSV(name, timer, currState);
                    timer = 0.0f;
                    currState = hBT.currentAction;
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
            string comma = ",";
            state = state.Replace(",", string.Empty);
            TextWriter tw = new StreamWriter(filename, true);
            tw.WriteLine(agent + ", " + state + ", " + totalTime);
            tw.Close();
            // Debug.Log(agent + " " + totalTime + " " + state);
            return;
        }

        public void PrintAtEnd(string agent, float totalTime, string state) {
            string comma = ",";
            state = state.Replace(",", string.Empty);
            TextWriter tw = new StreamWriter(filename, true);
            tw.WriteLine(agent + ", " + state + " (Final)," + totalTime);
            tw.Close();
        }
    }
}