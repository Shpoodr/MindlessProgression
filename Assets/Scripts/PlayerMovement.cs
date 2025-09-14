using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMovement : StateManager<PlayerMovement.PlayerState>
{
    //movement varaibles
    public float moveSpeed = 5f;
    public float rotationSpeed = 700f;

    //dash movement varaibles
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 2f;

    //public properties
    public Transform PlayerVisuals { get; private set;}
    public float LastDashTime { get; set; } = -Mathf.Infinity;

    //state Machine stuffs
    public enum PlayerState
    {
        Idle,
        Walk,
        Dash
    }

    //private Dictionary<PlayerState, BaseState<PlayerState>> states = new Dictionary<PlayerState, BaseState<PlayerState>>();
    //private BaseState<PlayerState> currentState;
    public CharacterController CharacterController;
    void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        PlayerVisuals = transform.Find("PlayerObject");
        States.Add(PlayerState.Walk, new WalkState(this));
        States.Add(PlayerState.Dash, new DashState(this));

        CurrentState = States[PlayerState.Walk];
    }

    public class WalkState : BaseState<PlayerState>
    {
        private PlayerMovement manager;
        public WalkState(PlayerMovement manager) : base(PlayerState.Walk)
        {
            this.manager = manager;
        }
        public override void EnterState()
        {
            Debug.Log("entering walk state");
        }
        public override void ExitState()
        {
            Debug.Log("exiting walk state");
        }
        public override void UpdateState()
        {
            float horizontalMovement = Input.GetAxisRaw("Horizontal");
            float verticalMovement = Input.GetAxisRaw("Vertical");

            Vector3 moveDirection = new Vector3(horizontalMovement, 0f, verticalMovement);
            manager.CharacterController.Move(moveDirection * manager.moveSpeed * Time.deltaTime);

            if (moveDirection.sqrMagnitude > 0.01f)
            {
                Quaternion rotation = Quaternion.LookRotation(moveDirection);
                //transform.rotation = rotation;
                manager.PlayerVisuals.rotation = Quaternion.RotateTowards(manager.PlayerVisuals.rotation, rotation, manager.rotationSpeed * Time.deltaTime);

            }
            
        }
        public override PlayerState GetNextState()
        {
            bool canDash = Time.time >= manager.LastDashTime + manager.dashCooldown;
            if (Input.GetKeyDown(KeyCode.Space) && canDash)
            {
                return PlayerState.Dash;
            }
            return PlayerState.Walk;
        }
        public override void OnTriggerEnter(Collider other)
        {
            
        }
        public override void OnTriggerStay(Collider other)
        {
            
        }
        public override void OnTriggerExit(Collider other)
        {
            
        }
        
    }

    public class DashState : BaseState<PlayerState>
    {
        private PlayerMovement manager;
        private Vector3 dashDirection;
        private float dashTimer;
        public DashState(PlayerMovement manager) : base(PlayerState.Dash)
        {
            this.manager = manager;
        }
        public override void EnterState()
        {
            manager.LastDashTime = Time.time;
            dashTimer = 0f;

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

            dashDirection = moveDirection.sqrMagnitude > 0.01f ? moveDirection : manager.PlayerVisuals.forward;
        }
        public override void ExitState()
        {
            
        }
        public override void UpdateState()
        {
            dashTimer += Time.deltaTime;
            manager.CharacterController.Move(dashDirection * manager.dashSpeed * Time.deltaTime);
        }
        public override PlayerState GetNextState()
        {
            if (dashTimer >= manager.dashDuration)
            {
                return PlayerState.Walk;
            }
            return PlayerState.Dash;
        }
        public override void OnTriggerEnter(Collider other)
        {
            
        }
        public override void OnTriggerStay(Collider other)
        {
            
        }
        public override void OnTriggerExit(Collider other)
        {
            
        }
    }
}

    
/*
    private CharacterController characterController;
    public Transform playerVisuals;
    Rigidbody rBody;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //getting the inputs for later use
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(horizontalMovement, 0f, verticalMovement);

        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion rotation = Quaternion.LookRotation(moveDirection);
            //transform.rotation = rotation;
            playerVisuals.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space clicked");
            characterController.Move(moveDirection * dashSpeed * Time.deltaTime);
        }
    }
}
*/