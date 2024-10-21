using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D castFilter;
    
    public bool IsGrounded { get
        {
            return _isGrounded;
        }
        private set{
            _isGrounded = value;
        } }

    CapsuleCollider2D touchingCol;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];

    [SerializeField] 
    private float _groundDistance = 0.5f;
    [SerializeField]
    private bool _isGrounded;



    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, _groundDistance) > 0;
        
    }
}
