using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour {

    public enum GunType
    {
        Pistol,
        Assault,
        Sniper
    };

    public Transform bulletFirePoint;
    public Bullet bullet;
    public GunType gunType;
    public Sprite gunSprite;

    public int clipSize;
    public int clips;
    public int bulletsInClip;
    public float fireRate;
    public float reloadRate;
    public float projectileSpeed;
    public int bulletRange;
    public int bulletDamage;

    private bool canFire = true;
    private bool canReload = true;
    private bool swapped = false;

    //Developer stuff
    public bool infiniteAmmo;

    // Load the initial clip
    public void Initialize()
    {
        bulletsInClip = clipSize;
    }

    //Called by the unit using it to check if we can fire
    public bool CanFire()
    {
        return canFire && bulletsInClip > 0 && canReload;
    }

    //Fires the weapon per projectile
    public void Fire()
    {
        // blat blat blat
        canFire = false;

        bulletsInClip--;

        //Creates the projectile
        Bullet bulletSpawned = Instantiate(bullet, bulletFirePoint.position, bulletFirePoint.rotation);
        bulletSpawned.Initialize(projectileSpeed, bulletRange, bulletDamage);

        //Starts the cooldown before next projectile
        StartCoroutine(FireTimer());
    }

    //Called by the unit to check if we need to reload
    public bool NeedsReload()
    {
        return bulletsInClip <= 0;
    }

    //Reloads the weapon
    public void Reload()
    {
        if (canReload && bulletsInClip < clipSize) {
            //Check if we have the ammo to do it
            if (clips > 0 || infiniteAmmo)
            {
                bulletsInClip = clipSize;
                if(!infiniteAmmo) { clips--; }

                canFire = false;
                canReload = false;

                //Starts the timer for reloading
                StartCoroutine(ReloadTimer());
            }
        }
    }

    private void OnEnable()
    {
        if (swapped)
        {
            canFire = false;
            canReload = false;
            swapped = false;
            StartCoroutine(ReloadTimer());
        }
    }

    private void OnDisable()
    {
        swapped = true;
    }

    private IEnumerator FireTimer()
    {
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }

    private IEnumerator ReloadTimer()
    {
        yield return new WaitForSeconds(reloadRate);
        canFire = true;
        canReload = true;
    }
}
