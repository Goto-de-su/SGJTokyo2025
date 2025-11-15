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
    private EMOTION emotion = new EMOTION();

    private void Start()
    {
        emotion = EMOTION.Exp;
    }

    public EMOTION GetEmotion() { return emotion; }

    public void UpdateEmotion(int emo)
    {
        Debug.Log("Emotion Update: " + emo);
    }
}
