using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Item : MonoBehaviour, IInventoryItem
{
    public enum Rarity
    {
        common,
        rare,
        mythic,
        relic
    }

    protected PlayerStats stats;

    public string Name => "Item";

    [SerializeField]
    private Sprite image;
    public Sprite Image => image;

    public int Cost => 0;

    public Rarity ItemRarity;

    [Header("Rarity colors")]
    [SerializeField]
    private Color common;
    [SerializeField]
    private Color rare;
    [SerializeField]
    private Color mythic;
    [SerializeField]
    private Color relic;

    public int DropChance => GetDropChance();

    public virtual void OnUse() { }


    private void Awake()
    {
        stats = GameObject.Find("Player").GetComponent<PlayerStats>();
        GetDropChance();
    }

    virtual public void OnPickup()
    {
        gameObject.SetActive(false);
    }

    virtual public void OnDrop()
    {
        DropItemOnTheGround();
    }

    private int GetDropChance()
    {
        if (ItemRarity == Item.Rarity.common)
            return 50;
        else if (ItemRarity == Item.Rarity.rare)
            return 25;
        else if (ItemRarity == Item.Rarity.mythic)
            return  15;
        else
            return 10;
    }

    private  void DropItemOnTheGround()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            gameObject.SetActive(true);
            gameObject.transform.position = hit.point;
        }
    }

    public Color GetRarityHaloColor()
    {
        switch(ItemRarity)
        {
            case Item.Rarity.common:
                return Color.white;
            case Item.Rarity.rare:
                return Color.blue;
            case Item.Rarity.mythic:
                return Color.magenta;
            case Item.Rarity.relic:
                return Color.red;
            default:
                return Color.white;
        }
    }
}
