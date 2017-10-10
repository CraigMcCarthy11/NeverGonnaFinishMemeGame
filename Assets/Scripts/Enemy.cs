using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    //Variables
    public float health;
    public float pointsToGive;

    public GameObject player;
    public Gun primaryWeapon;

    public float waitTime;

    private float currentTime;

    //Methods
    public void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void Update()
    {
        if(health <= 0)
        {
            Die();
        }

        this.transform.LookAt(player.transform);

        if (currentTime == 0)
        {
            if (primaryWeapon.CanFire())
            {
                primaryWeapon.Fire();
            }
            else if(primaryWeapon.NeedsReload())
            {
                primaryWeapon.Reload();
            }
        }


        if (currentTime < waitTime)
            currentTime += 1 * Time.deltaTime;

        if (currentTime >= waitTime)
            currentTime = 0;
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
