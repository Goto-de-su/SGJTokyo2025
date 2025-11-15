using System.Collections;
using UnityEngine;

public class OneColumnSlotMachine : MonoBehaviour
{
    [Header("?????????????")]
    [SerializeField] private ItemBoxDisplay slotDisplay;

    [Header("?????? 6 ? ItemData ????")]
    [SerializeField] private ItemData[] candidateItems;

    [Header("????")]
    [Tooltip("????????")]
    [SerializeField] private float spinDuration = 2f;

    [Tooltip("???????????????????")]
    [SerializeField] private float changeInterval = 0.05f;

    private bool isSpinning = false;      // ??????
    private ItemData lastResult;          // ????????

    // ????????????????? OnClick ???
    public void StartSpin()
    {
        if (!isSpinning)
        {
            StartCoroutine(SpinRoutine());
        }
    }

    // ??????????
    public ItemData GetLastResult()
    {
        return lastResult;
    }

    private IEnumerator SpinRoutine()
    {
        isSpinning = true;
        float timer = 0f;

        // ????? spinDuration ??
        while (timer < spinDuration)
        {
            // ??? 0 ~ candidateItems.Length-1 ???
            int index = Random.Range(0, candidateItems.Length);
            ItemData item = candidateItems[index];

            if (item != null && slotDisplay != null)
            {
                // ??
                slotDisplay.DisplayItem(item);
            }

            // ? changeInterval ??????
            yield return new WaitForSeconds(changeInterval);
            timer += changeInterval;
        }

        // ?????????????????? index?
        int finalIndex = Random.Range(0, candidateItems.Length);
        lastResult = candidateItems[finalIndex];

        if (slotDisplay != null && lastResult != null)
        {
            slotDisplay.DisplayItem(lastResult);
        }

        isSpinning = false;
    }
}
