using UnityEngine;
using UnityEngine.UI;

public class EmotionViewer : MonoBehaviour
{
    [SerializeField] private RawImage bad;
    [SerializeField] private RawImage normal;
    [SerializeField] private RawImage good;
    [SerializeField] private Texture play;

    public void DisplayEmotion(EMOTION emotion)
    {
        switch (emotion)
        {
            case EMOTION.ANGER:
                DisplayIcon(bad);
                HideIcon(good);
                HideIcon(normal);
                break;
            case EMOTION.RELAX:
                DisplayIcon(normal);
                HideIcon(good);
                HideIcon(bad);
                break;
            case EMOTION.EXP:
                DisplayIcon(good);
                HideIcon(normal);
                HideIcon(bad);
                break;
            case EMOTION.PLAY:
                ChangeSprite(normal, play);
                DisplayIcon(normal);
                HideIcon(good);
                HideIcon(bad);
                break;
            case EMOTION.POOP:
                DisplayIcon(bad);
                HideIcon(good);
                HideIcon(normal);
                break;
        }

    }

    private void ChangeSprite(RawImage image, Texture texture)
    {
        image.texture = texture;
    }

    private void DisplayIcon(RawImage image)
    {
        Color tmpColor = image.color;
        tmpColor.a = 1f;
        image.color = tmpColor;
    }

    private void HideIcon(RawImage image)
    {
        Color tmpColor = image.color;
        tmpColor.a = 0f;
        image.color = tmpColor;
    }
}
