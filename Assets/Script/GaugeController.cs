using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    // 各ゲージの「Image」コンポーネントをセット
    [SerializeField] private Image normalGauge;  // 平常時 (背景)
    [SerializeField] private Image dislikeImage; // 嫌がる
    [SerializeField] private Image angryImage;   // 怒り
    [SerializeField] private Image happyImage;   // 幸せ

    // ゲージが1秒間に増える量（0.0〜1.0の割合）
    [SerializeField] private float fillSpeed = 0.5f;

    // 各感情の現在の値 (0.0 から 1.0 の間)
    private float currentDislike = 0.1f;
    private float currentAngry = 0.2f;
    private float currentHappy = 0.3f;

    void Start()
    {
        // 起動時にすべてのゲージを0にリセット
        if (dislikeImage != null) dislikeImage.fillAmount = 0f;
        if (angryImage != null) angryImage.fillAmount = 0f;
        if (happyImage != null) happyImage.fillAmount = 0f;
    }

    void Update()
    {
        // --- 1キー（嫌悪感）: 左から増える ---
        if (Input.GetKey(KeyCode.Alpha1))
        {
            // currentDislikeの値を増やす
            currentDislike += fillSpeed * Time.deltaTime;
            // 値が1.0を超えないように制御
            currentDislike = Mathf.Clamp01(currentDislike);
            // ゲージのUIに反映
            dislikeImage.fillAmount = currentDislike;
        }

        // --- 2キー（怒り）: 右から増える ---
        if (Input.GetKey(KeyCode.Alpha2))
        {
            // currentAngryの値を増やす
            currentAngry += fillSpeed * Time.deltaTime;
            // 値が1.0を超えないように制御
            currentAngry = Mathf.Clamp01(currentAngry);
            // ゲージのUIに反映
            angryImage.fillAmount = currentAngry;
        }

        // --- 3キー（幸せ）: 左から増える ---
        // (ご提示のコードでは1キーになっていましたが、おそらく3キーかと思います)
        if (Input.GetKey(KeyCode.Alpha3))
        {
            // currentHappyの値を増やす
            currentHappy += fillSpeed * Time.deltaTime;
            // 値が1.0を超えないように制御
            currentHappy = Mathf.Clamp01(currentHappy);
            // ゲージのUIに反映
            happyImage.fillAmount = currentHappy;
        }
    }
}
