using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance
    {
        get;
        set;
    }
    public AudioSource shootingSound;
    public AudioSource reloadingSound;
    public AudioSource emptyMagazine;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}