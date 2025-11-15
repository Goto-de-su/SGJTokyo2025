using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullnessGauge : MonoBehaviour
{
    // ( ... Start() や SpawnGauges() など、他の部分はそのまま ... )

    [Header("ゲージの設定")]
    [SerializeField]
    private GameObject gaugePrefab; // 満腹ゲージのプレハブ

    [Header("配置の設定")]
    [SerializeField]
    private int numberOfGauges = 3; // 親の数

    [SerializeField]
    private float gaugeSpacing = 160f; // ゲージを配置する間隔

    [SerializeField] private float full1 = 0.5f;  // 半分
    [SerializeField] private float full2 = 1.0f;  // 1増える


    // ---- 内部管理用 ----
    private List<Image> fillImages = new List<Image>(); // 子のFillImageだけを格納
    private int currentStep = 0; // 現在のフィルステップ (0 = 空)
    private int maxSteps; // 最大ステップ数 (ゲージ数 * 2)

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

    void Update()
    {
        // Aキー (半分 = 1ステップ)
        if (Input.GetKeyDown(KeyCode.A))
        {
            IncrementSteps(1); // 1ステップ進める
        }

        // Dキー (1つ = 2ステップ)
        if (Input.GetKeyDown(KeyCode.D))
        {
            IncrementSteps(2); // 2ステップ進める
        }
    }


    // --- ▼▼▼ ここを修正 ▼▼▼ ---

    /// <summary>
    /// ゲージのステップを指定した量だけ進める
    /// </summary>
    /// <param name="stepsToAdd">追加するステップ数 (1=半分, 2=1つ)</param>
    public void IncrementSteps(int stepsToAdd)
    {
        // 1. ゲージが満タンの時にキーが押されたか？
        if (currentStep == maxSteps)
        {
            // 最大なら0にリセット (赤色が全部消える)
            currentStep = 0;
        }
        else // 2. ゲージが満タンではない時
        {
            // ステップを進める
            currentStep += stepsToAdd;

            // 3. ステップを追加した結果、maxStepsを超えたか？
            if (currentStep > maxSteps)
            {
                // 【変更点】maxStepsで止めるのではなく、0にリセットする
                currentStep = 0;
            }
            // (注意: ちょうど currentStep == maxSteps になった場合は、
            //  ここではリセットされず、次回のキー入力で 1. の条件に合致してリセットされます)
        }

        // 見た目を更新
        UpdateGaugeVisuals();
    }

    // --- ▲▲▲ ここまで修正 ▲▲▲ ---


    /// <summary>
    /// 現在のcurrentStepに基づいて、すべてのゲージの見た目を更新する
    /// </summary>
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
}