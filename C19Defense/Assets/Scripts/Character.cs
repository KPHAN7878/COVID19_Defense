using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

    //public variables for inspector

    //variable for health of each covid units
    public float covidHealth;

    //variable for healthbar proportion
    private float healthDecrease;

    //variable to control covid unit health bar
    public Image healthBar;

    //variable for currency reward once covid units are killed
    public int killReward;

    /*public int damage;*/

    
    //variable to set movement speed of covid uunits
    public float speed;


    //reference to Waypoints class in "Waypoints.cs" that contains list of waypoints
    private Waypoints wpoints;


    //waypoint index for covid  units to iterate through
    private int waypointIndex;



    //these are variables to slow covid units down if they get hit by face mask projectile
    private bool hit_by_slow_projectile;
    public float timeRemaining;


    //these are variables to initialize poison effect if void is hit by sanitizer projectile
    private bool hit_by_sanitzier_projectile;
    private float poisonTimeRemaining;

    private float nextTimeForPoison;





    //reference EnemyList script
    private void Awake()
    {
        EnemyList.covids.Add(gameObject);
    }


    //upon start, initialze all the waypoints on the map
    void Start()
    {
        wpoints = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<Waypoints>();

        healthDecrease = covidHealth;

        nextTimeForPoison = Time.time;
    }

    void Update()
    {
        //this if/else block determines if a covid unit has been hit by a slow projectile 
        //and if so, how long until slow effect wears off
        if(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            hit_by_slow_projectile = false;
        }


        //this if/else block determines if a covid unit has been hit by a sanitizer poison projectile 
        //and if so, how long until slow effect wears off
        if(poisonTimeRemaining > 0)
        {
            poisonTimeRemaining -= Time.deltaTime;

            if (Time.time >= nextTimeForPoison)
            {
                takeDamage(15);
                nextTimeForPoison = Time.time + 1;
            }

        }
        


        if(hit_by_slow_projectile == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, wpoints.waypoints[waypointIndex].position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, wpoints.waypoints[waypointIndex].position, (speed/2) * Time.deltaTime);
        }
        

        //this block of code is to rotate the covid asset as it moves through the waypoints
        Vector3 direction = wpoints.waypoints[waypointIndex].position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        //check if covid asset has moved to waypoint and if so, move to next waypoint
        if(Vector2.Distance(transform.position, wpoints.waypoints[waypointIndex].position) < 0.1f)

            //if covid unit has not moved to last waypoint, then icnrement index to move to next waypoint
            if(waypointIndex < wpoints.waypoints.Length - 1)
            {
                waypointIndex++;
            }
            //if covid unit has arrived at the last waypoint, then destroy the asset object and decrement health
            else
            {
                Destroy(gameObject);
                PlayerAttributes.health -= 1;
            }       
    }


    
    //detects collision with different types of projectiles and takes damage accordingly
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "blue_syringe")
        {
            takeDamage(20);
            //Debug.Log(collision.gameObject.tag);
        }

        if (collision.gameObject.tag == "SpikeTrap")
        {
            takeDamage(100);
            //Debug.Log(collision.gameObject.tag);

        }

        if (collision.gameObject.tag == "laser")
        {
            takeDamage(75);
            //Debug.Log(collision.gameObject.tag);

        }

        if (collision.gameObject.tag == "mask" && gameObject.tag != "boss")
        {
            takeDamage(10);
            hit_by_slow_projectile = true;

            //set duration of slow effect to 3 seconds
            //max duration of 3 seconds (slows cannot stack)
            timeRemaining = 3;
            if(timeRemaining > 3)
            {
                timeRemaining = 3;
            }

            //Debug.Log(collision.gameObject.tag);

        }

        if (collision.gameObject.tag == "sanitizer_squirt")
        {
            takeDamage(10);
            poisonTimeRemaining += 3;
            //Debug.Log(collision.gameObject.tag);

        }

        if (collision.gameObject.tag == "sniper_projectile")
        {
            if(gameObject.tag == "boss")
            {
                takeDamage(300);
            }
            else
            {
                takeDamage(100);
                //Debug.Log(collision.gameObject.tag);
            }
            
        }



    }
    
    

    //function for covid to take damage once they come in contact with projectile
    public void takeDamage(float damageAmount)
    {
        covidHealth -= damageAmount;

        //Debug.Log("healthDecrease/covidHealth: " + healthDecrease/covidHealth);
        healthBar.fillAmount = covidHealth/healthDecrease;

        //if instance of covid asset reaches 0, destroy the game object and reward the player with currency
        if (covidHealth <= 0)
        {
            Destroy(gameObject);
            PlayerAttributes.currency += killReward;
            PlayerAttributes.covidUnitsKilled += 1;
        }
    }











    /*

    //when covid dies
    private void die()
    {
        EnemyList.covids.Remove(gameObject);
        //int tempReward = killReward;
        Destroy(transform.gameObject);

        //PlayerAttributes.currency += tempReward;
    }

    private void initializeEnemy()
    {

    }

    private void moveEnemy()
    {

    }

    */
}


