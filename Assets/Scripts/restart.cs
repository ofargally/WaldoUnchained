using UnityEngine;
using UnityEngine.SceneManagement;


public class restart : MonoBehaviour
{
    public void LoadFirstScene()
    {
        SceneManager.LoadSceneAsync(0);
    }

}
