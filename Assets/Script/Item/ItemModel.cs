using UnityEngine;
using System.Collections.Generic;

public class ItemModel : MonoBehaviour
{
    [Tooltip("抽選するデータ")]
    [SerializeField] private List<ItemData> srcData;

    /// <summary>
    /// 現在のアイテム
    /// </summary>
    private ItemData nowItem;
    public ItemData NowItem => nowItem;

    /// <summary>
    /// アイテムをスポーン
    /// </summary>
    public void SpownRandomItem()
    {
        // データが格納されていない場合はスポーンしない
        if (this.srcData.Count == 0)
        {
            return;
        }

        // 乱数で生成したアイテムを保持
        int randomIndex = Random.Range(0, this.srcData.Count);
        nowItem = srcData[randomIndex];
    }

    /// <summary>
    /// アイテムを使用
    /// </summary>
    public void UseItem()
    {

    }
}
