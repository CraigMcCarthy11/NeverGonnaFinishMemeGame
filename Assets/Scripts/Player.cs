﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //Variables
    public float moveSpeed;
    public float points;
    public float maxHealth;
    public float health;

    public GameObject playerObj;
    public Gun primaryWeapon;

    private Rigidbody rigidBody;
    private Vector3 moveInput;
    private Vector3 moveVelocity;

    //Methods
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        health = maxHealth;
    }

    //Update is called before rendering a frame (game code goes here)
    private void Update()
    {
        //Player facing mouse
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0.0f;

        if (playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            playerObj.transform.rotation = Quaternion.Slerp(playerObj.transform.rotation, targetRotation, 7f * Time.deltaTime);
        }

        //Player Movement from inputmanager
        //We use getaxis because it stops movement when you let it go instead of lerping
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * moveSpeed;

        //Shooting
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            if(primaryWeapon.CanFire())
            {
                primaryWeapon.Fire();
            }
            else if (primaryWeapon.NeedsReload())
            {
                //Debug.Log("Needs Reload");
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            primaryWeapon.Reload();
        }

        //Player Death
        if (health <= 0)
        {
            Die();
        }
    }

    //FixedUpdate is called just before any physics calculations, so physics code goes in this
    private void FixedUpdate()
    {
        rigidBody.velocity = moveVelocity;
    }

    public void Die()
    {
        print("You dieded");
    }

}
