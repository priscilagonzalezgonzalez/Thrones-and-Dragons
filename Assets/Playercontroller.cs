using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Playercontroller : MonoBehaviour
{
    public float speed = 25f;
    public float maxSpeed = 50f;
    public bool grounded;
    public bool attack;
    public float jumpPower = 0.01f;
    public Text ScoreTxt;
    public GameObject[] hearts;
    
    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer spr;
    private bool jump;
    private bool doubleJump;
    private bool movement = true;
    private int life;
    private Vector3 originalScale;



    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); //Busca el componente Rigidbody2D del personaje
        anim = GetComponent<Animator>(); //Busca el componente Animator del personaje
        spr = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;

        //ScoreTxt.text = PersistentManagerScript.Instance.Score.ToString(); //Obteniendo el valor Score actual
        //life = PersistentManagerScript.Instance.Life;

        // LifesTxt.text = PersistentManagerScript.Instance.Lifes.ToString(); //Obteniendo el valor Lifes actual
    }

    // Update is called once per frame
    void Update() 
    {
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("Grounded", grounded);
        anim.SetBool("Attack", attack);

        if (grounded){
            doubleJump = true;
        }

        if(Input.GetKeyDown(KeyCode.UpArrow)){ //Detectar la tecla de salto
            if(grounded){
                jump = true;
                doubleJump = true;
            }
            else if(doubleJump){
                jump = true;
                doubleJump = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            attack = true;
        }
        if(Input.GetKeyUp(KeyCode.Space)){
            attack = false;
        }
        /*
        if(life < 1){
            Destroy(hearts[0].gameObject);
        }
        else if(life < 2){
            Destroy(hearts[1].gameObject);
        }
        else if(life < 3){
            Destroy(hearts[2].gameObject);
        }*/
    }


    void FixedUpdate(){ //Cada frame

        Vector3 fixedVelocity = rb2d.velocity;
        fixedVelocity.x *= 0.83f;
        if(grounded){
            rb2d.velocity = fixedVelocity;
        }

        float h = Input.GetAxis("Horizontal"); //Dirección del personaje -1 izquierda +1 derecha
        if(!movement) h = 0;

        if(h > 0.1f){
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        if(h < -0.1f){
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
    
        rb2d.AddForce(Vector2.right * speed * h); //Movimiento del personaje Vector2 = +1, h = dirección (+1, -1)

        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

        if(jump){
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); //Agregar salto
            jump = false;
        }
    
    }

    void OnBecameInvisible(){
        transform.position = new Vector3(-9, 2, 0);
    }

    public void EnemyJump(){
        jump = true;
    }

    public void EnemyKnockBack(float enemyPosX){
        jump = true;
        float side = Mathf.Sign(enemyPosX - transform.position.x);
        rb2d.AddForce(Vector2.left * side * jumpPower, ForceMode2D.Impulse);

        movement = false;
        Invoke("EnableMovement", 0.7f);

        spr.color = Color.red;
    }

    void EnableMovement(){
        movement = true;
        spr.color = Color.white;
    }

    public void IncrementScore(int points){
        PersistentManagerScript.Instance.Score+= points;
        ScoreTxt.text = PersistentManagerScript.Instance.Score.ToString();
    }
    /*
    public void RestLife(){
        if(life > 1){
            PersistentManagerScript.Instance.Life--;
            life--;
        }
        else{
            Invoke("GameOverLoad", 0.2f);
        }
        
    }*/

    void GameOverLoad(){
        SceneManager.LoadScene("GameOver");
    }

 /*    public void RestLife(){
        if(lifes == 1){
            //GAME OVER!!
        }else{
            lifes = lifes - 1;
        }
    } */

}

