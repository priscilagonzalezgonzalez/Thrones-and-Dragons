using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public void ChangeScene(){
        Invoke("LoadNewScene", 0.5f);
        PersistentManagerScript.Instance.Score = 0;
        PersistentManagerScript.Instance.Life = 3;
        PersistentManagerScript.Instance.Minutes = 5;
        PersistentManagerScript.Instance.Seconds = 59;
    }
    
    void LoadNewScene(){
        SceneManager.LoadScene("SampleScene");
    }

    public void GoBackToMenu(){
        SceneManager.LoadScene("MenuScene");
    }
}
