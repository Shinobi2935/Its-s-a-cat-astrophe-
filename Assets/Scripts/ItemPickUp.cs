using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public CollectableItem Item;
    // Start is called before the first frame update
    public void Pickup()
    {
        Debug.Log(Item + " " + Item.GetType());
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
    }
    private void OnMouseDown()
    {
        Pickup();
    }
}
