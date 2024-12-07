using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int PlayerHP = 10;

    // Update is called once per frame
    void Update()
    {
        if (PlayerHP <= 0)
        {
            Debug.Log("PLAYER LOST!");
            RestartScene();
        }
    }

    private bool isKnockedBack = false;
    public float knockbackDuration = 0.2f;

    public void takeDamage()
    {
        PlayerHP--;
        if (!isKnockedBack)
        {
            StartCoroutine(KnockbackCoroutine());
        }
        Debug.Log("PLAYER HP:" + PlayerHP);
    }

    private IEnumerator KnockbackCoroutine()
    {
        isKnockedBack = true;
        Vector3 shootingDirection = -transform.forward;
        float initialSpeed = 20000f;
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        float elapsedTime = 0f;
        while (elapsedTime < knockbackDuration)
        {
            float currentForce = Mathf.Lerp(initialSpeed, 0f, elapsedTime / knockbackDuration);
            rb.AddForce(shootingDirection * currentForce * Time.deltaTime, ForceMode.Impulse);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isKnockedBack = false;
    }

    void RestartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
