using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
    public Text ScoreTxt;
    // Start is called before the first frame update
    void Start()
    {
        ScoreTxt.text = PersistentManagerScript.Instance.Score.ToString();
    }
}
