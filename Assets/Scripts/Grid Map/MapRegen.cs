using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapRegen : MonoBehaviour
{
/*    public static MapRegen Instance { get; private set;}
    void Awake() 
    {
        if(Instance != null && Instance != this)
        {
            Debug.Log("Destroy Instance.");
            Destroy(this);
        }    
        else
        {
            Debug.Log("Instance = this.");
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }*/

    void Update()
    {
        //Generate New Map - For Testing Purposes
        if(Input.GetKeyUp(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
