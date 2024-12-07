using UnityEngine;
using System.Collections;
public class WeaponShoot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Update is called once per frame
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 30.0f;
    public float bulletLife = 3.0f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
        }
    }
    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletLife));
    }
    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
