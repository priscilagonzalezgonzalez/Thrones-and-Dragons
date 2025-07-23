using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{
    public float speed = 1f;
    public float maxSpeed = 1f;
    public bool attack = false;

    private Rigidbody2D rb2d;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetBool("Attack2", attack);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2d.AddForce(Vector2.right * speed); 
        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

        if(rb2d.velocity.x > -0.01f && rb2d.velocity.x < 0.01f){
            speed = -speed;
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        }

        if(speed > 0){
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if(speed < 0){
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        float yOffset = -2.09f;
        float side = Mathf.Sign(transform.position.x - col.transform.position.x);
        if(col.gameObject.tag == "Player"){
            Debug.Log(col.transform.position.y);
            if(col.transform.position.y >= yOffset){
                col.SendMessage("EnemyJump");
                col.SendMessage("IncrementScore", 10);
                Destroy(gameObject);
            }
            else if((transform.localScale.x == 1f && side == -1) || (transform.localScale.x == -1f && side == 1))
            {
                Debug.Log("True");
                attack = true;    
                col.SendMessage("RestLife", 1);
                Invoke("StopAttack", 0.1f);
                col.SendMessage("EnemyKnockBack", transform.position.x);
            }
        }
    }

    void StopAttack(){
        attack = false;
    }
}
