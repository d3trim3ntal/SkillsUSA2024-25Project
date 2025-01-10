using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public TextMeshProUGUI timerText;
    public GameObject gameOverPanel;
    public float maxHealth = 100f;
    private float currentHealth;
    public float duration = 10f;

    //Customizable colors
    public Color highHealthColor = Color.green;
    public Color medHealthColor = Color.yellow;
    public Color lowHealthColor = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        gameOverPanel.SetActive(false);
        StartCoroutine(DecreaseHealthOverTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DecreaseHealthOverTime()
    {
        float decrement = maxHealth / duration;
        float elapsedTime = 0f;
        while(currentHealth > 0)
        {
            currentHealth -= decrement;
            elapsedTime += 1f;
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }
            UpdateHealthBar();
            UpdateTimerText(elapsedTime);
            yield return new WaitForSeconds(1f);
        }
        GameOver();
    }

    void UpdateHealthBar()
    {
        float healthPercentage = currentHealth / maxHealth;
        healthBar.fillAmount = healthPercentage;

        if (healthPercentage > 0.5f)
        {
            healthBar.color = highHealthColor;
        }
        else if (healthPercentage > 0.25f)
        {
            healthBar.color = medHealthColor;
        }
        else
        {
            healthBar.color = lowHealthColor;
        }
    }

    void UpdateTimerText(float elapsedTime)
    {
        float remainingTime = duration - elapsedTime;
        timerText.text = "Time: " + remainingTime.ToString("F1") + "s";
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
