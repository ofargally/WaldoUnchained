using UnityEngine;
using System.Collections;

public class EnemyWeaponShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 30f;
    public float bulletLife = 3f;
    public float fireRate = 1f; // How often enemy fires
    public float spreadIntensity = 0.1f;
    public ParticleSystem muzzleEffect;
    public AudioSource shootingSound;

    private bool canShoot = true;

    public void TryShootAtPlayer(Vector3 targetPosition)
    {
        // If canShoot is true, shoot once and set cooldown
        if (canShoot)
        {
            Shoot(targetPosition);
            StartCoroutine(ResetShot());
        }
    }

    void Shoot(Vector3 targetPosition)
    {
        canShoot = false;

        // Play muzzle effect and sound
        muzzleEffect?.Play();
        shootingSound?.Play();

        Vector3 direction = (targetPosition - bulletSpawn.position).normalized;
        direction = ApplySpread(direction);

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.LookRotation(direction));
        bullet.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed, ForceMode.Impulse);
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletLife));
    }

    IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (bullet != null) Destroy(bullet);
    }

    IEnumerator ResetShot()
    {
        yield return new WaitForSeconds(1f / fireRate);
        canShoot = true;
    }

    Vector3 ApplySpread(Vector3 direction)
    {
        float x = Random.Range(-spreadIntensity, spreadIntensity);
        float y = Random.Range(-spreadIntensity, spreadIntensity);
        return (direction + new Vector3(x, y, 0)).normalized;
    }
}
