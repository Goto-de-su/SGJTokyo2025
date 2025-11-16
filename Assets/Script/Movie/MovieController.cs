using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MovieController : MonoBehaviour
{
    [SerializeField] private float2 aspect;
    [SerializeField] private float scale;
    private VideoPlayer Player;

    [SerializeField] private RawImage rawImage;       // UI の RawImage
    [HideInInspector] public RenderTexture renderTex; // 出力先の RenderTexture

    private void Start()
    {
        Player = GetComponent<VideoPlayer>();

        // アスペクト比に応じて RawImage のサイズを調整
        rawImage.rectTransform.sizeDelta = new Vector2(aspect.x * scale, aspect.y * scale);

        // AudioSource 設定
        AudioSource audio = GetComponent<AudioSource>();
        Player.audioOutputMode = VideoAudioOutputMode.AudioSource;
        Player.SetTargetAudioSource(0, audio);

        Player.targetTexture = renderTex;

        // 出力先を RenderTexture に設定
        Player.targetTexture = renderTex;
        rawImage.texture = renderTex;

        Player.Prepare();
        Player.prepareCompleted += OnEndPrepare;
    }

    private void OnEndPrepare(VideoPlayer player)
    {
        rawImage.enabled = true; // RawImage を表示
        Player.Play();
    }
}