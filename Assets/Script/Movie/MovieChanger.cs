using UnityEngine;
using UnityEngine.Video;

public class MovieChanger : MonoBehaviour
{
    [SerializeField] private EmotionControler emotion_controler;
    private GameObject current_movie;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ChangeMovie();
            emotion_controler.UpdateEmotion(1);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ChangeMovie();
            emotion_controler.UpdateEmotion(-1);
        }
    }

    public void ChangeMovie()
    {
        GameObject newClip = emotion_controler.GetMovieClip();
        if(current_movie != null)
        Destroy(current_movie);
        current_movie = Instantiate(newClip, transform.position, Quaternion.identity, transform.parent);
    }
}
