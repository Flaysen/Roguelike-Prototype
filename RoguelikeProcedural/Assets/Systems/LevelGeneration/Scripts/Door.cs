using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Renderer _renderer;

    public Collider _collider;

    private RoomManager _roomManager;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _renderer = GetComponent<Renderer>();

        _collider.enabled = false;
        _renderer.enabled = false;
    }

    private void Start()
    {
        _roomManager.OnRoomEntered += CloseDoor;
        _roomManager.OnRoomCleared += DisableDoors;
    }

    public void Initialize(RoomManager roomManager)
    {
        _roomManager = roomManager;
    }

    private void DisableDoors(RoomManager roomManager)
    {
        gameObject.SetActive(false);
    }


    private void CloseDoor(RoomManager room)
    {
        _collider.enabled = true;
        _renderer.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        _roomManager.RoomEntered();
    }
}
