using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 5f;
    private CharacterController characterController;
    public Transform playerVisuals;
    Rigidbody rBody;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public float rotationSpeed = 700f; 
    void Update()
    {
        //getting the inputs for later use
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(horizontalMovement, 0f, verticalMovement);

        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        Debug.Log(moveDirection);
        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion rotation = Quaternion.LookRotation(moveDirection);
            //transform.rotation = rotation;
            playerVisuals.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }
}
