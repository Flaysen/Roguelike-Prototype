using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IAmRigid
{

    [Header("MOVEMENT")]
    public float moveSpeed;
    public float gravity = 20.0f;

    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Vector3 moveDirection;

    private Camera mainCamera;
    private CharacterController characterController;
    private Collider collider;
    private Rigidbody rb;
    public Vector3 pointToLook;

    public bool freezeControls = false;
    public float freezeEnd;

    [Header("INTERACT")]
    private GameObject interactBox;
    private float interactDuration = 0.1f;
    private float interactEnd = 0.0f;
    [SerializeField] private KeyCode interactButton;

    [Header("JUMPING")]
    private bool canDoubleJump;
    public float jumpSpeed;
    [SerializeField] private KeyCode jumpButton;
   
    [Header("DASHING")]
    [SerializeField] private KeyCode dashButton;
    public float dashPower = 6.0f;
    public float dashTime = 1.0f;
    public float nextDash;

    public PlayerStats PlayerStats;

    public Inventory inventoryEquipment;
    public Inventory inventoryConsumables;
    public Inventory inventoryScrolls;
   


    public bool IsOutOfControl { get; set; }

    public float TakeControlTime => 0.3f;

    public event Action BasicAttack = delegate { };

    public KeyCode basicAttack;

    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();

        interactBox = GameObject.Find("InteractBox");
        interactBox.SetActive(false);

        collider = GetComponent<Collider>();

        PlayerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    void Update()
    {
        moveSpeed = PlayerStats.Speed.Value;
        if (Time.time > freezeEnd)
            freezeControls = false;
        if(freezeControls != true)
            moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, this.transform.position);
        float rayLenght;

        if (characterController.isGrounded && freezeControls != true)
        {
            moveDirection = moveInput.normalized;
            moveVelocity = moveDirection * moveSpeed;
        }

        if (Input.GetKeyDown(jumpButton))
        {
            if (characterController.isGrounded)
            {
                moveVelocity.y = jumpSpeed;
                canDoubleJump = true;
            }
            else if ((canDoubleJump == true) && (characterController.isGrounded == false))
            {
                moveVelocity.y = jumpSpeed;
                canDoubleJump = false;
            }
        }
                
        if (Input.GetKeyDown(dashButton))
        {
            if (Time.time > nextDash)
            {
                nextDash = Time.time + dashTime;
                freezeControls = true;
                freezeEnd = nextDash;
                moveVelocity = moveDirection * dashPower;              
            }
        }

        if(Input.GetKeyDown(interactButton))
        {
            interactBox.SetActive(true);
            interactEnd = Time.time + interactDuration;
        }

        if (Input.GetKey(basicAttack) && !EventSystem.current.IsPointerOverGameObject())
        {
            BasicAttack();
        }

        if (groundPlane.Raycast(cameraRay, out rayLenght))
        {
            pointToLook = cameraRay.GetPoint(rayLenght);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
        moveVelocity.y -= gravity * Time.deltaTime;
        characterController.Move(moveVelocity * Time.deltaTime);
        if (interactEnd < Time.time) interactBox.SetActive(false);
    }

    public void LoseControl()
    {
        characterController.enabled = false;
        collider.enabled = true;

        StartCoroutine(GetControlAfterTime(TakeControlTime, () =>
        {
            rb.velocity = Vector3.zero;
            characterController.enabled = true;
            collider.enabled = false;
        }));
    }

    public IEnumerator GetControlAfterTime(float time, Action task)
    {
        if (IsOutOfControl)
            yield break;
        IsOutOfControl = true;
        yield return new WaitForSeconds(time);
        task();
        IsOutOfControl = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        IInventoryItem item = other.GetComponent<IInventoryItem>();
        {
            if (item != null)
            {
                Inventory inventory = GetItemToInventory((Item)item);
                inventory?.AddItem(item);
            }
        }
    }

    private Inventory GetItemToInventory(Item item)
    {
        if (item is Equipment)
            if (item is Amplifier)
            {
                item.OnPickup();
                return null;
            }               
            else
                return inventoryEquipment;

        else if (item is Consumable)
            return inventoryConsumables;

        else if (item is Scroll)
            return inventoryScrolls;

        else return null;
    }
}
