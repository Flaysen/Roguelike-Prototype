using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClickHandler : MonoBehaviour
{
    public Inventory inventory;

    [SerializeField] private KeyCode key;

   public void OnItemClicked()
   {
        ItemDragHandler dragHandler =
            gameObject.transform.Find("ItemImage").GetComponent<ItemDragHandler>();

        IInventoryItem item = dragHandler.Item;

        inventory.UseItem(item);

        item?.OnUse();

        if(item is Consumable)
            inventory.RemoveItem(item);
   }

   void Update()
    {
        if(Input.GetKeyDown(key))
        {
            Invoke("OnItemClicked", 0);
        }
    }
}
