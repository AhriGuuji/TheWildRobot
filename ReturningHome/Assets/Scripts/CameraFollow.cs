using System;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    enum Type { Teleport, Linear, FeedbackLoop };

    [SerializeField] private Type       type;
    [SerializeField] private Transform  targetEntity;
    [SerializeField] public Vector3    offset;
    [SerializeField] private float      viewAbove;
    [SerializeField] private float      viewDown;
    [SerializeField] private float      maxSpeed;
    [SerializeField] private float      verticalMoveSpeed;
    public bool Invert {get; set;}
    private Vector2 originalOffSet;
    private Vector2 targetOffset;
    private float originalXOffset;

    [Header("Camera BounderyBox")]
    [SerializeField] private float      topLimite = 1f;
    [SerializeField] private float      bottomLimite = -1f;  
    [SerializeField] private float      leftLimite = -1f; 
    [SerializeField] private float      rightLimite = 1f; 

    void Start()
    {
        originalXOffset = offset.x;
        originalOffSet = offset;
        targetOffset = originalOffSet;
    }

    void Update()
    {
        if(Invert)
        {
            offset.x = -originalXOffset;
        }  

        if (Input.GetKeyDown(KeyCode.W))
        {
            targetOffset = originalOffSet + new Vector2(0, viewAbove);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            targetOffset = originalOffSet;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            targetOffset = originalOffSet + new Vector2(0, -viewDown);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            targetOffset = originalOffSet;
        }

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
