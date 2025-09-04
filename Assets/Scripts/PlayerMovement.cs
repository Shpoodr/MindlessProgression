using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using Unity.VisualScripting;
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

    //state Machine stuffs
    public enum PlayerState
    {
        Idle,
        Walk,
        Dash
    }

    private Dictionary<PlayerState, BaseState<PlayerState>> states = new Dictionary<PlayerState, BaseState<PlayerState>>();
    private BaseState<PlayerState> currentState;
    public CharacterController CharacterController;
    void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        currentState = states[PlayerState.Idle];
    }

}   
public class WalkState : BaseState<PlayerState>
{
    
}

public class DashState : BaseState<PlayerState>
{
      
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