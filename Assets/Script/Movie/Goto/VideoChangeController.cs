using UnityEngine;
using UnityEngine.Video;
using System.Collections.Generic;

public class VideoChangeController : MonoBehaviour
{
    [SerializeField] private List<VideoClip> videoSorces;
    [SerializeField] private VideoPlayer videoPlayer;

    // 現在再生中のクリップのインデックス
    private int currentClipIndex = 0;

    void Start()
    {
        // 初期設定: 配列の最初の動画を再生するように設定
        if (videoSorces.Count> 0 && videoPlayer != null)
        {
            //videoPlayer.clip = videoSorces[currentClipIndex];
            videoPlayer.Play();
        }
    }

    // ★ この関数をボタンのOnClickイベントに設定します
    public void PlayNextVideo()
    {
        if (videoPlayer == null || videoSorces.Count == 0)
        {
            Debug.LogError("VideoPlayerまたはVideo Clipsが設定されていません。");
            return;
        }

        // 次のクリップのインデックスを計算
        // 配列の最後までいったら0に戻る (ループ)
        currentClipIndex = (currentClipIndex + 1) % videoSorces.Count;

        // VideoPlayerのクリップを新しいものに設定
        videoPlayer.clip = videoSorces[currentClipIndex];

        // 新しいクリップを再生
        videoPlayer.Play();
    }
}
