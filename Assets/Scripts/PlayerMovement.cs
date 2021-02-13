using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    public float JumpSpeed;
    public SpriteRenderer SpriteRenderer;
    public int Life = 2;
    public GameObject Checkpoint;
    public GameObject DyingPoint;

    private Rigidbody2D _rb;
    private float _speed=0;
    //private Animator _animator;
    private Vector3 _velocity;
    public bool IsJumping;
    public bool CanJump;
    //public GameObject SpawnPoint;
    //public GameObject DyingPoint;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        //_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(CanJump)_animator.SetBool("IsJumping",false);
        _speed = Input.GetAxis("Horizontal")*Speed*Time.deltaTime;
        //_animator.SetFloat("Speed", Mathf.Abs(_speed));
        if (_speed < 0) SpriteRenderer.flipX = true;
        if (_speed > 0) SpriteRenderer.flipX = false;
        if (Input.GetKey(KeyCode.Space) && CanJump) IsJumping = true;
        if(DyingPoint.transform.position.y>=transform.position.y)BackCheckpoint();
        //if (transform.position.y <= DyingPoint.transform.position.y) transform.position = SpawnPoint.transform.position;

    }

    public void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Bonus")&&Life<3)
        {
            transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
            Destroy(obj.gameObject);
            Life++;
        }
    }

    public void BackCheckpoint()
    {
        Life = 2;
        transform.position = Checkpoint.transform.position;
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    

    private void Move(float _speed)
    {
        var targetVelocity = new Vector2(_speed, _rb.velocity.y);
        _rb.velocity = Vector3.SmoothDamp(_rb.velocity, targetVelocity, ref _velocity, 0);
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
