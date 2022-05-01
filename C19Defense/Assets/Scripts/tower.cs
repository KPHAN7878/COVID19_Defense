using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private float timeBetweenShots;

    [SerializeField] private float towerCost;

    public GameObject currentTarget;

    private float nextTimeShoot;

    private Vector3 projectileShootPosition;

    //vairable for projectile
    public GameObject liveProjectile;

    public Character testCovidPosition;

    
    private void Start()
    {
        nextTimeShoot = Time.time;
    }

    //function for tower to detect the nearest enemy
    private void updateNearestEnemy()
    {
        GameObject nearestEnemy = null;

        float distance = Mathf.Infinity;

        foreach(GameObject covid in EnemyList.covids)
        {
            if (covid != null)
            {
                float _distance = (transform.position - covid.transform.position).magnitude;

                if (_distance < distance)
                {
                    distance = _distance;
                    nearestEnemy = covid;
                }
            }
            
        }

        if (distance <= range)
        {
            currentTarget = nearestEnemy;
        }
        else
        {
            currentTarget = null;
        }
    }


    //function to shoot covid units and take damage
    //changed to protected virtual from private
    protected virtual void shoot()
    {
        Character covidScript = currentTarget.GetComponent<Character>();
        testCovidPosition = covidScript;

        
        //TESTING ROTATION OF THE TOWER WHEN SHOOTING
        Vector2 relative = covidScript.transform.position - this.transform.position;
        float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;
        Vector3 newRotation = new Vector3(0, 0, angle-90);
        this.transform.localRotation = Quaternion.Euler(newRotation);

        //spawn the projectile when shooting
        //GameObject currentBullet = Instantiate(liveProjectile, this.transform.position, this.transform.rotation);
        Vector3 projectileAngle = new Vector3(0, 0, angle);
        GameObject currentBullet = Instantiate(liveProjectile, this.transform.position, liveProjectile.transform.localRotation = Quaternion.Euler(projectileAngle));

        
        /*
        covidScript.takeDamage(damage);
        */
        
    }

    private void Update()
    {
        updateNearestEnemy();

        if (Time.time >= nextTimeShoot)
        {
            if (currentTarget != null)
            {
                shoot();
                nextTimeShoot = Time.time + timeBetweenShots;
            }
        }
    }
}
