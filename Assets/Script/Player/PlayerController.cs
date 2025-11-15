using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("アイテム")]
    [SerializeField] private ItemController itemController;
    [Header("サル")]
    [SerializeField] private EmotionControler emotionController;
    [Header("コントローラー")]
    [SerializeField] private InputModule inputModule;

    public ItemController ItemController => itemController;

    public EmotionControler EmotionController => emotionController;

    /// <summary>
    /// プレイヤー番号
    /// </summary>
    private int id;

    public PlayerController(int number)
    {
        this.id = number;
    }
}
