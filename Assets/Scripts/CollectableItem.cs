using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName = "CollectableItem/Create New Item")]
public class CollectableItem : ScriptableObject
{
    public int id;
    public string itemName;
    public int value;
    public Sprite icon;
    public ItemType itemType;

    public enum ItemType
    {
        Potion,
        Stone,
        Stick
    }
}
