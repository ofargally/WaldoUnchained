using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public int MaxHP = 10;
    public bool PlayerWeaponMode = true;
    private int PlayerHP;
    public HitFlashImageController hitFlashController;
    public CameraShake cameraShake;
    public float shakeDuration = 0.1f;
    public float shakeMagnitude = 0.1f;
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

    public void TakeDamage(int damage, Vector3 hitpoint)
    {
        PlayerHP -= damage;
        TriggerHitEffect(hitpoint);
        Debug.Log("PLAYER HP:" + PlayerHP);
    }
    void TriggerHitEffect(Vector3 hitPoint)
    {
        if (hitFlashController != null)
        {
            hitFlashController.TriggerFlash();
        }
        if (cameraShake != null)
        {
            StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude));
        }

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