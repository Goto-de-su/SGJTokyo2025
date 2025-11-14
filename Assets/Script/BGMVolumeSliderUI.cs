using UnityEngine;
using UnityEngine.UI;

public class BGMVolumeSliderUI : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        // ???? Inspector ???????????? Slider
        if (slider == null)
        {
            slider = GetComponent<Slider>();
        }

        // ???????0~1?
        slider.minValue = 0f;
        slider.maxValue = 1f;

        // ?????? BGMManager
        if (BGMManager.Instance != null)
        {
            slider.value = BGMManager.Instance.GetBGMVolume();
        }

        // ????????????
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnDestroy()
    {
        if (slider != null)
        {
            slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }
    }

    private void OnSliderValueChanged(float value)
    {
        if (BGMManager.Instance != null)
        {
            BGMManager.Instance.SetBGMVolume(value);
        }
    }
}
