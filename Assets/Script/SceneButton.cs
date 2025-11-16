using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneButton : MonoBehaviour
{
    [SerializeField] private string sceneName = "Main";
    [SerializeField] private AudioSource clickSound; // 拖这里

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(this.gameObject);
    }

    public void LoadSceneOnClick()
    {
        StartCoroutine(PlaySoundAndLoad());
    }

    private IEnumerator PlaySoundAndLoad()
    {
        if (clickSound != null)
        {
            clickSound.Play();
            yield return new WaitForSeconds(clickSound.clip.length);
        }
        SceneManager.LoadScene(sceneName);
    }
}
