using UnityEngine;
using UnityEngine.SceneManagement;


public class restart : MonoBehaviour
{
    public void LoadFirstScene()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void LoadPlayScene()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
