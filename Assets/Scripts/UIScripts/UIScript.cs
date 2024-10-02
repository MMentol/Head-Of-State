using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [Header("Gameplay Menus")]
    [SerializeField]
    GameObject pauseMenu = null;
    [SerializeField]
    GameObject buildMenu = null;
    [SerializeField]
    GameObject overlayMenu = null;
    [SerializeField]
    TMP_Text timerText = null;

    [Header("Main Menu Items")]
    [SerializeField]
    GameObject stagePick = null;
    [SerializeField]
    GameObject mainMenu = null;
    [SerializeField]
    GameObject instructions = null;
    [SerializeField]
    GameObject credits = null;

    public float time = 0f;
    public int clock = 0;
    public float currTime = 0;


    public HumanStats[] allHumanStats;

    // Start is called before the first frame update
    bool pause = false;
    bool x2 = false;
    bool x4 = false;
    void Start()
    {
        allHumanStats = GameObject.FindObjectsOfType<HumanStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
           // OpenBuild();
        }
        if(SceneManager.GetActiveScene().name.Equals("Main Menu")) {
            pause = false;
            if(Input.GetKeyDown(KeyCode.Escape)) {
                mainMenu.SetActive(true);
                stagePick.SetActive(false);
                instructions.SetActive(false);
                credits.SetActive(false);
            }
        }
        else if(Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }
        time += Time.deltaTime;
        if (!SceneManager.GetActiveScene().name.Equals("Main Menu"))
            timerText.text = getTime(time);
        float newTime = Mathf.Floor(time);
        if (Mathf.Floor(time) % 60 == 0)
        {
            Debug.Log("time:"+ currTime + "old time: " + newTime);
            if(currTime!= newTime)
            {
                currTime = newTime;
                Debug.Log("Age Older");
                allHumanStats = GameObject.FindObjectsOfType<HumanStats>();
                foreach (HumanStats human in allHumanStats)
                {
                    Debug.Log(human + " is dying");
                    human.getOlder();
                }
            }
        }
    }

    void PauseGame(bool stop) {
        if(stop) {
            pauseMenu.SetActive(pause);
            overlayMenu.SetActive(!pause);
            Time.timeScale = 0;
        } else {
            pauseMenu.SetActive(pause);
            overlayMenu.SetActive(!pause);
            Time.timeScale = 1;
        }
    }

    public void TogglePause() {
        if(pause) {
            pause = false;
        } else {
            pause = true;
        }
        PauseGame(pause);
    }
    public void Unpause() {
        pause = false;
        PauseGame(pause);
    }

    public void toggleGo2() {
        if(!x2) {
            x2 = true;
            if(x4) {
                x4 = false;
            }
            Time.timeScale = 2;
        } else {
            x2 = false;
            Time.timeScale = 1;
        }
    }

    public void toggleGo4() {
        if(!x4) {
            x4 = true;
            if(x2) {
                x2 = false;
            }
            Time.timeScale = 4;
        } else {
            x4 = false;
            Time.timeScale = 1;
        }
    }
    public string getTime(float time) {
        float intTime = time;
        int minutes = Mathf.FloorToInt(intTime / 60);
        int seconds = Mathf.FloorToInt(intTime % 60);
        float fraction = time * 1000;
        fraction = fraction % 1000;
        return string.Format ("{0:00}:{1:00}", minutes, seconds);
    }
    public void PickStage() {
        stagePick.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void OpenBuild() {
        buildMenu.SetActive(true);
    }

    public void CloseBuild() {
        buildMenu.SetActive(false);
    }

    public void startGOAP() {
        SceneManager.LoadScene("UITesting");
    }
    
    public void startBT() {
        SceneManager.LoadScene("BTBaseScene");
    }

    public void goMainMenu() {
        SceneManager.LoadScene("Main Menu");
    }

    public void restartLevel(){
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
