using UnityEngine;

public class RunningAway : MonoBehaviour
{
    [SerializeField] private Vector2 Velocity;
    [SerializeField] private Animator animator;

    void Update()
    {

        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

        rb.linearVelocityX = Velocity.x;

        animator.SetFloat("VelocityX", Velocity.x);
    }
}
