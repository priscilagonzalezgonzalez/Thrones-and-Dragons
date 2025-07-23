using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeControllerLvl2 : MonoBehaviour
{
    public Text MinTxt;
    public Text SecTxt;
    
    private int minutes;
    private int seconds;
    private bool takingAway = false;
    // Start is called before the first frame update
    void Start()
    {
        minutes = PersistentManagerScript.Instance.Minutes;
        MinTxt.text = PersistentManagerScript.Instance.Minutes.ToString();
        seconds = PersistentManagerScript.Instance.Seconds;
        SecTxt.text = PersistentManagerScript.Instance.Seconds.ToString();
    }

    void Update(){
        if(takingAway == false && minutes >= 0){
            StartCoroutine(TimeTaker());
        }
    }

    // Update is called once per frame
    IEnumerator TimeTaker()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        seconds -= 1;
        if(minutes == 5){
            minutes -= 1;
        }
        PersistentManagerScript.Instance.Minutes = minutes;
        MinTxt.text = PersistentManagerScript.Instance.Minutes.ToString();
        PersistentManagerScript.Instance.Seconds = seconds;
        if(seconds < 10){
            SecTxt.text = "0" + PersistentManagerScript.Instance.Seconds.ToString();
        }
        else{
            SecTxt.text = PersistentManagerScript.Instance.Seconds.ToString();
        }
            
        
        if(seconds == 0){
            seconds = 60;
            if(minutes != 0){
                minutes -= 1;
            }
            if(minutes == 0){
                Invoke("TimeIsUp", 0.3f);
            }
        }
        takingAway = false;
    }

    void TimeIsUp(){
        SceneManager.LoadScene("GameOver");
    }
}
