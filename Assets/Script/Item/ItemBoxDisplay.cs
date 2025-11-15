using UnityEngine;
using UnityEngine.UI;

public class ItemBoxDisplay : MonoBehaviour
{
    [Tooltip("アイテムボックスのスプライトレンダラー")]
    [SerializeField] private Image image;

    public void DisplayItem(ItemData inputData)
    {
        if (inputData != null && inputData.icon != null)
        {
            this.image.sprite = inputData.icon;
            this.image.enabled = true; // 画像を表示
        }
        else
        {
            // データがnullなら非表示
            ClearDisplay();
        }
    }

    /// <summary>
    /// 表示をクリア（非表示に）
    /// </summary>
    public void ClearDisplay()
    {
        this.image.sprite = null;
        this.image.enabled = false; // 画像を非表示
    }
}