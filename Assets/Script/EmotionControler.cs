using UnityEngine;
using UnityEngine.Video;


public enum EMOTION
{
    Play,
    Exp,
    Relax,
    Anger,
    Poop
}



public class EmotionControler : MonoBehaviour
{
    private int delta;
    private EMOTION stat = new EMOTION();

    [SerializeField] private MoviePool Play;
    [SerializeField] private MoviePool Exp;
    [SerializeField] private MoviePool Relax;
    [SerializeField] private MoviePool Anger;
    [SerializeField] private MoviePool Poop;

    private void Start()
    {
        stat = EMOTION.Play;
        delta = 0;
    }
    public void UpdateEmotion(int delta_) { delta = delta_; }

    private void LateUpdate()
    {
        float tmpEmotion = (float)stat + delta;
        tmpEmotion = Mathf.Clamp(tmpEmotion, (float)EMOTION.Play, (float)EMOTION.Poop);
        stat = (EMOTION)(int)tmpEmotion;
        delta = 0;
    }


    public GameObject GetMovieClip()
    {
        Debug.Log(stat);

        return stat switch
        {
            EMOTION.Play => Play.GetClip(),
            EMOTION.Exp => Exp.GetClip(),
            EMOTION.Relax => Relax.GetClip(),
            EMOTION.Anger => Anger.GetClip(),
            EMOTION.Poop => Poop.GetClip(),
        };
    }
}
