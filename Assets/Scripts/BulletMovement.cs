using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy of name: " + other.gameObject.name);
            EnemyManager enemyManager = other.gameObject.GetComponent<EnemyManager>();
            if (enemyManager != null)
            {
                Debug.Log("Damage Dealt");
                enemyManager.takeDamage();
            }
            Destroy(gameObject);
        }
    }
}
