
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] private GameObject[] cursors = new GameObject[3];
    [SerializeField] private GameObject[] anchor = new GameObject[3];

    [SerializeField] private float OFFSET;

    private int p1 = 0;
    private int p2 = 0;
    private int p3 = 0;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            if(p1 != 0)
            {
                p1--;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (p1 != 2)
            {
                p1++;
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (p2 != 0)
            {
                p2--;
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (p2 != 2)
            {
                p2++;
            }
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (p3 != 0)
            {
                p3--;
            }
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (p3 != 2)
            {
                p3++;
            }
        }

        UpdateCursor(p1, 0);
        UpdateCursor(p2, 1);
        UpdateCursor(p3, 2);
    }

    public void UpdateCursor(int anchor_idx, int cursor_idx)
    {
        Vector3 pos = anchor[anchor_idx].transform.position;
        pos.x += OFFSET * (cursor_idx - 1);
        cursors[cursor_idx].transform.position = pos;
    }
}