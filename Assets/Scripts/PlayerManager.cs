using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public int MaxHP = 10;
    public bool PlayerWeaponMode = true;
    private int PlayerHP;
    void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        PlayerHP = MaxHP;
    }

    void Update()
    {
        //Activate Or Deactivate Weapon Mode
        SwitchWeaponMode();
        if (PlayerHP <= 0)
        {
            Debug.Log("PLAYER LOST!");
            Die();
        }
        if (GlobalReferences.Instance.HealthDisplay != null)
        {
            GlobalReferences.Instance.HealthDisplay.text = $"HP: {PlayerHP}/{MaxHP}";
        }
    }

    public void TakeDamage(int damage)
    {
        PlayerHP -= damage;
        Debug.Log("PLAYER HP:" + PlayerHP);
    }

    public void Die()
    {
        Debug.Log("PLAYER DEATH");
        LoadDeathScene();
    }

    void RestartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    void LoadDeathScene()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        UnityEngine.SceneManagement.SceneManager.LoadScene(3); // Death scene
    }

    void SwitchWeaponMode()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerWeaponMode = !PlayerWeaponMode;
        }
    }

    public void Win()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        UnityEngine.SceneManagement.SceneManager.LoadScene(4); // Win scene
    }
}