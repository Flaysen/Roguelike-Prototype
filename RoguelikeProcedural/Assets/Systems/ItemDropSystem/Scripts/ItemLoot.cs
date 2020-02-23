using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemLoot : MonoBehaviour
{
    [System.Serializable]
    public struct ItemDropType
    {
        [Range(0, 100)]
        public int dropChance;

        public List<Item> itemsToDrop;
    }

    [Range(0, 100)]
    [SerializeField] private int dropChance;
    [Space]
    [Range(0,100)]
    [SerializeField] private int amplifierDropChance;
    [Range(0, 100)]
    [SerializeField] private int consumableDropChance;
    [Range(0, 100)]
    [SerializeField] private int equipmentDropChance;
    [Range(0, 100)]
    [SerializeField] private int scrollDropChance;

    [SerializeField]
    private ItemDropType equipable = new ItemDropType();
    [SerializeField]
    private ItemDropType amplifier = new ItemDropType();
    [SerializeField]
    private ItemDropType consumable = new ItemDropType();
    [SerializeField]
    private ItemDropType scroll = new ItemDropType();

    private List<ItemDropType> itemDropTypes = new List<ItemDropType>();

    private List <Item> itemTypeToDrop = new List<Item>();

    private void Start()
    {
        equipable.dropChance = equipmentDropChance;
        equipable.itemsToDrop = ItemLibrary.Instance.equipment;
        itemDropTypes.Add(equipable);

        amplifier.dropChance = amplifierDropChance;
        amplifier.itemsToDrop = ItemLibrary.Instance.amplifiers;
        itemDropTypes.Add(amplifier);

        consumable.dropChance = consumableDropChance;
        consumable.itemsToDrop = ItemLibrary.Instance.consumables;
        itemDropTypes.Add(consumable);

        scroll.dropChance = scrollDropChance;
        scroll.itemsToDrop = ItemLibrary.Instance.scrolls;
        itemDropTypes.Add(scroll);
    }

    public void RollType()
    {
      int typeWeight = amplifierDropChance
            + consumableDropChance
            + equipmentDropChance
            + scrollDropChance;

        int randomValue = Random.Range(0, typeWeight);

        for (int j = 0; j < itemDropTypes.Count; j++)
        {
            if (randomValue <= itemDropTypes[j].dropChance)
            {
                itemTypeToDrop = itemDropTypes[j].itemsToDrop;
                return;
            }
            randomValue -= itemDropTypes[j].dropChance;
        }
    }
       
    public Item RollLoot()
    {
        int rollDropChance = Random.Range(1, 101);

        if(rollDropChance > dropChance)
        {
            Debug.Log("No Drop");
            return null;
        }

        RollType();

        if (rollDropChance <= dropChance)
        {
            int randomValue = Random.Range(0, itemTypeToDrop.Count);
            return itemTypeToDrop[randomValue];
        }
        else Debug.Log("!!"); return null;  
    }
    private Vector3 RandomVector(float min, float max)
    {
        return new Vector3(
            Random.Range(min, max),
            Random.Range(70, 85),
            Random.Range(min, 0));
    }
}
