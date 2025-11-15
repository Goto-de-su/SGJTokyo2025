using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Video;

public class StreakController : MonoBehaviour
{
    [SerializeField] private EmotionControler emotion_controller;
    [SerializeField] private MovieChanger movie_changer;

    [SerializeField] private int[] thresholds;
    [SerializeField] private int streak_increment;

    private int current_streak = 0;
    private int current_emotion_up = 1;

    private void Start()
    {
        movie_changer.OnChange += OnMovieChange;
        movie_changer.OnLoop += OnMovieEnd;
    }

    private void OnMovieEnd()
    {
        if (emotion_controller.GetEmotion() == EMOTION.Exp)
        {
            current_streak++;

            int inc = 0;

            foreach (int threshold in thresholds)
            {
                if (current_streak > threshold)
                {
                    inc += streak_increment;
                }
            }

            emotion_controller.UpdateEmotion(current_emotion_up + inc);
        }
    }

    private void OnMovieChange()
    {
        current_streak = 0;
    }
}
