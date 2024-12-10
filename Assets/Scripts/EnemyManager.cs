using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int EnemyHP = 5;
    void Update()
    {
        if (EnemyHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        EnemyHP -= damage;
        Debug.Log("CURRENT HP: " + EnemyHP);
    }
}
