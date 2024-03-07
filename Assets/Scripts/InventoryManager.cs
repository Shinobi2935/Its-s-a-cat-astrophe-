using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public static List<CollectableItem> Items = new List<CollectableItem>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public Toggle EnableRemove;

    public Text itemCounterText;
    public int globalItemCounter;

    //public InventoryItemController[] inventoryItems;
    public List<InventoryItemController> inventoryItems;
    //public List<InventoryItemController> inventoryItems;

    private void Awake()
    {
        Instance = this;
    }
    public void Add(CollectableItem item)
    {
        if (globalItemCounter < 20) {
            Items.Add(item);
            Debug.Log("Objeto Agregado: " + item.name);
            globalItemCounter += 1;
            itemCounterText.text = globalItemCounter.ToString() + "/20";
        }
    }

    public void Remove(CollectableItem item)
    {
        Items.Remove(item);
        globalItemCounter -= 1;
        itemCounterText.text = globalItemCounter.ToString() + "/20";
    }

    public void ListItems()
    {
        //Clean content before open.
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach(CollectableItem item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

            if (EnableRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }
        }

        Debug.Log("Items en el Inventario");
        SetInventoryItems();
    }

    public void EnableItemsRemove()
    {
        if (EnableRemove.isOn)
        {
            foreach(Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }
    }

    public void SetInventoryItems()
    {
        Debug.Log("inventoryItems");
        inventoryItems = new List<InventoryItemController>();
        ItemContent.GetComponentsInChildren<InventoryItemController>(false, inventoryItems);
        int duples = inventoryItems.Count - Items.Count;
        for (int i = 0; i < duples; i++)
        {
            inventoryItems.RemoveAt(0);
        }
        //inventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();
        Debug.Log("inventoryItems: " + inventoryItems.Count + " Items: " + Items.Count);
        foreach(InventoryItemController item in inventoryItems)
        {
            print(item.name);
        }
        for (int i = 0; i < Items.Count; i++)
        {
            inventoryItems[i].AddItem(Items[i]);
            Debug.Log("index " + i + " - " + inventoryItems[i].ToString());
        }
        //int i = 0;
        //foreach (InventoryItemController element in inventoryItems)
        //{
        //    Debug.Log("index " + i);
        //    inventoryItems[i].AddItem(Items[i]);
        //    i++;
        //}
        Debug.Log("inventoryItems After: " + inventoryItems.Count + " Items: " + Items.Count);
    }

    public List<CollectableItem> GetItems()
    {
        return Items;
    }
}
