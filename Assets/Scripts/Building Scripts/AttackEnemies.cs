using UnityEngine;
using System.Collections;

public class AttackEnemies : State
{

    public GameObject enemy;
    public GameObject bullet;

    private float timeLeft = 0;
    private float maxSeconds;
    void Start()
    {
        maxSeconds = 1 / GetComponent<BuildingProperties>().firingSpeed;
    }

    void Update()
    {
        if (enemy)
        {
            if(timeLeft <= 0)
            {
                GameObject newBullet = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
                newBullet.GetComponent<BulletScript>().damage = GetComponent<BuildingProperties>().damagePerHit;
                newBullet.GetComponent<BulletScript>().target = enemy;
                timeLeft = maxSeconds;
            } else
            {
                timeLeft -= Time.deltaTime;
            }
        }
        else
        {
            timeLeft = 0;
            nextState = GetComponent<LookForEnemiesScript>();
        }
    }
}
