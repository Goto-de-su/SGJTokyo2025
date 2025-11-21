using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FullnessGauge : MonoBehaviour
{
    [Header("�Q�[�W�̐ݒ�")]
    [SerializeField]
    private GameObject gaugePrefab; // �����Q�[�W�̃v���n�u

    [Header("�z�u�̐ݒ�")]
    [SerializeField]
    private int numberOfGauges = 3; // �e�̐�

    [SerializeField]
    private float gaugeSpacing = 160f; // �Q�[�W��z�u����Ԋu

    private float full1 = 0.5f;  // ����
    private float full2 = 1.0f;  // 1������

    // ---- �����Ǘ��p ----
    private List<Image> fillImages = new List<Image>(); // �q��FillImage�������i�[
    private int currentStep = 0; // ���݂̃t�B���X�e�b�v (0 = ��)
    private int maxSteps; // �ő�X�e�b�v�� (�Q�[�W�� * 2)

    private bool isCoolTime = false;

    [SerializeField] private EmotionControler emotionControler; 

    void Start()
    {
        // �ő�X�e�b�v�����v�Z (�Q�[�W1�ɂ�2�X�e�b�v)
        maxSteps = numberOfGauges * 2;

        // �Q�[�W�𐶐��E�z�u
        SpawnGauges();
    }

    void SpawnGauges()
    {
        // (�ȗ�... ���̃R�[�h�Ɠ���)
        RectTransform containerRect = GetComponent<RectTransform>();
        for (int i = 0; i < numberOfGauges; i++)
        {
            Vector3 spawnPos = new Vector3(i * gaugeSpacing, 0, 0);
            GameObject gaugeInstance = Instantiate(gaugePrefab, this.transform);
            gaugeInstance.GetComponent<RectTransform>().localPosition = spawnPos;
            Image fillImage = gaugeInstance.transform.GetChild(0).GetComponent<Image>();
            if (fillImage != null)
            {
                fillImages.Add(fillImage);
                fillImage.fillAmount = 0;
            }
        }
    }

    public void IncrementSteps(int stepsToAdd)
    {
        if (isCoolTime)
        {
            return;
        }

        bool didReset = false;

        if (currentStep == maxSteps)
        {
            currentStep = 0;
            didReset = true;
        }
        else
        {
            // �X�e�b�v��i�߂�
            currentStep += stepsToAdd;

            if (currentStep > maxSteps)
            {
                // �ő�l�𒴂��� -> ���Z�b�g
                currentStep = 0;
                didReset = true;
            }
        }

        // 3. �Q�[�W�̌����ڂ��X�V (���Z�b�g����Ă��A����ĂȂ��Ă�)
        UpdateGaugeVisuals();

        // 4. �������Z�b�g���������Ă�����A�N�[���^�C���R���[�`�����J�n
        if (didReset)
        {
            StartCoroutine(PoopAction());
        }
    }

    void UpdateGaugeVisuals()
    {
        // (�ȗ�... ���̃R�[�h�Ɠ���)
        for (int i = 0; i < fillImages.Count; i++)
        {
            Image gaugeImage = fillImages[i];
            int stepsForThisGauge = i * 2;

            if (currentStep <= stepsForThisGauge)
            {
                gaugeImage.fillAmount = 0f;
            }
            else if (currentStep == stepsForThisGauge + 1)
            {
                gaugeImage.fillAmount = full1;
            }
            else
            {
                gaugeImage.fillAmount = full2;
            }
        }
    }

    private IEnumerator PoopAction()
    {
        // 1. �N�[���^�C���J�n�i�t���O�𗧂Ă�j
        isCoolTime = true;

        emotionControler.Full();

        Debug.Log("���񂿁i�N�[���^�C���J�n�j");

        // 2. 3�b�҂�
        yield return new WaitForSeconds(3.0f);

        // 3. �N�[���^�C���I���i�t���O�����낷�j
        isCoolTime = false;
        Debug.Log("�N�[���^�C���I��");

    }
}