using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class ArmMechanic : MonoBehaviour
{

    [Header("General")]
    [SerializeField] private LayerMask mask;
    [SerializeField] private float followThreshold = 1f;
    private Rigidbody2D _trans;
    private bool moveX = false;
    private bool moveY = false;

    [Header("Grapple Settings")]
    [SerializeField] private Vector3 grapVelocity;
    private bool grappling = false;

    [Header("Grappling Hook")]
    [SerializeField] private float grappleLenght;
    private Vector3 _trans_point;
    private DistanceJoint2D joint;
    private bool grapHook = false;

    [Header("Line Renderer Settings")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject _arm;

    [Header("Rotation Settings")]
    [SerializeField] private float rotateSpeed = 45f;
    [SerializeField] private float rotateDetectorRadius;
    [SerializeField] private LayerMask rotatePointLayer;
    private bool rotateAround;
    private bool rotate;
    private Rigidbody2D playerRB;
    private Camera _cam;
    private Player stopMov;

    void Start()
    {
        _cam = Camera.main;
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        joint = gameObject.GetComponent<DistanceJoint2D>();
        joint.enabled = false;

        stopMov = GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = _cam.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.zero, Mathf.Infinity, mask);
            if (hit.collider != null)
            {
                _trans = hit.transform.GetComponent<Rigidbody2D>();
                _trans_point = hit.point;
                _trans_point.z = 0;
                
                moveX = hit.transform.gameObject.name.Contains("MoveX");
                moveY = hit.transform.gameObject.name.Contains("MoveY");
                grappling = hit.transform.gameObject.name.Contains("Grappling");
                grapHook = hit.transform.gameObject.name.Contains("GrapHook");
                rotateAround = hit.transform.gameObject.name.Contains("RotateAround");
                rotate = hit.transform.gameObject.name.Contains("Rotate");
            }
        }

        if (Input.GetMouseButton(0) && _trans != null)
        {
            Vector3 mousepos = _cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 current = _trans.transform.position;
            mousepos.z = current.z;
            Vector3 newPos = current;
            
            if (moveX)
            {
                newPos.x = mousepos.x;
            }
            if (moveY)
            {
                newPos.y = mousepos.y;
            }

            if (grappling && Input.GetMouseButton(1))
            {
                stopMov.enabled = false;
                Vector3 direction = -(gameObject.transform.position - current);
                Vector3 moveDir = direction.normalized;
                float x = moveDir.x * grapVelocity.x;
                float y = moveDir.y * grapVelocity.y;
                float z = 0f;
                playerRB.linearVelocity = new Vector3(x, y, z);
            }
            if (grapHook && Input.GetMouseButton(1))
            {
                stopMov.enabled = false;
                joint.connectedAnchor = _trans_point;
                joint.enabled = true;
                joint.distance = grappleLenght;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                joint.enabled = false;
                stopMov.enabled = true;
            }

            if (rotateAround && Input.GetKey(KeyCode.Q))
            {
                Collider2D rotatePoint = Physics2D.OverlapCircle(transform.position, rotateDetectorRadius, rotatePointLayer);
                _trans.transform.RotateAround(rotatePoint.transform.position, Vector3.forward, rotateSpeed * Time.deltaTime);
            }
            if (rotateAround && Input.GetKey(KeyCode.E))
            {
                Collider2D rotatePoint = Physics2D.OverlapCircle(transform.position, rotateDetectorRadius, rotatePointLayer);
                _trans.transform.RotateAround(rotatePoint.transform.position, Vector3.forward, -rotateSpeed * Time.deltaTime);
            }

            if (rotate && Input.GetKey(KeyCode.Q))
            {
                Collider2D rotatePoint = Physics2D.OverlapCircle(transform.position, rotateDetectorRadius, rotatePointLayer);
                _trans.transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
            }
            if (rotate && Input.GetKey(KeyCode.E))
            {
                Collider2D rotatePoint = Physics2D.OverlapCircle(transform.position, rotateDetectorRadius, rotatePointLayer);
                _trans.transform.Rotate(Vector3.forward, -rotateSpeed * Time.deltaTime);
            }

            float baseSpeedX = 0;
            float baseSpeedY = 0;

            lineRenderer.enabled = true;

            float dist = Vector3.Distance(newPos, current); // / 700 300 = 0.5
            if (dist > followThreshold)
            {
                Vector3 diff = (newPos - current).normalized;
                //_trans.position += new Vector3(x, y, 0);
                //float moveSpeed = dist > 16 ? 200f : 50f;
                float t = (dist - followThreshold) / 720; // 720 being world view size on screen
                float moveSpeed = Mathf.Lerp(50f, 500f, t); // 50 = speed on 0, 500 = speed on 1.
                baseSpeedX = diff.x * moveSpeed;
                baseSpeedY = diff.y * moveSpeed;
                //Debug.Log($"{newPos}\n{current}/{diff}");                
            }
            if (moveX) { _trans.linearVelocity = new Vector2(baseSpeedX, _trans.linearVelocity.y); }
            if (moveY) { _trans.linearVelocity = new Vector2(_trans.linearVelocityX, baseSpeedY); }
            if (moveX || moveY) { _trans.linearVelocity = new Vector2(baseSpeedX, baseSpeedY); }
            UpdateLineRenderer();

            if (_trans != null)
            {
                _trans.constraints = RigidbodyConstraints2D.None;
                _trans.constraints = RigidbodyConstraints2D.FreezeRotation;
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), _trans.GetComponent<Collider2D>(), true);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            stopMov.enabled = true;
            joint.enabled = false;
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), _trans.GetComponent<Collider2D>(), false);
            _trans.linearVelocity = Vector2.zero;
            _trans.constraints = RigidbodyConstraints2D.FreezePositionX;
            _trans.constraints = RigidbodyConstraints2D.FreezeRotation;
            _trans = null;
            lineRenderer.enabled = false;
        }

        void UpdateLineRenderer()
        {
            if (_trans != null)
            {
                lineRenderer.SetPosition(0, _arm.transform.position);
                lineRenderer.SetPosition(1, _trans.transform.position); 
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rotateDetectorRadius);
    }
}
