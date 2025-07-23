using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDead : MonoBehaviour
{
    public Playercontroller player;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Player"){
            Debug.Log("Ground Collision");
            player.RestLife();
        }
    }
}
