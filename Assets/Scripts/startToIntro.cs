using UnityEngine;
using UnityEngine.SceneManagement;

public class startToIntro : MonoBehaviour
{
    public void LoadIntroScene()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
