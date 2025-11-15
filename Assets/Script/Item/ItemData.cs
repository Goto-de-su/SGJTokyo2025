using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public Item item;
    public Sprite icon;
    public int motivationValue;
    public int foodValue;
}
