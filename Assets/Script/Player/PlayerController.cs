using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("�A�C�e��")]
    [SerializeField] private ItemController itemController;
    [Header("�T��")]
    [SerializeField] private EmotionControler emotionController;
    [Header("�R���g���[���[")]
    [SerializeField] private InputModule inputModule;

    [Header("Gauges")]
    [SerializeField] private FullnessGauge fullnessGauge;


    public ItemController ItemController => itemController;

    public EmotionControler EmotionController => emotionController;

    public FullnessGauge FullnessGauge => fullnessGauge;

    /// <summary>
    /// �v���C���[�ԍ�
    /// </summary>
    [Header("�v���C���[ID")]
    [SerializeField] private int id;

    public PlayerController(int number)
    {
        this.id = number;
    }

    public void Start()
    {
        itemController.SetPlayerId(id);
    }
}
