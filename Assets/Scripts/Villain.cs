using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villain : MonoBehaviour
{

    public float walkingSpeed;
    public GameObject character;

    private bool left;
    private bool rigth;
    public Vector3 startPosition;

    void Start()
    {
        this.left = false;
        this.rigth = false;
        this.startPosition = this.transform.position;

    }

    void Update()
    {
         walk();
         dead();

    }

    void walk()
    {
        if(character.transform.position.x > this.transform.position.x)
        {
            this.rigth = true;
            this.left = false;
        }
        else
        {
            this.rigth = false;
            this.left = true;
        }

        if (left) walkLeft();
        else if(rigth) walkRigth();
    }

    void walkLeft()
    {
        Vector3 position = this.transform.position;
        position.x -= this.walkingSpeed;
        this.transform.position = position;

        this.GetComponent<SpriteRenderer>().flipX = false;
       
    }

    void walkRigth()
    {
        
        Vector3 position = this.transform.position;
        position.x += this.walkingSpeed;
        this.transform.position = position;

        
        this.GetComponent<SpriteRenderer>().flipX = true;
        
    }

    void dead()
    {
        Vector3 currentPosition = this.transform.position;

        if(currentPosition.y < -10f)
        {
            this.transform.position = this.startPosition;
        }
    }
}
