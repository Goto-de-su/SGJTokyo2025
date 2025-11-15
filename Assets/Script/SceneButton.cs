using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneButton : MonoBehaviour
{
    [SerializeField] private string sceneName = "Main";
    [SerializeField] private AudioSource clickSound;

    void Start()
    {
        // 这一行不是必须的，只是默认选中这个按钮
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(this.gameObject);
        }
    }

    // Button 的 OnClick 调用这个函数
    public void OnButtonClicked()
    {
        StartCoroutine(PlaySoundAndLoad());
    }

    private IEnumerator PlaySoundAndLoad()
    {
        // 播放点击音
        if (clickSound != null && clickSound.clip != null)
        {
            clickSound.Play();

            // 这里用“真实时间”来等待，不受 Time.timeScale 影响
            yield return new WaitForSecondsRealtime(clickSound.clip.length);
            // 如果不想等完整个长度，也可以写死一个很短的时间，例如：
            // yield return new WaitForSecondsRealtime(0.2f);
        }

        // 为了调试，先打印一下
        Debug.Log("Load scene: " + sceneName);

        // 切换场景
        SceneManager.LoadScene(sceneName);
    }
}
