using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController3D : MonoBehaviour
{

    public float MoveSpeed;
    public float jumpForce;
    public Rigidbody rB;
    public Collider playerCollider; 

    private float xInput;
    private float yInput;

    private Vector2 newPosition;

    public bool IsGrounded;


    private void Awake()
    {
        rB = GetComponent<Rigidbody>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void Update()
    {
        MovePlayer();
        Jump();
    }

    void MovePlayer()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        newPosition = transform.position;
        newPosition.x += xInput * MoveSpeed;
        rB.MovePosition(newPosition);

    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }


}
