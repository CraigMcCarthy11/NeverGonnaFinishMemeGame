using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    //Variables
    public float health;
    public float pointsToGive;

    public GameObject player;
    public GameObject bullet;
    public Transform bulletSpawnPoint;

    private Transform bulletSpawned;

    public float waitTime;

    private float currentTime;
    private bool shot;

    //Methods
    public void Start()
    {
        player = GameObject.FindWithTag("Player");

        bulletSpawnPoint = this.transform.GetChild(0).GetChild(2);
    }

    public void Update()
    {
        if(health <= 0)
        {
            Die();
        }

        this.transform.LookAt(player.transform);

        if(currentTime == 0)
            Shoot();
        
        if (shot && currentTime < waitTime)
            currentTime += 1 * Time.deltaTime;

        if (currentTime >= waitTime)
            currentTime = 0;
    }

    public void Die()
    {
        Destroy(this.gameObject);

        player.GetComponent<Player>().points += pointsToGive;
    }

    public void Shoot()
    {
        shot = true;

        bulletSpawned = Instantiate(bullet.transform, bulletSpawnPoint.transform.position, Quaternion.identity);
        bulletSpawned.rotation = this.transform.rotation;
    }
}
