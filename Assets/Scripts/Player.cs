using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //Variables
    public float movementSpeed;
    public float points;
    public float maxHealth;
    public float health;

    public GameObject playerObj;
    public Gun primaryWeapon;

    //Methods
    private void Start()
    {
        health = maxHealth;
    }

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

        //Player Movement
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        
        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
        
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
        
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);

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

    public void Die()
    {
        print("You dieded");
    }

}
