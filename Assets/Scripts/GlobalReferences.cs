using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class GlobalReferences : MonoBehaviour
{
    public static GlobalReferences Instance
    {
        get;
        set;
    }
    public UnityEngine.UI.Image bulletImage;
    public GameObject bulletImpactEffectPrefab;
    public TextMeshProUGUI AmmoDisplay;
    public TextMeshProUGUI HealthDisplay;
    public PlayerManager playerManager;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void SetBulletImageColorRedTemporary()
    {
        StartCoroutine(ChangeColorRoutine());
    }

    private IEnumerator ChangeColorRoutine()
    {
        bulletImage.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        bulletImage.color = Color.white;
    }
}