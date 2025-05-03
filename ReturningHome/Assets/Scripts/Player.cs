using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private string horizontalAxisName = "Horizontal";
    [SerializeField]
    private float jumpMaxDuration;
    [SerializeField]
    private float jumpGravity;
    [SerializeField]
    private float speed;

    private float jumpTimer;
    private float originalGravity;
    private float moveDir;

    protected override void Start()
    {
        base.Start();

        originalGravity = rb.gravityScale;
    }

    // Update is called once per frame
    protected override void Update()
    {
        ComputeGroundState();

        moveDir = Input.GetAxis(horizontalAxisName);

        Vector2 currentVelocity = rb.linearVelocity;

        if(Input.GetKey(KeyCode.LeftShift))
        {
            currentVelocity.x = moveDir * velocity.x * speed;
        }
        else
        {
            currentVelocity.x = moveDir * velocity.x;
        }
        

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                currentVelocity.y = velocity.y;
                //jumpTimer = 0.0f;
                rb.gravityScale = jumpGravity;
            }
        }
        else if (rb.linearVelocityY <= 0) 
        {
            //jumpTimer = jumpTimer + Time.deltaTime;
            if (Input.GetButton("Jump"))
            {
                rb.gravityScale = jumpGravity;
            }
            else
            {
                //jumpTimer = jumpMaxDuration;
                rb.gravityScale = originalGravity;
            }
        }
        else
        {
            rb.gravityScale = originalGravity;
        }

        if ((isGrounded) && (jumpTimer >= jumpMaxDuration) && (Mathf.Abs(currentVelocity.y) > 1.0f))    
        {
            currentVelocity.y = 0.0f;
        }

        rb.linearVelocity = currentVelocity;

        base.Update();
    }

    protected override float GetDirection()
    {
        return moveDir;
    }
}
