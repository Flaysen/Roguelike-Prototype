using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawning : MonoBehaviour
{
    [SerializeField] private Monster _monsterPrefab;

    private RoomManager _roomManager;
      
    private void Start()
    {
        _roomManager.OnRoomEntered += SpawnMonsters;
    }

    public void InitializeRoomManager(RoomManager roomManager)
    {
        _roomManager = roomManager;
    }

    private void SpawnMonsters(RoomManager roomManager)
    {
      
        Monster monster = Instantiate(_monsterPrefab,
                    transform.position + _monsterPrefab.transform.localPosition,
                    Quaternion.identity);

        roomManager.monsters.Add(monster);
    
        _roomManager.OnRoomEntered -= SpawnMonsters;
    }
}
