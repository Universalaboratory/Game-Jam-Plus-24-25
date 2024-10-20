using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    

    Vector2 moveInput;
    Rigidbody2D rb;
    Animator animator;

    private bool facingRight = true;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private Transform _groundCheck; 
    [SerializeField] private LayerMask _groundLayer;
    bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moveInput.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput.x < 0 && facingRight)
        {
            Flip();
        }
        
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCapsule(_groundCheck.position,new Vector2(16.4f,38.6f),CapsuleDirection2D.Vertical, 0, _groundLayer);
        rb.velocity = new Vector2(moveInput.x * _walkSpeed, rb.velocity.y);
        animator.SetFloat("xSpeed", Mathf.Abs(rb.velocity.x));

    }

    public void OnMove(InputAction.CallbackContext context)
    { 
        moveInput = context.ReadValue<Vector2>();
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded) 
        {
            rb.velocity = new Vector2(rb.velocity.x,_jumpForce);
            Debug.Log("Jump");
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

}
