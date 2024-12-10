using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int EnemyHP = 5;
    void Update()
    {
        if (EnemyHP <= 0)
        {
            // Disable enemy AI
            EnemyAI enemyAI = GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.enemyDisabled = true;
            }
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                Destroy(agent);
            }
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = gameObject.AddComponent<Rigidbody>();
            }
            rb.isKinematic = false;

            // Check if the game object has the "WALDO" tag
            if (gameObject.CompareTag("WALDO"))
            {
                // Apply player win condition
                GlobalReferences.Instance.playerManager.Win();
                Destroy(gameObject);
            }
            else
            {
                // Destroy the game object after 5 seconds

                Destroy(gameObject, 5f);
            }
        }
    }
    // Apply a diagonal force to the enemy to make it fall


    public void TakeDamage(int damage)
    {
        if (EnemyHP <= 0)
        {
            return;
        }
        EnemyHP -= damage;
        Debug.Log("CURRENT HP: " + EnemyHP);
    }
}
