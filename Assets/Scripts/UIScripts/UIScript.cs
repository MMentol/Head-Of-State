using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIScript : MonoBehaviour
{
    [Header("Main Menu stuff")]
    [SerializeField]
    GameObject mainMenu = null;
    [SerializeField]
    GameObject stagePick = null;

    [Header("Ingame Overlays")]
    [SerializeField]
    GameObject pauseMenu = null;
    [SerializeField]
    GameObject buildMenu = null;
    [SerializeField]
    GameObject overlayMenu = null;

    // Start is called before the first frame update
    bool pause = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name.Equals("Main Menu")) {
            pause = false;
            if(Input.GetKeyDown(KeyCode.Escape)) {
                mainMenu.SetActive(true);
                stagePick.SetActive(false);
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(pause) {
                pause = false;
            } else {
                pause = true;
            }
        }
        PauseGame(pause);
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

    public void OpenBuild() {
        buildMenu.SetActive(true);
    }

    public void CloseBuild() {
        buildMenu.SetActive(false);
    }
    
    public void StartGame() {
        mainMenu.SetActive(false);
        stagePick.SetActive(true);
    }
}
