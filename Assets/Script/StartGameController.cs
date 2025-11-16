using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameController : MonoBehaviour
{
    // 在 Inspector 里填你游戏场景的名字，比如 "GameScene"
    public string gameSceneName = "GameScene";

    public void OnStartButtonClicked()
    {
        // 点击 Start 按钮后，加载游戏场景
        SceneManager.LoadScene(gameSceneName);
    }
}
