using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<Door> doors = new List<Door>();

    public List<Monster> monsters = new List<Monster>();

    public MonsterSpawning _monsterSpawning;

    public event Action<RoomManager> OnRoomEntered;

    public event Action<RoomManager> OnRoomCleared;

    private bool _isClosed;

    private bool _isClear;

    private void Awake()
    {
        foreach(Door door in GetComponentsInChildren<Door>())
        {
            doors.Add(door);
            door.Initialize(this);
        }

        _monsterSpawning.InitializeRoomManager(this);

        Monster.OnMonsterDeath += RemoveMonster;
    }

    private void RemoveMonster(Monster monster)
    {
        if(_isClosed == true)
        {
            monsters.Remove(monster);
            if (monsters.Count == 0)
            {
                _isClear = true;
                OnRoomCleared?.Invoke(this);
            }
        }
        
    }

    public void RoomEntered()
    {
        _isClosed = true;
        OnRoomEntered?.Invoke(this);
    }
}
