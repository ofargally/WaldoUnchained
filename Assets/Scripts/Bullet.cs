using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision ObjectHit)
    {
        CreateBulletImpactEffect(ObjectHit);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("PLAYER HIT BY BULLET!");
            GlobalReferences.Instance.playerManager.TakeDamage(1);
        }
        else
        {
            CreateBulletImpactEffect(other);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy of name: " + other.gameObject.name);
            EnemyManager enemyManager = other.gameObject.GetComponent<EnemyManager>();
            if (enemyManager != null)
            {
                Debug.Log("Damage Dealt");
                enemyManager.TakeDamage(1);
            }
            Destroy(gameObject);
        }
    }

    private void CreateBulletImpactEffect(Collision ObjectHit)
    {
        ContactPoint contact = ObjectHit.contacts[0];
        GameObject hole = Instantiate(
            GlobalReferences.Instance.bulletImpactEffectPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)
        );
        hole.transform.SetParent(ObjectHit.gameObject.transform);
    }

    private void CreateBulletImpactEffect(Collider other)
    {
        GameObject hole = Instantiate(
            GlobalReferences.Instance.bulletImpactEffectPrefab,
            other.ClosestPoint(transform.position),
            Quaternion.identity
        );
        hole.transform.SetParent(other.gameObject.transform);
    }
}