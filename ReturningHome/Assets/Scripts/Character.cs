using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected Vector2 velocity;
    [SerializeField]
    protected Transform groundCheck;
    [SerializeField]
    protected float groundCheckRadius = 2.0f;
    [SerializeField]
    protected LayerMask groundLayer;

    protected Rigidbody2D rb;
    protected Animator animator;
    protected bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if ((GetDirection() < 0) && (transform.right.x > 0))
        {
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        else if ((GetDirection() > 0) && (transform.right.x < 0))
        {
            transform.rotation = Quaternion.identity;
        }

        Vector2 currentVelocity = rb.linearVelocity;

        animator.SetFloat("AbsVelocityX", Mathf.Abs(currentVelocity.x));
        animator.SetFloat("VelocityY", currentVelocity.y);
        animator.SetBool("isGrounded", isGrounded);
    }
    protected void ComputeGroundState()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        isGrounded = (collider != null);
    }

    protected virtual void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    protected abstract float GetDirection();
}
