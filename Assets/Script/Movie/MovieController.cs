using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MovieController : MonoBehaviour
{
    [SerializeField] private float2 aspect;
    [SerializeField] private float scale;
    private VideoPlayer Player;

    private void Start()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        Player = GetComponent<VideoPlayer>();
        gameObject.transform.localScale = new Vector3(aspect.x / aspect.y * scale, scale, 1);
        Player.Prepare();
        Player.prepareCompleted += OnEndPrepare;

    }

    private void OnEndPrepare(VideoPlayer player)
    {
        Player.Play();
    }
}
