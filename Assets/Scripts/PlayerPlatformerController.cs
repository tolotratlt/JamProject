using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject {

    public enum DashState
    {
        Ready,
        Dashing,
        Cooldown
    }

    public enum AxisDirection
    {
        None,
        Left,
        Right
    }

    [Header("Movement")]
    public float maxSpeed = 7f;
    public float jumpTakeOffSpeed = 7f;

    private SpriteRenderer spriteRenderer;
    //private Animator animator;

    private Vector2 orientation;

    //old horizontal direction
    private float oldHDirection = 0f;

    [Header("Dash")]
    //dash
    [SerializeField]
    private float dashSpeed = 3f; //must be greater than max speed    
    DashState dashState = DashState.Ready;
    private float dashTimer = 0f;
    //dash duration (in s)
    [SerializeField]
    private float dashDuration = 0.3f;
    //dash cooldown duration (in s) until news dash. Cooldown is the elasped time you can't perform dash after dash
    [SerializeField]
    private float cooldownDuration = 0.15f;
    //double click detection variables
    [SerializeField]
    private float timeForNextKey = 0.35f;
    private float keyDuration = 0f;
    private bool isAxisInUse = false;
    private bool performDash = false;

    private AxisDirection currentAxisDirection = AxisDirection.None;
    private AxisDirection previousAxisDirection = AxisDirection.None;

    [Header("Other")]
    [SerializeField]
    private bool inverseFlip = false;
    

    // Use this for initialization
    void Awake () 
    {
        spriteRenderer = GetComponent<SpriteRenderer> ();
        //animator = GetComponent<Animator> ();
        orientation = Vector2.right;

    }

    new void Start()
    {
        base.Start();

        //mandatory. avoid bug rotation 
        rb2d.freezeRotation = true;
    }

    protected override void ComputeVelocity()
    {
        //input check
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxisRaw ("Horizontal"); //Raw to avoid automatic lerp calculation

        //perform jump
        if (Input.GetButtonDown ("Jump") && grounded) {
            velocity.y = jumpTakeOffSpeed;
        } else if (Input.GetButtonUp ("Jump")) 
        {
            if (velocity.y > 0) {
                velocity.y = velocity.y * 0.5f;
            }
        }

        //process axis direction
        if (move.x != oldHDirection) // check if holding
        {
            //when left or right
            if (move.x != 0) // check if direction or neutral
            {
                if (!isAxisInUse) //check direction
                {

                    if (move.x > 0)
                    {
                        //if previous input was right, perform dash
                        currentAxisDirection = AxisDirection.Right;
                        Debug.Log("Right");

                        if (previousAxisDirection == AxisDirection.Right && keyDuration <= timeForNextKey)
                        {
                            //performDash = true;
                            //Debug.Log("PErform Dash");

                            OnPerformDash();
                            dashState = DashState.Dashing;
                            orientation = move;
                        }
                        else
                        {
                            //performDash = false;
                        }
                    }
                    else
                    {
                        currentAxisDirection = AxisDirection.Left;
                        Debug.Log("Left");

                        if (previousAxisDirection == AxisDirection.Left && keyDuration <= timeForNextKey)
                        {
                            //performDash = true;
                            //Debug.Log("PErform Dash");

                            OnPerformDash();
                            dashState = DashState.Dashing;
                            orientation = move;
                        }
                        else
                        {
                            //performDash = false;
                        }
                    }


                    keyDuration = 0f;
                    Debug.Log("Reset key duration");
                    isAxisInUse = true;
                }
            }
            else
            {
                //when neutral
                previousAxisDirection = currentAxisDirection;
                currentAxisDirection = AxisDirection.None;

                //Debug.Log("None");
                isAxisInUse = false;
            }            
        }
        else
        {
            keyDuration += Time.deltaTime;
        }


        //check and perform dash input
        //if (move.x != 0f)
        //{
        /*
        if (dashState == DashState.Ready)
        {
            bool isDashKeyDown = Input.GetButtonDown("Dash");
            if (isDashKeyDown)
            {
                Debug.Log("Dash mode");
                dashState = DashState.Dashing;
            }

        }
        else if (dashState == DashState.Dashing)*/

            if (dashState == DashState.Ready)
            {
            } else if (dashState == DashState.Dashing) 
            {
                dashTimer += Time.deltaTime;
                if (dashTimer >= dashDuration)
                {

                    dashState = DashState.Cooldown;
                    dashTimer = 0f;
                }
            }
            else if (dashState == DashState.Cooldown)
            {
                dashTimer += Time.deltaTime;
                if (dashTimer >= cooldownDuration)
                {

                    dashState = DashState.Ready;
                    dashTimer = 0f;
                }

            }
        //}


        //check orientation. One shot
        if (move.x != oldHDirection)
        {
             
            if (move.x > 0)
            {
                orientation = Vector2.right;
                spriteRenderer.flipX = !inverseFlip? false : true;
                
            }
            else if( move.x < 0)
            {
                orientation = Vector2.left;
                spriteRenderer.flipX = !inverseFlip ? true : false;
            }
        }        

        //move
        if(dashState == DashState.Dashing)
            targetVelocity = orientation * dashSpeed;
        else
            targetVelocity = move * maxSpeed;

        oldHDirection = move.x;
    }

    void OnPerformDash()
    {

    }
}