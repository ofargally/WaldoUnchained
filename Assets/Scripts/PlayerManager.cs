using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int PlayerHP = 10;

    // Update is called once per frame

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
    void Update()
    {
        //Activate Or Deactivate Weapon Mode
        if (Input.GetKeyDown(KeyCode.F))
        {
            GlobalReferences.Instance.PlayerWeaponMode = !GlobalReferences.Instance.PlayerWeaponMode;
        }
        if (PlayerHP <= 0)
        {
            Debug.Log("PLAYER LOST!");
            RestartScene();
        }
    }
    public void takeDamage()
    {
        PlayerHP--;
        Debug.Log("PLAYER HP:" + PlayerHP);
    }

    public void Die()
    {
        Debug.Log("PLAYER DEATH");
        //Could add some effect here later, should also provide a rumbling sound for lava
        RestartScene();
    }

    void RestartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
