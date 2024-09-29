using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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

    // Start is called before the first frame update
    bool pause = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            OpenBuild();
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
            if(pause) {
                pause = false;
            } else {
                pause = true;
            }
            PauseGame(pause);
        }
        time += Time.deltaTime;
        timerText.text = getTime(time);
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
    public void Unpause() {
        pause = false;
        PauseGame(pause);
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
}
