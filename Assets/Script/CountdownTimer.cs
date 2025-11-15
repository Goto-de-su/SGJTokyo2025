using UnityEngine;
using UnityEngine.UI;
using TMPro;   // << ?????

public class CountdownTimer : MonoBehaviour
{
    [Header("UI ??")]
    public TextMeshProUGUI timerText;   // ?? 00:00 ????TMP ???
    public GameObject gameOverImage; // ?? GameOver ????GameOverImage?
    public GameObject startButton;   // ?????????????

    [Header("????")]
    public float startTime = 60f;    // ?????????? 60 = 01:00

    private float currentTime;
    private bool isRunning = false;

    private void Start()
    {
        // ?????
        currentTime = startTime;
        UpdateTimerText();

        // ????? GameOver ??
        if (gameOverImage != null)
        {
            gameOverImage.SetActive(false);
        }
    }

    private void Update()
    {
        if (!isRunning)
        {
            return;
        }

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            isRunning = false;
            UpdateTimerText();
            ShowGameOver();
        }
        else
        {
            UpdateTimerText();
        }
    }

    // ????? 00:00 ??
    private void UpdateTimerText()
    {
        int totalSeconds = Mathf.CeilToInt(currentTime);
        if (totalSeconds < 0) totalSeconds = 0;

        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        if (timerText != null)
        {
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    // ?? GameOver ??
    private void ShowGameOver()
    {
        if (gameOverImage != null)
        {
            gameOverImage.SetActive(true);
        }
    }

    // ??“????”?????
    public void StartTimer()
    {
        currentTime = startTime;    // ?????????
        isRunning = true;

        // ?????? GameOver ?????????????
        if (gameOverImage != null)
        {
            gameOverImage.SetActive(false);
        }

        // ???????????????
        if (startButton != null)
        {
            startButton.SetActive(false);
        }
    }
}
