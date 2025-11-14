using UnityEngine;
using UnityEngine.InputSystem.Controls;


public enum EMOTION
{
    CRAZY,
    HAPPY,
    NEUTRAL,
    SAD,
    ANGRY,
}

public class Statas
{
    public EMOTION CurrentEmotion = EMOTION.NEUTRAL;
}


public class EmotionControler : MonoBehaviour
{
    private int delta;
    private Statas stat = new Statas();

    private void Start()
    {
        stat = GetComponent<Statas>();
        delta = 0;
    }
    public void UpdateEmotion(int delta_) { delta = delta_; }

    private void LateUpdate()
    {
        Mathf.Max((float)EMOTION.ANGRY, (float)stat.CurrentEmotion + delta);
        Mathf.Min((float)EMOTION.CRAZY, (float)stat.CurrentEmotion + delta);
    }
}
