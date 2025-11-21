using UnityEngine;
using System.Collections.Generic;

public class ItemModel : MonoBehaviour
{
    [Tooltip("���I����f�[�^")]
    [SerializeField] private List<ItemData> srcData;

    public ItemData GetRandomItem()
    {
        // �f�[�^���i�[����Ă��Ȃ��ꍇ��null��Ԃ�
        if (this.srcData.Count == 0)
        {
            return null;
        }

        // �����Ő��������A�C�e����Ԃ�
        int randomIndex = Random.Range(0, this.srcData.Count);
        return srcData[randomIndex];
    }

    /// <summary>
    /// �A�C�e�����g�p (���ۂ̌��ʂ������ɏ���)
    /// </summary>
    [System.Obsolete]
    public void UseItem(ItemData itemToUse, int playerId)
    {
        GameManager gameManager = GameManager.Instance;
        gameManager.Players[playerId].EmotionController.UpdateEmotion(itemToUse.motivationValue);

        // 満腹ゲージ
        int value = itemToUse.foodValue;

        if (value == 1)
        {
            if (gameManager.Players[playerId].FullnessGauge != null)
            {
                gameManager.Players[playerId].FullnessGauge.IncrementSteps(1);
            }
        }

        if (value == 2)
        {
            if (gameManager.Players[playerId].FullnessGauge != null)
            {
                gameManager.Players[playerId].FullnessGauge.IncrementSteps(2);
            }
        }
    }
}
