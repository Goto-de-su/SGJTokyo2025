using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    [SerializeField] private string sceneName = "Main";

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(this.gameObject);
    }

    public void LoadSceneOnClick()
    {
        SceneManager.LoadScene(sceneName);
    }
}
