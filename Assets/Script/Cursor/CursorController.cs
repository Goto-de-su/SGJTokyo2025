
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] private GameObject[] cursors = new GameObject[3];
    [SerializeField] private GameObject[] anchor = new GameObject[3];

    [SerializeField] private float OFFSET;

    private int current_x = 0;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            current_x--;
            if(current_x < 0)
            {
                current_x = 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            current_x = (current_x + 1) % 3;
        }

        UpdateCursor(current_x, 0);
    }

    public void UpdateCursor(int anchor_idx, int cursor_idx)
    {
        Vector3 pos = anchor[anchor_idx].transform.position;
        pos.x += OFFSET * (cursor_idx - 1);
        cursors[cursor_idx].transform.position = pos;
    }
}