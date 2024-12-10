using UnityEngine;
using System.Collections;
using UnityEngine.Animations;
using System;
using TMPro;
public class WeaponShoot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Update is called once per frame

    //Shooting
    public bool isShooting, readyToShoot;
    public float shootingDelay = 2f;
    bool allowReset = true;

    //Burst
    private int burstBulletsLeft;
    public int bulletsPerBurst = 3;

    //Spread
    public float spreadIntensity;

    //Bullet
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 30.0f;
    public float bulletLife = 3.0f;

    public GameObject muzzleEffect;



    // Loading Weapon
    public float reloadTime;
    public int magazineSize;
    private int bulletsLeft;
    public bool isReloading;
    // Shootingmode
    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }
    public ShootingMode currentShootingMode;
    public Animator animator;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
        animator = GetComponent<Animator>();
        bulletsLeft = magazineSize;
    }


    void Update()
    {
        //If weapon mode is not activated, we return
        if (!GlobalReferences.Instance.playerManager.PlayerWeaponMode)
        {
            return;
        }
        //Sound Handling
        if (currentShootingMode == ShootingMode.Auto)
        {
            //Hold down mouse button to shoot
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if (currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst)
        {
            //Press mouse button to shoot
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && isReloading == false)
        {
            Reload();
        }
        if (readyToShoot && isShooting && !isReloading && bulletsLeft > 0)
        {
            burstBulletsLeft = bulletsPerBurst;
            Fire();
        }
        if (bulletsLeft <= 0 && isShooting)
        {
            if (AudioManager.Instance.emptyMagazine != null)
            {
                AudioManager.Instance.emptyMagazine.Play();
            }
            readyToShoot = false;
            if (!isReloading)
            {
                Reload();
            }
        }

        // Update Bullets UI
        if (GlobalReferences.Instance.AmmoDisplay != null)
        {
            GlobalReferences.Instance.AmmoDisplay.text = $"Bullets: {bulletsLeft / bulletsPerBurst}/{magazineSize / bulletsPerBurst}";
        }
    }
    private void Fire()
    {
        if (isReloading) return;

        bulletsLeft--;
        muzzleEffect.GetComponent<ParticleSystem>().Play();
        // Can not shoot again before the first shot is done
        readyToShoot = false;
        if (AudioManager.Instance.shootingSound != null)
        {
            AudioManager.Instance.shootingSound.Play();
        }
        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;
        animator.SetTrigger("RECOIL");

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.transform.forward = shootingDirection;


        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletSpeed, ForceMode.Impulse);
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletLife));
        //Check if shooting is done
        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        //Burst Mode check
        if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1)
        {
            burstBulletsLeft--;
            Invoke("Fire", shootingDelay);
        }
    }
    private void Reload()
    {
        if (AudioManager.Instance.reloadingSound != null)
        {
            AudioManager.Instance.reloadingSound.Play();
        }
        animator.SetTrigger("RELOAD");
        isReloading = true;
        readyToShoot = false; // Prevent shooting during reload
        Invoke("ReloadCompleted", reloadTime);
    }

    private void ReloadCompleted()
    {
        bulletsLeft = magazineSize;
        isReloading = false;
        readyToShoot = true; // Allow shooting after reload is completed
    }
    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

    public Vector3 CalculateDirectionAndSpread()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100);
        }
        Vector3 direction = targetPoint - bulletSpawn.position;
        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        // returning the shooting direction and spread
        return direction + new Vector3(x, y, 0);
    }
    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }
}
