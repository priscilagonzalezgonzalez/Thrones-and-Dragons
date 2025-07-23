using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DragonController : MonoBehaviour
{
    private Animator anim;
    
    public bool attack = false;
    public float attackTime = 10f;
    public GameObject player;
    float startingTime = 0f;
    float currentTime = 0f;
    float count = 0;
    float attackCount = 0;
    public int dragonLife = 8;
    public Playercontroller playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xOffset = 2.5f;
        float yOffset = -3.6f;
        float xOffsetP = 1.2f;
        float yOffsetP = -3.43f;
        if(player.transform.position.x < transform.position.x){
            transform.localScale = new Vector3(1f, 1f, 1f);
            if((transform.position.x - player.transform.position.x <= xOffset) && attack == true){
                if (player.transform.position.y <= yOffset){
                    StartDamage();
                    count++;
                }
            }
            else{
                restartCount();
            }

            if(player.transform.localScale.x == 1 && (transform.position.x - player.transform.position.x <= xOffsetP) && playerController.attack == true && player.transform.position.y <= yOffsetP){
                Debug.Log(dragonLife);
                attackCount++;
            }
            else{
                restartCountPlayer();
            }
            
        }
        else if(player.transform.position.x > transform.position.x){
            transform.localScale = new Vector3(-1f, 1f, 1f);
            if((player.transform.position.x - transform.position.x <= xOffset) && attack == true){
                if (player.transform.position.y <= yOffset){
                    count++;
                    StartDamage();
                }
            }
            else{
                restartCount();
            }

            if(player.transform.localScale.x == -1 && (player.transform.position.x - transform.position.x <= xOffsetP) && playerController.attack == true && player.transform.position.y <= yOffsetP){
                Debug.Log(dragonLife);
                attackCount++;
            }
            else{
                restartCountPlayer();
            }
        }
    }

    void Update()
    {
        anim.SetBool("Attack", attack);
        currentTime += 1 * Time.deltaTime;
        if(currentTime >= attackTime){
            currentTime = startingTime;
            attack = true;
            Invoke("StopAttack", 0.1f);
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Player" && playerController.attack == true){
            Debug.Log("On collision");
        }
    }

    void StopAttack(){
        attack = false;
    }

    void StartDamage(){
        player.SendMessage("EnemyKnockBack", transform.position.x);
    }

    void restartCount(){
        if(count > 0){
            player.SendMessage("RestLife");
        }
        
        count = 0;
    }

    void restartCountPlayer(){
        if(attackCount > 0){
            if(dragonLife > 1){
                dragonLife--;
            }
            else{
                Invoke("Winner", 0.5f);
                playerController.IncrementScore(1000);
            }
            
        }
        attackCount = 0;
    }

    void Winner(){
        SceneManager.LoadScene("Win");
    }
}
