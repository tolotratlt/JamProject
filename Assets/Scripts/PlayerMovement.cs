using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    public float JumpSpeed;
    private Rigidbody2D _rb;
    private float _speed=0;
    private SpriteRenderer _sr;
    //private Animator _animator;
    private Vector3 _velocity;
    public bool IsJumping;
    private bool CanJump;
    public GameObject Left;
    public GameObject Right;
    //public GameObject SpawnPoint;
    //public GameObject DyingPoint;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        //_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool CanJump = Physics2D.OverlapArea(Left.transform.position, Right.transform.position);
        //if(CanJump)_animator.SetBool("IsJumping",false);
        _speed = Input.GetAxis("Horizontal")*Speed*Time.deltaTime;
        //_animator.SetFloat("Speed", Mathf.Abs(_speed));
        if (_speed < 0) _sr.flipX = true;
        if (_speed > 0) _sr.flipX = false;
        if (Input.GetKey(KeyCode.Space) && CanJump) IsJumping = true;
        Debug.Log(IsJumping);
        //if (transform.position.y <= DyingPoint.transform.position.y) transform.position = SpawnPoint.transform.position;

    }

    public void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("bonus"))
        {
            transform.localScale += new Vector3(-0.5f, -0.5f, -0.5f);
            Destroy(obj.gameObject);
        }
    }

    private void Move(float _speed)
    {
        var targetVelocity = new Vector2(_speed, _rb.velocity.y);
        _rb.velocity = Vector3.SmoothDamp(_rb.velocity, targetVelocity, ref _velocity, 0.3f);
        if (IsJumping)
        {
            _rb.AddForce(new Vector2(0,JumpSpeed));
            IsJumping = false;
            //_animator.SetBool("IsJumping",true);
        }
    }

    void FixedUpdate()
    {
        Move(_speed);
    }
}
