using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class MovieChanger : MonoBehaviour
{
    [SerializeField] private EmotionControler emotion_controler;
    private GameObject current_movie;
    public Action OnChange;
    public Action OnLoop;

    [SerializeField] private MoviePool Play;
    [SerializeField] private MoviePool Exp;
    [SerializeField] private MoviePool Relax;
    [SerializeField] private MoviePool Anger;
    [SerializeField] private MoviePool Poop;

    [SerializeField] private RenderTexture renderTexture;

    private void Start()
    {
        ChangeMovie();
    }

    public void ChangeMovie()
    {
        GameObject newClip = GetMovieClip(emotion_controler.GetEmotion()).gameObject;
        if (current_movie != null)
        Destroy(current_movie);
        current_movie = Instantiate(newClip, transform.position, Quaternion.identity, transform.parent);
        current_movie.GetComponent<VideoPlayer>().loopPointReached += OnMovieEnd;
        current_movie.GetComponent<VideoPlayer>().targetTexture = renderTexture;
        OnChange?.Invoke();
    }


    private GameObject GetMovieClip(EMOTION emo)
    {
        switch (emo)
        {
            case EMOTION.PLAY:
                return Play.GetClip();
            case EMOTION.EXP:
                return Exp.GetClip();
            case EMOTION.RELAX:
                return Relax.GetClip();
            case EMOTION.ANGER:
                return Anger.GetClip();
            case EMOTION.POOP:
                return Poop.GetClip();
        }
        return null;
    }

    private void OnMovieEnd(VideoPlayer vp)
    {
        OnLoop?.Invoke();
    }
}
