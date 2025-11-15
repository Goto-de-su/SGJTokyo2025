using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FullnessGauge : MonoBehaviour
{
    [Header("ゲージの設定")]
    [SerializeField]
    private GameObject gaugePrefab; // 満腹ゲージのプレハブ

    [Header("配置の設定")]
    [SerializeField]
    private int numberOfGauges = 3; // 親の数

    [SerializeField]
    private float gaugeSpacing = 160f; // ゲージを配置する間隔

    private float full1 = 0.5f;  // 半分
    private float full2 = 1.0f;  // 1増える

    // ---- 内部管理用 ----
    private List<Image> fillImages = new List<Image>(); // 子のFillImageだけを格納
    private int currentStep = 0; // 現在のフィルステップ (0 = 空)
    private int maxSteps; // 最大ステップ数 (ゲージ数 * 2)

    private bool isCoolTime = false;

    void Start()
    {
        // 最大ステップ数を計算 (ゲージ1つにつき2ステップ)
        maxSteps = numberOfGauges * 2;

        // ゲージを生成・配置
        SpawnGauges();
    }

    void SpawnGauges()
    {
        // (省略... 元のコードと同じ)
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
            // ステップを進める
            currentStep += stepsToAdd;

            if (currentStep > maxSteps)
            {
                // 最大値を超えた -> リセット
                currentStep = 0;
                didReset = true;
            }
        }

        // 3. ゲージの見た目を更新 (リセットされても、されてなくても)
        UpdateGaugeVisuals();

        // 4. もしリセットが発生していたら、クールタイムコルーチンを開始
        if (didReset)
        {
            StartCoroutine(PoopAction());
        }
    }

    void UpdateGaugeVisuals()
    {
        // (省略... 元のコードと同じ)
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
        // 1. クールタイム開始（フラグを立てる）
        isCoolTime = true;

        Debug.Log("うんち（クールタイム開始）");

        // 2. 3秒待つ
        yield return new WaitForSeconds(3.0f);

        // 3. クールタイム終了（フラグを下ろす）
        isCoolTime = false;
        Debug.Log("クールタイム終了");

    }
}