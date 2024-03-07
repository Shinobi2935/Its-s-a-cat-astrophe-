using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    public CollectableItem item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);

        Destroy(gameObject);
    }

    public void AddItem(CollectableItem newItem)
    {
        item = newItem;
    }

    public void UseItem()
    {
        Debug.Log(CharacterStats.Instance);
        /*if(item == null)
        {
            return;
        }*/
        Debug.Log(item.ToString() + item.itemType);
        switch (item.itemType)
        {
            case CollectableItem.ItemType.Potion:
                //PlayerTesting.Instance.IncreaseHealth(item.value);
                //CharacterStats.Instance.Heal(item.value);
                FindObjectOfType<PlayerStats>().Heal(item.value);
                RemoveItem();
                break;
            case CollectableItem.ItemType.Stone:
                //PlayerTesting.Instance.IncreaseExp(item.value);
                //CharacterStats.Instance.DamageUpgrade(item.value);
                //FindObjectOfType<PlayerStats>().DamageUpgrade(item.value);
                if (FindObjectOfType<PlayerStats>().UpgradeStat(15, 5, CollectableItem.ItemType.Stone, Stat.StatsType.Armor)) { RemoveItem(); }
                break;
            case CollectableItem.ItemType.Stick:
                //PlayerTesting.Instance.IncreaseExp(item.value);
                //CharacterStats.Instance.DamageUpgrade(item.value);
                //FindObjectOfType<PlayerStats>().DamageUpgrade(item.value);
                if (FindObjectOfType<PlayerStats>().UpgradeStat(15, 5, CollectableItem.ItemType.Stick, Stat.StatsType.Damage)) { RemoveItem(); }
                break;
        }
    }
}
