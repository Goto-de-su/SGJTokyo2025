using UnityEngine;
using UnityEngine.UI;

public class BGMToggleUI : MonoBehaviour
{
    public Toggle toggle;

    private void Start()
    {
        // ???? Inspector ????????????? Toggle
        if (toggle == null)
        {
            toggle = GetComponent<Toggle>();
        }

        // ??????? BGMManager
        if (BGMManager.Instance != null)
        {
            bool isOn = BGMManager.Instance.IsBGMEnabled();
            toggle.isOn = isOn;
        }

        // ????? Toggle ???
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    private void OnDestroy()
    {
        if (toggle != null)
        {
            toggle.onValueChanged.RemoveListener(OnToggleChanged);
        }
    }

    private void OnToggleChanged(bool isOn)
    {
        if (BGMManager.Instance != null)
        {
            BGMManager.Instance.SetBGMEnabled(isOn);
        }
    }
}
