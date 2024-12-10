using UnityEngine;
using UnityEngine.SceneManagement;

public class startToIntro : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadSceneAsync(1);
        }

    }
}
