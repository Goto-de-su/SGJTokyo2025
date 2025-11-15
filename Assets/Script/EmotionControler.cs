using System;
using Unity.VisualScripting;
using UnityEngine;


public enum EMOTION
{
    PLAY,
    EXP,
    RELAX,
    POOP,
    ANGER,
}

public class EmotionControler : MonoBehaviour
{
    [SerializeField] public int PLAY;
    [SerializeField] public int EXP;
    [SerializeField] public int RELAX;
    [SerializeField] public int POOP;
    [SerializeField] public int ANGER;

    [SerializeField] public MovieChanger movie_changer;
    [SerializeField] public StreakController streak_controller;


    private int motivaion = 0;
    private EMOTION emotion = new EMOTION();
    private Emotion_StateMachine state = new Emotion_StateMachine();

    [HideInInspector] public Action<int> OnItemUsed;
    [HideInInspector] public Action OnLoop;

    private void Start()
    {
        movie_changer.OnLoop += OnLoop;
        emotion = EMOTION.RELAX;
        state.ChangeState(Emotion_Relax.instance, this);
    }

    public EMOTION GetEmotion() { return emotion; }
    public Emotion_StateMachine GetStateMachine() { return state; }
    public void SetEmotion(EMOTION emo) { emotion = emo; }
    public int GetMotivaion() { return motivaion; }
    public void UpdateMotivation(int amount) { motivaion += amount; }

    public void UpdateEmotion(int amount)
    {
        UpdateMotivation(amount);
        OnItemUsed?.Invoke(amount);
    }

    public void Full()
    {
        Debug.Log("‚¤‚ñ‚¿ó‘Ô");
        GetStateMachine().ChangeState(Emotion_Poop.instance, this);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            UpdateEmotion(1);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            UpdateEmotion(-1);
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            Full();
        }
    }
}

public interface IEmotion
{
    public void Enter(EmotionControler controler);
}

public class  Emotion_StateMachine
{
    private IEmotion state;
    public void ChangeState(IEmotion newState, EmotionControler controler)
    {
        controler.movie_changer.OnLoop -= controler.OnLoop;
        state = newState;
        state.Enter(controler);
        controler.movie_changer.OnLoop += controler.OnLoop;
    }
}

public class Emotion_Relax : IEmotion
{
    public static Emotion_Relax instance = new Emotion_Relax();
    public void Enter(EmotionControler controler)
    {
        controler.SetEmotion(EMOTION.RELAX);
        controler.UpdateMotivation(controler.RELAX);
        controler.movie_changer.ChangeMovie();

        controler.OnLoop = () =>
        {
            controler.GetStateMachine().ChangeState(Emotion_Relax.instance, controler);
            return;
        };
        controler.OnItemUsed = (amount) =>
        {
            if (amount > 0)
            {
                controler.GetStateMachine().ChangeState(Emotion_Exp.instance, controler);
                return;
            }
            if (amount < 0)
            {
                controler.GetStateMachine().ChangeState(Emotion_Anger.instance, controler);
                return;
            }
        };
    }
}

public class Emotion_Exp : IEmotion
{
    public static Emotion_Exp instance = new Emotion_Exp();
    public void Enter(EmotionControler controler)
    {
        controler.SetEmotion(EMOTION.EXP);
        controler.UpdateMotivation(controler.EXP);
        controler.movie_changer.ChangeMovie();

        controler.streak_controller.OnPlay = () =>
        {
            controler.GetStateMachine().ChangeState(Emotion_Play.instance, controler);
            return;
        };

        controler.OnLoop = null;

        controler.OnItemUsed = (amount) =>
        {
            if (amount > 0)
            {
                controler.GetStateMachine().ChangeState(Emotion_Exp.instance, controler);
                return;
            }
            if (amount < 0)
            {
                controler.GetStateMachine().ChangeState(Emotion_Relax.instance, controler);
                return;
            }
        };
    }
}

public class Emotion_Anger : IEmotion
{
    public static Emotion_Anger instance = new Emotion_Anger();
    public void Enter(EmotionControler controler)
    {
        controler.SetEmotion(EMOTION.ANGER);
        controler.UpdateMotivation(controler.ANGER);
        controler.movie_changer.ChangeMovie();

        controler.OnLoop = () =>
        {
            controler.GetStateMachine().ChangeState(Emotion_Relax.instance, controler);
            return;
        };
        controler.OnItemUsed = (amount) =>
        {
            if (amount > 0)
            {
                controler.GetStateMachine().ChangeState(Emotion_Relax.instance, controler);
                return;
            }
            if (amount < 0)
            {
                controler.GetStateMachine().ChangeState(Emotion_Anger.instance, controler);
                return;
            }
        };
    }
}

public class Emotion_Play : IEmotion
{
    public static Emotion_Play instance = new Emotion_Play();
    public void Enter(EmotionControler controler)
    {
        controler.SetEmotion(EMOTION.PLAY);
        controler.UpdateMotivation(controler.PLAY);
        controler.movie_changer.ChangeMovie();

        controler.OnLoop = () =>
        {
            controler.GetStateMachine().ChangeState(Emotion_Relax.instance, controler);
            return;
        };
    }
}

public class Emotion_Poop : IEmotion
{
    public static Emotion_Poop instance = new Emotion_Poop();
    public void Enter(EmotionControler controler)
    {
        controler.SetEmotion(EMOTION.POOP);
        controler.UpdateMotivation(controler.POOP);
        controler.movie_changer.ChangeMovie();

        controler.OnLoop = () =>
        {
            controler.GetStateMachine().ChangeState(Emotion_Relax.instance, controler);
            return;
        };
    }
}