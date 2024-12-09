using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private void OnCollisionEnter(Collision ObjectHit)
    {
        if (!ObjectHit.gameObject.CompareTag("Player"))
        {
            CreateBulletImpactEffect(ObjectHit);
        }

        if (ObjectHit.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy of name: " + ObjectHit.gameObject.name);
            EnemyManager enemyManager = ObjectHit.gameObject.GetComponent<EnemyManager>();
            if (enemyManager != null)
            {
                Debug.Log("Damage Dealt");
                enemyManager.takeDamage();
            }
            Destroy(gameObject);
        }
    }
    void CreateBulletImpactEffect(Collision ObjectHit)
    {
        ContactPoint contact = ObjectHit.contacts[0];
        GameObject hole = Instantiate(
            GlobalReferences.Instance.bulletImpactEffectPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)
        );
        hole.transform.SetParent(ObjectHit.gameObject.transform);
    }
}
