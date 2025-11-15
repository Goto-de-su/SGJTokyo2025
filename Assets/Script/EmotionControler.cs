using UnityEngine;
using UnityEngine.InputSystem.Controls;


public enum EMOTION
{
    Play,
    Exp,
    Relax,
    Anger,
    Poop
}

public class Statas
{
    public EMOTION CurrentEmotion = EMOTION.Relax;
}


public class EmotionControler : MonoBehaviour
{
    private int delta;
    private EMOTION stat = new EMOTION();

    private void Start()
    {
        stat = GetComponent<EMOTION>();
        delta = 0;
    }
    public void UpdateEmotion(int delta_) { delta = delta_; }

    private void LateUpdate()
    {
        float tmpEmotion = (float)stat + delta;
        Mathf.Max((float)EMOTION.Anger, tmpEmotion);
        Mathf.Min((float)EMOTION.Play, tmpEmotion);
        stat = (EMOTION)(int)tmpEmotion;
    }
}
