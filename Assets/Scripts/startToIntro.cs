using UnityEngine;
using UnityEngine.SceneManagement;

public class startToIntro : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadSceneAsync(1);
        }

    }
}
