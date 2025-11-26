using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class MovieChanger : MonoBehaviour
{
    [SerializeField] private EmotionControler emotion_controler;
    [SerializeField] private VideoPlayer videoPlayer;
    private GameObject current_movie;
    public Action OnChange;
    public Action OnLoop;

    [SerializeField] private MoviePool Play;
    [SerializeField] private MoviePool Exp;
    [SerializeField] private MoviePool Relax;
    [SerializeField] private MoviePool Anger;
    [SerializeField] private MoviePool Poop;

    private void Start()
    {
        ChangeMovie();
    }

    public void ChangeMovie()
    {
        VideoClip newClip = GetMovieClip(emotion_controler.GetEmotion());        
        videoPlayer.clip = newClip;
        videoPlayer.loopPointReached += OnMovieEnd;
        OnChange?.Invoke();
    }


    private VideoClip GetMovieClip(EMOTION emo)
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
