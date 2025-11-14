using UnityEngine;

public class ItemController : MonoBehaviour
{
    [Header("Module")]
    [SerializeField] private ItemModel ItemModel;

    [Header("View")]
    [SerializeField] private ItemBoxDisplay itemBoxDisplay;

    private void OnEnable()
    {
        // InputManagerが発火するイベントを購読する
        InputModule.OnOKPressed += UseItem;
        InputModule.OnNGPressed += ClearItem;
    }

    private void OnDisable()
    {
        // 忘れずに購読を解除する
        InputModule.OnOKPressed -= UseItem;
        InputModule.OnNGPressed -= ClearItem;
    }

    /// <summary>
    /// アイテムを使用
    /// </summary>
    public void UseItem()
    {
        ItemModel.UseItem();
        this.ChangeItem();
    }

    /// <summary>
    /// アイテムを使用せずスキップ
    /// </summary>
    public void ClearItem()
    {
        this.ChangeItem();
    }


    /// <summary>
    /// アイテム変更
    /// </summary>
    public void ChangeItem()
    {
        ItemModel.SpownRandomItem();
        itemBoxDisplay.DisplayItem(ItemModel.NowItem);
    }
}
