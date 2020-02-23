using UnityEngine;

public interface IInventoryItem
{
    string Name { get; }
    Sprite Image { get; }
    int Cost { get; }
  
    void OnPickup();
    void OnDrop();
    void OnUse();
} 

