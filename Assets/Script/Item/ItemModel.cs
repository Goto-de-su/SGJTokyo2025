using UnityEngine;
using System.Collections.Generic;

public class ItemModel : MonoBehaviour
{
    [Tooltip("抽選するデータ")]
    [SerializeField] private List<ItemData> srcData;

    public ItemData GetRandomItem()
    {
        // データが格納されていない場合はnullを返す
        if (this.srcData.Count == 0)
        {
            return null;
        }

        // 乱数で生成したアイテムを返す
        int randomIndex = Random.Range(0, this.srcData.Count);
        return srcData[randomIndex];
    }

    /// <summary>
    /// アイテムを使用 (実際の効果をここに書く)
    /// </summary>
    [System.Obsolete]
    public void UseItem(ItemData itemToUse, int playerId)
    {
        GameManager gameManager = GameManager.Instance;
        gameManager.Players[playerId].EmotionController.UpdateEmotion(itemToUse.motivationValue);
    }
}
