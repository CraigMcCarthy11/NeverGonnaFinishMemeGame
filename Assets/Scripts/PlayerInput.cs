using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public bool useController;

    private Player thisPlayer;

    private Rigidbody rigidBody;
    private Vector3 moveInput;
    private Vector3 moveVelocity;

    // Use this for initialization
    void Start () {
        thisPlayer = GetComponent<Player>();
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update () {
        //Player Movement from inputmanager
        //We use getaxis because it stops movement when you let it go instead of lerping
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * thisPlayer.moveSpeed;

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
            if (Input.GetMouseButtonDown(0) || (Input.GetMouseButton(0) && thisPlayer.primaryWeapon.gunType == Gun.GunType.Assault))
            {
                thisPlayer.FireWeapon();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                thisPlayer.ReloadWeapon();
            }

            if(Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                thisPlayer.SwapWeapon();
            }
        }
        else //Newfangled controller controls
        {
            //Get the direction on the joystick, get rid of the '-' near the end to make controls inverted
            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("RHorizontal") + Vector3.forward * -Input.GetAxisRaw("RVertical");

            //If theres any movement rotate the player
            if (playerDirection.sqrMagnitude > 0.0f)
            {
                transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            }

            //Right bumper on xbox controller
            if (Input.GetKeyDown(KeyCode.Joystick1Button5))
            {
                thisPlayer.FireWeapon();
            }
        }
    }


    //FixedUpdate is called just before any physics calculations, so physics code goes in this
    private void FixedUpdate()
    {
        rigidBody.velocity = moveVelocity;
    }
}
