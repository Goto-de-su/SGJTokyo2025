using UnityEngine;
using UnityEngine.UI;

public class MotivationGauge : MonoBehaviour
{
    [SerializeField] private Image gauge;

    public void DisplayGauge(int value)
    {
        int valueGauge;
        int maxGauge = GameManager.Instance.WinScore;

        if (value < 0)
        {
            valueGauge = 0;
        }
        else
        {
            valueGauge = value;
        }

        gauge.fillAmount = (float)value / maxGauge;
    }
}
