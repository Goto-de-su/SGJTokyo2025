using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        // このスクリプトがアタッチされているオブジェクトがコンテナ(入れ物)になる
        RectTransform containerRect = GetComponent<RectTransform>();

        for (int i = 0; i < numberOfGauges; i++)
        {
            // プレハブをインスタンス化(生成)
            // (i * gaugeSpacing) で右側にずらして配置する
            Vector3 spawnPos = new Vector3(i * gaugeSpacing, 0, 0);

            // this.transform (コンテナ) の子として生成
            GameObject gaugeInstance = Instantiate(gaugePrefab, this.transform);

            // UIの位置はlocalPositionで設定
            gaugeInstance.GetComponent<RectTransform>().localPosition = spawnPos;

            // プレハブの構造が「親」->「子」であることを前提に、
            // 0番目の子(GaugeFill)のImageコンポーネントを取得
            Image fillImage = gaugeInstance.transform.GetChild(0).GetComponent<Image>();

            if (fillImage != null)
            {
                fillImages.Add(fillImage); // リストに追加
                fillImage.fillAmount = 0; // 初期状態は0
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

    /// ゲージのステップを1つ進める
    void IncrementSteps(int stepsToAdd)
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
                // 0にリセットする
                currentStep = 0;
            }
        }

        // 見た目を更新
        UpdateGaugeVisuals();
    }

    /// 現在のcurrentStepに基づいて、すべてのゲージの見た目を更新する
    void UpdateGaugeVisuals()
    {
        // fillImagesリストにあるすべてのゲージをチェック
        for (int i = 0; i < fillImages.Count; i++)
        {
            Image gaugeImage = fillImages[i];

            // このゲージが担当するステップ数を計算
            // 0番目のゲージ -> 0 (空), 1 (半分), 2 (全部)
            // 1番目のゲージ -> 2 (空), 3 (半分), 4 (全部)
            int stepsForThisGauge = i * 2;

            if (currentStep <= stepsForThisGauge)
            {
                // 現在ステップがこのゲージの開始前なら 0%
                gaugeImage.fillAmount = 0f;
            }
            else if (currentStep == stepsForThisGauge + 1)
            {
                // 現在ステップがこのゲージの 1ステップ目 (半分) なら 50%
                gaugeImage.fillAmount = full1;
            }
            else // currentStep >= stepsForThisGauge + 2
            {
                // 現在ステップがこのゲージの 2ステップ目以降 (全部) なら 100%
                gaugeImage.fillAmount = full2;
            }
        }
    }
}