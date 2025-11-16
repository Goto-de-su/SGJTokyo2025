using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [Header("Module")]
    [SerializeField] private ItemModel itemModel;
    [SerializeField] private InputModule inputModel;

    [Header("View")]
    [Tooltip("アイテム侮ｦスロット（左から順に3つ設定）")]
    [SerializeField] private List<ItemBoxDisplay> itemBoxDisplays;

    [Header("Controller")]
    [SerializeField] private CursorController cursorController;

    [Header("Settings")]
    [Tooltip("アイテムの所持枠の数")]
    [SerializeField] private int itemBoxSize = 3;

    [Header("Gauges")]
    [SerializeField] private FullnessGauge fullnessGauge;

    private int playerId;

    // 現在所持しているアイテムのキュー（リスト）
    private List<ItemData> currentItems = new List<ItemData>();

    public void SetPlayerId(int id)
    {
        this.playerId = id;
        this.selectedPlayer = this.playerId;
        DisplayCursor();
    }

    /// <summary>
    /// アイテムの効果先
    /// </summary>
    private int selectedPlayer;

    private void Start()
    {
        // ゲーム開始時に所持枠の数だけアイテムを初期化
        InitializeItemBox();
        //cursorController.UpdateCursor(0, this.selectedPlayer);
    }

    //private void Update()
    //{
    //    Debug.Log("操作プレイヤー:" + playerId + ", 選択プレイヤー:" + selectedPlayer);
    //}

    private void OnEnable()
    {
        // (省略... InputModuleの購読はそのまま)
        inputModel.OnOKPressed += UseItem;
        inputModel.OnNGPressed += ClearItem;
        inputModel.OnRightPressed += SelectRight;
        inputModel.OnLeftPressed += SelectLeft;
    }

    private void OnDisable()
    {
        // (省略... 購読解除はそのまま)
        inputModel.OnOKPressed -= UseItem;
        inputModel.OnNGPressed -= ClearItem;
        inputModel.OnRightPressed += SelectRight;
        inputModel.OnLeftPressed += SelectLeft;
    }

    /// <summary>
    /// 最初にアイテムボックスを埋める
    /// </summary>
    private void InitializeItemBox()
    {
        currentItems.Clear();
        for (int i = 0; i < itemBoxSize; i++)
        {
            // 新しいアイテムを抽選してリストの「最後」に追加
            currentItems.Add(itemModel.GetRandomItem());
        }

        // UIを更新
        UpdateAllDisplays();
    }

    /// <summary>
    /// 一番左のアイテムを使用
    /// </summary>
    [System.Obsolete]
    public void UseItem()
    {
        if (currentItems.Count == 0) return;

        // 1. 使用するアイテムのデータを取得
        ItemData itemToUse = currentItems[0];

        // 2. 実際のアイテム使用ロジックを呼ぶ (EmotionControllerなど)
        itemModel.UseItem(itemToUse, selectedPlayer);

        // 3. 満腹ゲージを増やす処理 (ClearItemから移動)
        if (itemToUse != null)
        {
            int value = itemToUse.foodValue;

            if (value == 1)
            {
                if (fullnessGauge != null)
                {
                    fullnessGauge.IncrementSteps(1);
                }
            }

            if (value == 2)
            {
                if (fullnessGauge != null)
                {
                    fullnessGauge.IncrementSteps(2);
                }
            }
        }

        // 4. アイテムを変更（左詰め＆補充）
        this.ChangeItem();
    }


    /// <summary>
    /// アイテムを使用せずスキップ（一番左を破棄）
    /// </summary>
    public void ClearItem()
    {
        if (currentItems.Count == 0) return;

       
        this.ChangeItem();
    }

    /// <summary>
    /// アイテム変更（左詰めにして、一番右に補充）
    /// </summary>
    public void ChangeItem()
    {
        if (currentItems.Count == 0)
        {
            // もし空なら、初期化を試みる
            InitializeItemBox();
            return;
        }

        // 1. 一番左(インデックス0)のアイテムを削除
        //    -> これで自動的にリストが左詰めされます
        currentItems.RemoveAt(0);

        // 2. 新しいアイテムを抽選してリストの「最後」に追加
        currentItems.Add(itemModel.GetRandomItem());

        // 3. UIの表示をすべて更新
        UpdateAllDisplays();
    }

    /// <summary>
    /// currentItemsリストの内容を、すべてのUIスロットに反映
    /// </summary>
    private void UpdateAllDisplays()
    {
        // 3つの表示スロットを順番に処理
        for (int i = 0; i < itemBoxDisplays.Count; i++)
        {
            // currentItemsに表示すべきアイテムがあるか？
            if (i < currentItems.Count)
            {
                // あれば表示
                itemBoxDisplays[i].DisplayItem(currentItems[i]);
            }
            else
            {
                // なければクリア
                itemBoxDisplays[i].ClearDisplay();
            }
        }
    }

    /// <summary>
    /// アイテム使用先のカーソル制御(右)
    /// </summary>
    private void SelectRight()
    {
        Debug.Log("変更前");
        Debug.Log("操作プレイヤー:" + playerId + ", 選択プレイヤー:" + selectedPlayer);
        selectedPlayer++;
        Debug.Log("計算後");
        Debug.Log("操作プレイヤー:" + playerId + ", 選択プレイヤー:" + selectedPlayer);
        if (selectedPlayer > 2)
        {
            playerId = 0;
        }
        Debug.Log("変更後");
        Debug.Log("操作プレイヤー:" + playerId + ", 選択プレイヤー:" + selectedPlayer);
        DisplayCursor();
    }

    /// <summary>
    /// アイテム使用先のカーソル制御(左)
    /// </summary>
    private void SelectLeft()
    {
        Debug.Log("変更前");
        Debug.Log("操作プレイヤー:" + playerId + ", 選択プレイヤー:" + selectedPlayer);
        selectedPlayer--;
        Debug.Log("計算後");
        Debug.Log("操作プレイヤー:" + playerId + ", 選択プレイヤー:" + selectedPlayer);
        if (selectedPlayer < 0)
        {
            selectedPlayer = 2;
        }
        Debug.Log("変更後");
        Debug.Log("操作プレイヤー:" + playerId + ", 選択プレイヤー:" + selectedPlayer);
        DisplayCursor();
    }

    private void DisplayCursor()
    {
        //Debug.Log("操作プレイヤー:" + playerId + ", 選択プレイヤー:" + selectedPlayer);
        cursorController.UpdateCursor(selectedPlayer, playerId);
    }
}