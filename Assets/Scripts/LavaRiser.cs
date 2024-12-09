using UnityEngine;

public class LavaRiser : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float riseSpeed = 0.5f; // units per second
    public float startDelay = 5f;  // delay before lava starts rising

    private float startTime;
    void Start()
    {
        startTime = Time.time + startDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= startTime)
        {
            Vector3 newPos = transform.position;
            newPos.y += riseSpeed * Time.deltaTime;
            transform.position = newPos;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("PLAYER HIT BY LAVA");
            other.gameObject.GetComponent<PlayerManager>().Die();
        }
    }
}
