using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    //Variables
    public float moveSpeed;
    public float points;
    public float maxHealth;
    public float health;

    //Associated Object Elements
    public GameObject playerObj;
    public Gun primaryWeapon;
    public Gun secondaryWeapon;
    public Gun currentWeapon;

    //HUD Elements
    public RectTransform healthBar;
    public Text bulletsInClipText;
    public Text clipsText;
    public Image activeWeapon;
    public Image holdsteredWeapon;

    //Methods
    /// <summary>
    /// Called during object's first frame of being active
    /// </summary>
    private void Start()
    {
        health = maxHealth;
        healthBar.sizeDelta = new Vector2((health / 100) * 400, 16);

        currentWeapon = primaryWeapon;
        activeWeapon.sprite = primaryWeapon.gunSprite;
        holdsteredWeapon.sprite = secondaryWeapon.gunSprite;

        primaryWeapon.Initialize();
        secondaryWeapon.Initialize();

        UpdateHUD();
    }

    /// <summary>
    /// Updates weapon HUD values
    /// </summary>
    private void UpdateHUD()
    {
        bulletsInClipText.text = currentWeapon.bulletsInClip + "<color=#ffffff77>/</color>" + currentWeapon.clipSize;
        clipsText.text = currentWeapon.infiniteAmmo ? "~" : currentWeapon.clips.ToString();
    }

    /// <summary>
    /// What I want to do right now
    /// </summary>
    private void Die()
    {
        //print("You dieded");
    }

    /// <summary>
    /// Public function called from bullets that hit this object
    /// </summary>
    /// <param name="points"> Points to remove from health </param>
    public void TakeDamage(float points)
    {
        var h = health;
        h -= points;
        if (h <= 0)
        {
            h = 0;
            Die();
        }
        health = h;
        healthBar.sizeDelta = new Vector2((health / 100) * 400, 16);
    }

    /// <summary>
    /// Fires currenty weapon
    /// </summary>
    public void FireWeapon()
    {
        if (currentWeapon.CanFire())
        {
            currentWeapon.Fire();
            UpdateHUD();
        }
    }

    /// <summary>
    /// Reloads current weapon
    /// </summary>
    public void ReloadWeapon()
    {
        currentWeapon.Reload();
        UpdateHUD();
    }

    /// <summary>
    /// Swaps between primary and secondary weapon and sets current weapon, also assigns correct icon on HUD
    /// </summary>
    public void SwapWeapon()
    {
        if (currentWeapon == primaryWeapon)
        {
            primaryWeapon.gameObject.SetActive(false);
            secondaryWeapon.gameObject.SetActive(true);
            currentWeapon = secondaryWeapon;

            activeWeapon.sprite = secondaryWeapon.gunSprite;
            holdsteredWeapon.sprite = primaryWeapon.gunSprite;
        }
        else
        {
            primaryWeapon.gameObject.SetActive(true);
            secondaryWeapon.gameObject.SetActive(false);
            currentWeapon = primaryWeapon;

            activeWeapon.sprite = primaryWeapon.gunSprite;
            holdsteredWeapon.sprite = secondaryWeapon.gunSprite;
        }
        UpdateHUD();
    }
}
