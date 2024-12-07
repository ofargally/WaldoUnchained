using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            print("Hit enemy of name: " + other.gameObject.name);
            Destroy(gameObject);
        }
    }
}
