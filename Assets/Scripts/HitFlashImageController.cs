using UnityEngine;
using UnityEngine.UI;

public class HitFlashImageController : MonoBehaviour
{
    public Image hitFlashImage;
    public float flashDuration = 0.2f;
    private float flashTimer = 0f;

    void Update()
    {
        if (flashTimer > 0)
        {
            flashTimer -= Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, flashTimer / flashDuration);
            hitFlashImage.color = new Color(1f, 0f, 0f, alpha);
        }
        else
        {
            hitFlashImage.color = new Color(1f, 0f, 0f, 0f);
        }
    }

    public void TriggerFlash()
    {
        flashTimer = flashDuration;
    }
}

