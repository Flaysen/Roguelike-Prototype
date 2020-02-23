using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDInventory : MonoBehaviour
{
    public Inventory equipment;
    public Inventory consumables;
    public Inventory scrolls;

    void Start()
    {
        equipment.ItemAdded += InventoryItemAdded;
        equipment.ItemRemoved += InventoryItemRemoved;

        consumables.ItemAdded += InventoryItemAdded;
        consumables.ItemRemoved += InventoryItemRemoved;

        scrolls.ItemAdded += InventoryItemAdded;
        scrolls.ItemRemoved += InventoryItemRemoved;
    }

    private void InventoryItemAdded(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = GetPanelTransform((Item)e.Item);
        foreach (Transform slot in inventoryPanel)
        {
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            ItemDragHandler itemDragHandle = imageTransform.GetComponent<ItemDragHandler>();

            if(!image.enabled)
            {
                image.enabled = true;
                image.sprite = e.Item.Image;

                itemDragHandle.Item = e.Item;
                break;
            }
        }
    }

    private void InventoryItemRemoved(object sender, InventoryEventArgs e)
    {

        Transform inventoryPanel = GetPanelTransform((Item)e.Item);
        foreach (Transform slot in inventoryPanel)
        {
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();

            if (itemDragHandler.Item.Equals(e.Item))
            {
                image.enabled = false;
                image.sprite = null;
                itemDragHandler.Item = null;
                break;
            }
        }
    }

    private Transform GetPanelTransform(Item item)
    {
        if (item is Equipment)
            return transform.Find("EquipmentPanel");

        else if (item is Consumable)
            return transform.Find("ConsumablesPanel");

        else if (item is Scroll)
            return transform.Find("ScrollPanel");

        else return null;
    }
}
