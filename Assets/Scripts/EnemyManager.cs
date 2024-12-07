using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int EnemyHP = 5;

    // Update is called once per frame

    void Start()
    {
        Debug.Log("CURRENT HP: " + EnemyHP);
    }
    void Update()
    {
        if (EnemyHP <= 0)
        {
            Debug.Log("EnemyHP LOST!");
            Destroy(gameObject);
        }
    }

    public void takeDamage()
    {
        EnemyHP--;
        Debug.Log("CURRENT HP: " + EnemyHP);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerManager>().takeDamage();
        }
    }
}
