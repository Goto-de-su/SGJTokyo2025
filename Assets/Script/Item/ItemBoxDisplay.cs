using UnityEngine;

public class ItemBoxDisplay : MonoBehaviour
{
    [Tooltip("アイテムボックスのスプライトレンダラー")]
    [SerializeField] private SpriteRenderer itemSpriteRenderer;

    /// <summary>
    /// アイテムボックスにアイテム表示
    /// </summary>
    public void DisplayItem(ItemData inputData)
    {
        this.itemSpriteRenderer.sprite = inputData.icon;
    }
}