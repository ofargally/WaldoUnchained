using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    void Awake()
    {
        // If an instance already exists, destroy the new one
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Make this instance persistent
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
