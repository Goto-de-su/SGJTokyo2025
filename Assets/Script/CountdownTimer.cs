using UnityEngine;
using TMPro;  // 用 TextMeshProUGUI
using UnityEngine.SceneManagement;  // ★ 新增：为了 LoadScene
public class CountdownManager : MonoBehaviour
{
    public float gameTime = 300f;               // 总时间（秒）
    public TextMeshProUGUI timerText;           // 05:00 的文本
    public GameObject gameOverPanel;            // 如果你还想在本场景里弹出一张图，可以用它
    public string gameOverSceneName = "GameOver";  // ★ 新增：GameOver 场景的名字

    private float currentTime;
    private bool isGameOver = false;

    private void Start()
    {
        currentTime = gameTime;
        UpdateTimerText();

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (isGameOver) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            UpdateTimerText();
            OnTimeUp();
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
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void OnTimeUp()
    {
        isGameOver = true;

        // 如果你想在当前场景先显示一下 GameOver 图片，可以保留这几行
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // ★ 关键：加载 GameOver 场景
        SceneManager.LoadScene(gameOverSceneName);

        // 如果之前用了 Time.timeScale = 0f; 记得在 GameOver 场景 Start 里改回 1f
    }
}
