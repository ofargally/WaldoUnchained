using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startText : MonoBehaviour
{
    public GameObject[] texts;
    public int totalTexts;
    public float fasterTime;

    public float timeInterval = 2f;
    public float timeCounter = 0f;
    private int currentText = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        texts[currentText].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        timeCounter += Time.deltaTime;
        if (currentText == 2)
        {
            timeInterval = fasterTime;
        }
        if (timeCounter > timeInterval && currentText < totalTexts)
        {
            texts[currentText].SetActive(false);
            currentText++;
            timeCounter = 0f;
            if (currentText == totalTexts)
            {
                SceneManager.LoadScene(2);
            }
            else
            {
                texts[currentText].SetActive(true);
            }

        }
    }
}
