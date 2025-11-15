using UnityEngine;
using UnityEngine.UI;

public class ItemBoxDisplay : MonoBehaviour
{
    [Tooltip("アイテムボックスのスプライトレンダラー")]
    [SerializeField] private Image image;

    /// <summary>
    /// アイテムボックスにアイテム表示
    /// </summary>
    public void DisplayItem(ItemData inputData)
    {
        this.image.sprite = inputData.icon;
    }
}