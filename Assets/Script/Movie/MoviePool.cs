using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "MoviePool", menuName = "Scriptable Objects/MoviePool")]
public class MoviePool : ScriptableObject
{
    [SerializeField] private GameObject[] screen;

    public GameObject GetClip()
    {
        if (screen.Length == 0)
        {
            return null;
        }
        int index = Random.Range(0, screen.Length);
        return screen[index];
    }
}
