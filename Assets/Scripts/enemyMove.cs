using UnityEngine;

public class enemyMove : MonoBehaviour
{
    public float movementSpeed;

    private float movedDistance;

    public float range = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movementSpeed /= 60f;

    }

    // Update is called once per frame
    void Update()
    {
        if (movedDistance > range || movedDistance < (0 - range))
        {
            movementSpeed = 0 - movementSpeed;
            movedDistance = 0;
        }
        transform.position += new Vector3(0f, 0f, movementSpeed);
        movedDistance += movementSpeed;
    }
}
