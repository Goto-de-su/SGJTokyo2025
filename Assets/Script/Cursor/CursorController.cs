
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] private GameObject[] cursors = new GameObject[3];
    [SerializeField] private GameObject[] anchor = new GameObject[3];

    [SerializeField] private float OFFSET;

    public void UpdateCursor(int anchor_idx, int cursor_idx)
    {
        Vector3 pos = anchor[anchor_idx].transform.position;
        pos.x += OFFSET * (cursor_idx - 1);
        cursors[cursor_idx].transform.position = pos;
    }
}