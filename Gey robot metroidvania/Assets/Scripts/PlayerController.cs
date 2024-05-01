using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float MoveSpeed;
    public Rigidbody2D rB;

    private float xInput;

    private Vector2 newPosition;


    private void Awake()
    {
        rB = GetComponent<Rigidbody2D>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        xInput = Input.GetAxis("Horizontal");
        newPosition = transform.position;
        newPosition.x += xInput;
        rB.MovePosition(newPosition);
    }


}
