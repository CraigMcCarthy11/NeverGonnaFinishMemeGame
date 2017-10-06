using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour {

    public Transform bulletSpawnPoint;
    public Transform bullet;

    public int clipSize;
    public int clips;
    public int bulletDamage;
    public float fireRate;
    public float reloadRate;
    public bool infiniteAmmo;

    private bool canFire = true;
    private bool canReload = true;
    private int bulletsInClip;

    private void Start()
    {
        Initialize();
    }

    // Load the initial clip
    protected void Initialize()
    {
        bulletsInClip = clipSize;
    }

    public bool CanFire()
    {
        return canFire && bulletsInClip > 0 && canReload;
    }

    public void Fire()
    {
        // blat blat blat
        canFire = false;

        bulletsInClip--;

        Transform bulletSpawned = Instantiate(bullet.transform, bulletSpawnPoint.transform.position, Quaternion.identity);
        bulletSpawned.rotation = bulletSpawnPoint.transform.rotation;

        StartCoroutine(FireTimer());
    }

    public bool NeedsReload()
    {
        return bulletsInClip <= 0;
    }

    public void Reload()
    {
        if (canReload) {
            if (clips > 0 || infiniteAmmo)
            {
                bulletsInClip = clipSize;
                clips--;

                canFire = false;
                canReload = false;
                StartCoroutine(ReloadTimer());
            }
        }
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
