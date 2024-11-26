using UnityEngine;

public class HumanSpawner : MonoBehaviour
{
    public GameObject[] myObjects;

    public int numHuman;

    private void Start()
    {
        for(int i = 0; i < numHuman; i++)
        {
            int randomIndex = Random.Range(0, myObjects.Length);
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-10f, 10f), 2, Random.Range(-10f, 10f));
            Instantiate(myObjects[randomIndex], randomSpawnPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
