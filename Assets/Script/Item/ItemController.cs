using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [Header("Module")]
    [SerializeField] private ItemModel itemModel;
    [SerializeField] private InputModule inputModel;

    [Header("View")]
    [Tooltip("�A�C�e������X���b�g�i�����珇��3�ݒ�j")]
    [SerializeField] private List<ItemBoxDisplay> itemBoxDisplays;

    [Header("Controller")]
    [SerializeField] private CursorController cursorController;

    [Header("Settings")]
    [Tooltip("�A�C�e���̏����g�̐�")]
    [SerializeField] private int itemBoxSize = 3;

    private int playerId;

    // ���ݏ������Ă���A�C�e���̃L���[�i���X�g�j
    private List<ItemData> currentItems = new List<ItemData>();

    public void SetPlayerId(int id)
    {
        this.playerId = id;
        this.selectedPlayer = this.playerId;
        DisplayCursor(selectedPlayer, playerId);
    }

    /// <summary>
    /// �A�C�e���̌��ʐ�
    /// </summary>
    private int selectedPlayer;

    private void Start()
    {
        // �Q�[���J�n���ɏ����g�̐������A�C�e����������
        InitializeItemBox();
        //cursorController.UpdateCursor(0, this.selectedPlayer);
    }

    //private void Update()
    //{
    //    Debug.Log("����v���C���[:" + playerId + ", �I���v���C���[:" + selectedPlayer);
    //}

    private void OnEnable()
    {
        // (�ȗ�... InputModule�̍w�ǂ͂��̂܂�)
        inputModel.OnOKPressed += UseItem;
        inputModel.OnNGPressed += ClearItem;
        // �Ȃ������]���Ă��邽�߁A���]
        inputModel.OnRightPressed += SelectLeft;
        inputModel.OnLeftPressed += SelectRight;
    }

    private void OnDisable()
    {
        // (�ȗ�... �w�ǉ����͂��̂܂�)
        inputModel.OnOKPressed -= UseItem;
        inputModel.OnNGPressed -= ClearItem;
        // �Ȃ������]���Ă��邽�߁A���]
        inputModel.OnRightPressed += SelectLeft;
        inputModel.OnLeftPressed += SelectRight;
    }

    /// <summary>
    /// �ŏ��ɃA�C�e���{�b�N�X�𖄂߂�
    /// </summary>
    private void InitializeItemBox()
    {
        currentItems.Clear();
        for (int i = 0; i < itemBoxSize; i++)
        {
            // �V�����A�C�e���𒊑I���ă��X�g�́u�Ō�v�ɒǉ�
            currentItems.Add(itemModel.GetRandomItem());
        }

        // UI���X�V
        UpdateAllDisplays();
    }

    /// <summary>
    /// ��ԍ��̃A�C�e�����g�p
    /// </summary>
    [System.Obsolete]
    public void UseItem()
    {
        if (currentItems.Count == 0) return;

        // 1. �g�p����A�C�e���̃f�[�^���擾
        ItemData itemToUse = currentItems[0];

        // 2. ���ۂ̃A�C�e���g�p���W�b�N���Ă� (EmotionController�Ȃ�)
        itemModel.UseItem(itemToUse, selectedPlayer);

        // 4. �A�C�e����ύX�i���l�߁���[�j
        this.ChangeItem();
    }


    /// <summary>
    /// �A�C�e�����g�p�����X�L�b�v�i��ԍ���j���j
    /// </summary>
    public void ClearItem()
    {
        if (currentItems.Count == 0) return;

       
        this.ChangeItem();
    }

    /// <summary>
    /// �A�C�e���ύX�i���l�߂ɂ��āA��ԉE�ɕ�[�j
    /// </summary>
    public void ChangeItem()
    {
        if (currentItems.Count == 0)
        {
            // ������Ȃ�A�����������݂�
            InitializeItemBox();
            return;
        }

        // 1. ��ԍ�(�C���f�b�N�X0)�̃A�C�e�����폜
        //    -> ����Ŏ����I�Ƀ��X�g�����l�߂���܂�
        currentItems.RemoveAt(0);

        // 2. �V�����A�C�e���𒊑I���ă��X�g�́u�Ō�v�ɒǉ�
        currentItems.Add(itemModel.GetRandomItem());

        // 3. UI�̕\�������ׂčX�V
        UpdateAllDisplays();
    }

    /// <summary>
    /// currentItems���X�g�̓��e���A���ׂĂ�UI�X���b�g�ɔ��f
    /// </summary>
    private void UpdateAllDisplays()
    {
        // 3�̕\���X���b�g�����Ԃɏ���
        for (int i = 0; i < itemBoxDisplays.Count; i++)
        {
            // currentItems�ɕ\�����ׂ��A�C�e�������邩�H
            if (i < currentItems.Count)
            {
                // ����Ε\��
                itemBoxDisplays[i].DisplayItem(currentItems[i]);
            }
            else
            {
                // �Ȃ���΃N���A
                itemBoxDisplays[i].ClearDisplay();
            }
        }
    }

    /// <summary>
    /// �A�C�e���g�p��̃J�[�\������(�E)
    /// </summary>
    private void SelectRight()
    {
        Debug.Log("�ύX�O");
        Debug.Log("����v���C���[:" + playerId + ", �I���v���C���[:" + selectedPlayer);
        selectedPlayer++;
        Debug.Log("�v�Z��");
        Debug.Log("����v���C���[:" + playerId + ", �I���v���C���[:" + selectedPlayer);
        if (selectedPlayer > 2)
        {
            selectedPlayer = 0;
        }
        Debug.Log("�ύX��");
        Debug.Log("����v���C���[:" + playerId + ", �I���v���C���[:" + selectedPlayer);
        DisplayCursor(selectedPlayer, playerId);
    }

    /// <summary>
    /// �A�C�e���g�p��̃J�[�\������(��)
    /// </summary>
    private void SelectLeft()
    {
        Debug.Log("�ύX�O");
        Debug.Log("����v���C���[:" + playerId + ", �I���v���C���[:" + selectedPlayer);
        selectedPlayer--;
        Debug.Log("�v�Z��");
        Debug.Log("����v���C���[:" + playerId + ", �I���v���C���[:" + selectedPlayer);
        if (selectedPlayer < 0)
        {
            selectedPlayer = 2;
        }
        Debug.Log("�ύX��");
        Debug.Log("����v���C���[:" + playerId + ", �I���v���C���[:" + selectedPlayer);
        DisplayCursor(selectedPlayer, playerId);
    }

    private void DisplayCursor(int target, int me)
    {
        //Debug.Log("����v���C���[:" + playerId + ", �I���v���C���[:" + selectedPlayer);
        cursorController.UpdateCursor(target, me);
    }
}