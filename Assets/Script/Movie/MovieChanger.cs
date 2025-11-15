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

    private void Start()
    {
        ChangeMovie();
        current_movie.GetComponent<VideoPlayer>().loopPointReached += OnMovieEnd;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ChangeMovie();
        }
    }

    public void ChangeMovie()
    {
        GameObject newClip = GetMovieClip(emotion_controler.GetEmotion()).gameObject;
        if (current_movie != null)
        Destroy(current_movie);
        current_movie = Instantiate(newClip, transform.position, Quaternion.identity, transform.parent);
        OnChange?.Invoke();
    }


    private GameObject GetMovieClip(EMOTION emo)
    {
        switch (emo)
        {
            case EMOTION.Play:
                return Play.GetClip();
            case EMOTION.Exp:
                return Exp.GetClip();
            case EMOTION.Relax:
                return Relax.GetClip();
            case EMOTION.Anger:
                return Anger.GetClip();
            case EMOTION.Poop:
                return Poop.GetClip();
        }
        return null;
    }

    private void OnMovieEnd(VideoPlayer vp)
    {
        OnLoop?.Invoke();
    }
}
