using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSheep1 : MonoBehaviour
{

    const int MAX_JUMP = 1;

    public float walkingSpeed;
    public float forceJump;
    private int numberJump;

    public GameObject textCoinScore;
    private int numberCoinsCollected;

    public GameObject textKeyScore;
    private int numberKeysCollected;

    public AudioClip coinCollected;
    public AudioClip keyCollected;

    public Vector3 startPosition;

    private bool isDead;

    void Start()
    {
        this.numberJump = MAX_JUMP;
        this.numberCoinsCollected = 0;
        this.numberKeysCollected = 0;
        this.startPosition = this.transform.position;
        this.isDead = false;
        updateCoinScore();
        updateKeyScore();
    }

    void Update()
    {
        
        walk();
        jump();
        dead();
       
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {

        if(collision2D.collider.CompareTag("collisionFloor"))
        {
            this.numberJump = MAX_JUMP;
            this.GetComponent<Animator>().SetBool("isJumping", false);
        }

        if(collision2D.collider.CompareTag("villian")) this.isDead = true;

    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {

        if(collider2D.CompareTag("colliderCoin"))
        {
            Destroy(collider2D.gameObject);
            numberCoinsCollected++;
            updateCoinScore();
            this.GetComponent<AudioSource>().PlayOneShot(coinCollected);
        }

        if(collider2D.CompareTag("colliderKey"))
        {
            Destroy(collider2D.gameObject);
            numberKeysCollected++;
            updateKeyScore();
            this.GetComponent<AudioSource>().PlayOneShot(keyCollected);
        } 


        if(collider2D.CompareTag("win") && numberKeysCollected == 1) 
        {
            Destroy(collider2D.gameObject);
            this.isDead = true;
            dead();
        }
    }

    void updateCoinScore()
    {
        textCoinScore.GetComponent<Text>().text = numberCoinsCollected.ToString();
    }

    void updateKeyScore()
    {
        textKeyScore.GetComponent<Text>().text = numberKeysCollected.ToString();
    }

    void walk()
    {
        
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 position = this.transform.position;
            position.x += this.walkingSpeed;
            this.transform.position = position;

            this.GetComponent<Animator>().SetBool("isRunning", true);
            this.GetComponent<SpriteRenderer>().flipX = false;
        } 
        else if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 position = this.transform.position;
            position.x -= this.walkingSpeed;
            this.transform.position = position;

            this.GetComponent<SpriteRenderer>().flipX = true;
            this.GetComponent<Animator>().SetBool("isRunning", true);
        }
        else
        {
            this.GetComponent<Animator>().SetBool("isRunning", false);
        }
    }

    void jump()
    {
        if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)
            || Input.GetKey(KeyCode.UpArrow)) && this.numberJump > 0)
        {
            this.numberJump--;
            Vector2 jumpStrength = new Vector2(0f, this.forceJump);
            this.GetComponent<Rigidbody2D>().AddForce(jumpStrength, ForceMode2D.Impulse);

            this.GetComponent<Animator>().SetBool("isJumping", true);
        }
    }

    void dead()
    {
        Vector3 currentPosition = this.transform.position;

        if(currentPosition.y < -10f) this.isDead = true;
        
        if(this.isDead)
        {
            this.transform.position = this.startPosition;
            this.isDead = false;
        }
    }

}
