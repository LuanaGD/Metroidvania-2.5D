using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController3D : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public float MoveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Rigidbody rB;
    [SerializeField] private Collider playerCollider;
    private float xInput;
    private float yInput;
    [SerializeField] public bool canControl;
    [SerializeField] private bool IsGrounded;


    [Header("Dash")]
    [SerializeField] private float dashVelocity;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    [SerializeField] private Transform frontPlayer;
    [SerializeField] private bool canDash;
    [SerializeField] private bool isDashing;
    private Vector3 dashDir;


    [Header("LayerChange")]
    [SerializeField] private Vector2 newPosition;

    [Header("Attack")]
    [SerializeField] private bool facingRight;


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

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && canControl)
        {
            Dash();
            if (isDashing)
            {
                rB.velocity = dashDir.normalized * dashVelocity;
                return;
            }
            if (IsGrounded)
            {
                canDash = true;
            }
        }

        //Debug.DrawRay(frontPlayer.transform.position, transform.right * 5, Color.red);

        MovePlayer();
        Jump();
        Attack();
    }

    void MovePlayer()
    {
        if (canControl)
        {
            xInput = Input.GetAxisRaw("Horizontal");
            newPosition = transform.position;
            newPosition.x += xInput * MoveSpeed;
            rB.MovePosition(newPosition);

            if (xInput < 0 && facingRight)
            {
                facingRight = false;
                transform.Rotate(0f, 180f, 0f);
            }
            if (xInput > 0 && !facingRight)
            {
                facingRight=true;
                transform.Rotate(0f, 180f, 0f);
            }
        }

    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded && canControl)
        {
            IsGrounded = false;
            rB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void Attack()
    {
        //RaycastHit hitInfo = Physics.Raycast(frontPlayer.position, frontPlayer.right) ;
        if (canControl && Input.GetKeyDown(KeyCode.E))
        {
            Debug.DrawRay(frontPlayer.transform.position, transform.right * 5, Color.red);
            Debug.Log("Has fired");
        }
        //Debug.DrawRay(frontPlayer.transform.position, transform.right * 5, Color.red);

        //Debug.DrawRay(transform.position, Color(blue), )
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal == Vector3.up)
        {
            IsGrounded = true;
        }
    }

    void Dash()
    {
        isDashing = true;
        canDash = false;
        dashDir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

        if (dashDir == Vector3.zero) //si dash à l'arrêt
        {
            dashDir = new Vector3(transform.localScale.x, 0, 0);
        }

        StartCoroutine(DashCoroutine());
    }

    public void MoveToOtherLayer(Vector3 pos, Quaternion rotation)
    {
        transform.position = pos;
        Physics.SyncTransforms();
    }

    private IEnumerator DashCoroutine()
    {
        yield return new WaitForSeconds(dashTime);
        Debug.Log("Has dashed");
        isDashing = false;
        Debug.Log("Cooldown");
        yield return new WaitForSeconds(dashCooldown);
        Debug.Log("Can Dash");
        canDash = true;
    }


}
