using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int EnemyHP;
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
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                // Apply player win condition
                SceneManager.LoadSceneAsync(4);
                Destroy(gameObject);
            }
            else
            {
                // Destroy the game object after 5 seconds

                Destroy(gameObject, 2f);
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
