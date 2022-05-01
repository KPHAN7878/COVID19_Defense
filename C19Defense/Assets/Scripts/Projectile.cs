using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed;

    public tower tow;
    public Vector2 targetPosition;
    
    //detect collisions with covid assets
    //if collision is detected, destroy projectile isntance
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Character" || collision.gameObject.tag == "boss")
        {
            //Debug.Log("Hello");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //destroy projectile 5 seconds after instantiated if it has not hit any covid units
        Destroy(gameObject, 2f);
        
    }

    private void Update()
    {
        //if projectile belogns to sniper tower, then make it move faster
        if (gameObject.tag == "sniper_projectile")
        {
            transform.position += transform.right * 0.36f;
        }

        else
        {
            //for every update call, makes the projectile move in direction of the covid unit with a certain speed
            transform.position += transform.right * 0.21f;
        }
        

    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

}
