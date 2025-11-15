using UnityEngine;
using TMPro;  // 用 TextMeshProUGUI

public class CountdownManager : MonoBehaviour
{
    public float gameTime = 300f;               // 总时间（秒），现在是 300 = 5 分钟
    public TextMeshProUGUI timerText;           // 你的 TimerText（05:00）
    public GameObject gameOverPanel;            // 我们待会儿把 GameOverImage 拖进来

    private float currentTime;
    private bool isGameOver = false;

    private void Start()
    {
        currentTime = gameTime;
        UpdateTimerText();

        // 一开始先把 GameOver 图片隐藏起来
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (isGameOver) return; // 已经 GameOver 就别再减时间了

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            UpdateTimerText();
            OnTimeUp();  // 时间到
        }
        else
        {
            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        if (timerText == null) return;

        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // 05:00 这种格式
    }

    private void OnTimeUp()
    {
        isGameOver = true;

        // 显示 GameOver 图片
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // 如果你想时间停止（比如暂停玩家动作），可以加上一句：
        // Time.timeScale = 0f;
    }
}
