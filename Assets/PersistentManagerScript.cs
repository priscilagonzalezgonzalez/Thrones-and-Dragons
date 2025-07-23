using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentManagerScript : MonoBehaviour
{
    public static PersistentManagerScript Instance {get; private set;}

    public int Score;
    public int Life;
    public int Minutes;
    public int Seconds;

    private void Awake()
    {
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }
}
