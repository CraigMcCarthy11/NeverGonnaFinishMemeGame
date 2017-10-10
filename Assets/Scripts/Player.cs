using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //Variables
    public float moveSpeed;
    public float maxHealth;
    public float health;

    public GameObject playerObj;
    public Gun primaryWeapon;

    //Methods
    private void Start()
    {
        health = maxHealth;
    }

    //Update is called before rendering a frame (game code goes here)
    private void Update()
    {
        //Player Death
        if (health <= 0)
        {
            Die();
        }
    }

    public void UseActiveWeapon()
    {
        if (primaryWeapon.CanFire())
        {
            primaryWeapon.Fire();
        }
        else if (primaryWeapon.NeedsReload())
        {
            //Debug.Log("Needs Reload");
        }
    }

    public void Die()
    {
        //print("You dieded");
    }

}