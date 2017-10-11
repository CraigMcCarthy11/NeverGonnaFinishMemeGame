using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour {

    public void OnTriggerEnter(Collider other)
    {
        /*if (other.tag == "Enemy")
        {
           
        }*/

        if (other.tag == "Player")
        {
            //Get the object just in case we want to do anything with this
            GameObject triggeringPlayerObject = other.gameObject;

            //Gets player class then gives ammo to both weapons + updates the HUD
            Player thisPlayer = triggeringPlayerObject.GetComponent<Player>();
            thisPlayer.primaryWeapon.clips++;
            thisPlayer.secondaryWeapon.clips++;
            thisPlayer.UpdateHUD();

            Destroy(this.gameObject);
        }
    }
}
