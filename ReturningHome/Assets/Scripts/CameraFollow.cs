using System;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    enum Type { Teleport, Linear, FeedbackLoop };

    [SerializeField] private Type       type;
    [SerializeField] private Transform  targetEntity;
    [SerializeField] private Vector3    offset;
    [SerializeField] private float viewAbove;
    [SerializeField] private float      maxSpeed;
    [SerializeField] private float verticalMoveSpeed;
    private Vector2 originalOffSet;
    private Vector2 targetOffset;

    [Header("Camera BounderyBox")]
    [SerializeField] private float topLimite    = 1f;
    [SerializeField] private float bottomLimite = -1f;  
    [SerializeField] private float leftLimite   = -1f; 
    [SerializeField] private float rightLimite  = 1f; 

    void Start()
    {
        originalOffSet = offset;
        targetOffset = originalOffSet;
    }

    void Update()
    {
        // Check for W key input
        if (Input.GetKeyDown(KeyCode.W))
        {
            targetOffset = originalOffSet + new Vector2(0, viewAbove);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            targetOffset = originalOffSet;
        }

        // Smoothly interpolate between current offset and target offset
        offset = Vector3.Lerp(offset, targetOffset, verticalMoveSpeed * Time.deltaTime);
    }
    void FixedUpdate()
    {
        if (targetEntity == null)
        {
            Player go = FindAnyObjectByType<Player>();

            if (go == null) return;

            targetEntity = go.transform;
        }

        Vector3 currentTarget = GetTargetPosition();
        currentTarget.z = transform.position.z;
        currentTarget = currentTarget + offset;

        switch (type)
        {
            case Type.Teleport:
                transform.position = new Vector3(Math.Clamp(currentTarget.x, leftLimite, rightLimite), Math.Clamp(currentTarget.y, bottomLimite, topLimite), currentTarget.z);
                break;
            case Type.Linear:
                transform.position = Vector3.MoveTowards(transform.position, currentTarget, maxSpeed * Time.fixedDeltaTime);
                break;
            case Type.FeedbackLoop:
                {
                    Vector3 toTarget = currentTarget - transform.position;

                    transform.position = new Vector3(Math.Clamp(transform.position.x + toTarget.x * maxSpeed, leftLimite, rightLimite), Math.Clamp(transform.position.y + toTarget.y * maxSpeed, bottomLimite,topLimite), currentTarget.z);;
                }
                break;
            default:
                break;
        }

    }

    Vector3 GetTargetPosition()
    {
        return targetEntity.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(new Vector2(leftLimite,topLimite), new Vector2(rightLimite,topLimite));
        Gizmos.DrawLine(new Vector2(leftLimite,bottomLimite), new Vector2(rightLimite,bottomLimite));
        Gizmos.DrawLine(new Vector2(leftLimite,topLimite), new Vector2(leftLimite,bottomLimite));
        Gizmos.DrawLine(new Vector2(rightLimite,topLimite), new Vector2(rightLimite,bottomLimite));
    }

}
