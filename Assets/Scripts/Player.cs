using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //Variables
    public float moveSpeed;
    public float points;
    public float maxHealth;
    public float health;

    public bool useController;

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
        //Player Movement from inputmanager
        //We use getaxis because it stops movement when you let it go instead of lerping
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * moveSpeed;

        //Good ol' keyboard and mouse controls
        if (!useController)
        {
            //Player facing mouse
            //Plane gets created facing upwards at where the player is at
            Plane playerPlane = new Plane(Vector3.up, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Distance of ray
            float hitDist;

            if (playerPlane.Raycast(ray, out hitDist))
            {
                //raycast hit point
                Vector3 targetPoint = ray.GetPoint(hitDist);

                //Slow movement aiming
                /*Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
                targetRotation.x = 0;
                targetRotation.z = 0;
                playerObj.transform.rotation = Quaternion.Slerp(playerObj.transform.rotation, targetRotation, 7f * Time.deltaTime);*/

                //Fast movement aiming
                transform.LookAt(new Vector3(targetPoint.x, transform.position.y, targetPoint.z));
            }

            //Shooting
            if (Input.GetMouseButtonDown(0) || (Input.GetMouseButton(0) && primaryWeapon.gunType == Gun.GunType.Assault))
            {
                UseActiveWeapon();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                primaryWeapon.Reload();
            }
        }
        else //Newfangled controller controls
        {
            //Get the direction on the joystick, get rid of the '-' near the end to make controls inverted
            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("RHorizontal") + Vector3.forward * -Input.GetAxisRaw("RVertical");
            
            //If theres any movement rotate the player
            if(playerDirection.sqrMagnitude > 0.0f)
            {
                transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            }

            //Right bumper on xbox controller
            if (Input.GetKeyDown(KeyCode.Joystick1Button5))
            {
                UseActiveWeapon();
            }
        }

        //Player Death
        if (health <= 0)
        {
            Die();
        }
    }

    private void UseActiveWeapon()
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

    //FixedUpdate is called just before any physics calculations, so physics code goes in this
    private void FixedUpdate()
    {
        rigidBody.velocity = moveVelocity;
    }

    public void Die()
    {
        //print("You dieded");
    }

}
