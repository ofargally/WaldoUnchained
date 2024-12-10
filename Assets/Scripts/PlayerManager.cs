using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public int MaxHP, PlayerHP = 10;
    public bool PlayerWeaponMode = true;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
    void Update()
    {
        //Activate Or Deactivate Weapon Mode
        SwitchWeaponMode();
        if (PlayerHP <= 0)
        {
            Debug.Log("PLAYER LOST!");
            RestartScene();
        }
        if (GlobalReferences.Instance.HealthDisplay != null)
        {
            GlobalReferences.Instance.HealthDisplay.text = $"{PlayerHP}/{MaxHP}";
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
        RestartScene();
    }

    void RestartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    void SwitchWeaponMode()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerWeaponMode = !PlayerWeaponMode;
        }
    }
}
