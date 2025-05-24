using UnityEngine;


public class ArmMechanic : MonoBehaviour
{

    [Header("General")]
    [SerializeField] private LayerMask _grableMask;
    [SerializeField] private float _followThreshold = 1f;
    private Rigidbody2D _grabbedObject;
    private bool _moveObjectXAxis = false;
    private bool _moveObjectYAxis = false;
    private Rigidbody2D _playerRigidBody2D;
    private Camera _cam;
    private Player _playerMov;
    [Header("Object Names")]
    [SerializeField] private string _moveXObject;
    [SerializeField] private string _moveYObject;
    [SerializeField] private string _pushPlayerObject;
    [SerializeField] private string _swingPlayerObject;
    [SerializeField] private string _rotateAroundAPointObject;
    [SerializeField] private string _rotateItselfObject;

    [Header("Push Yourself Towards Object Settings")]
    [SerializeField] private Vector3 _pushYourselfVelocity;
    private bool _canPushYourself = false;

    [Header("Grappling Hook")]
    [SerializeField] private float _lenghtOfTheArmToSwing;
    [SerializeField] private DistanceJoint2D _joint2D;
    private Vector3 _transPoint;
    private bool _canSwing = false;

    [Header("Line Renderer Settings")]
    [SerializeField] private LineRenderer _armLine;
    [SerializeField] private GameObject _arm;

    [Header("Rotation Settings")]
    [SerializeField] private float _rotateSpeed = 45f;
    [SerializeField] private float _rotateDetectorRadius;
    [SerializeField] private LayerMask _rotatePointLayer;
    private bool _rotateTowardsAPoint;
    private bool _rotateItself;
    private bool _thisGameObjectDoesntNeedConstraits = false;

    void Start()
    {
        _cam = Camera.main;
        _playerRigidBody2D = GetComponent<Rigidbody2D>();
        _joint2D.enabled = false;
        _playerMov = GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 _mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(_mousePos, Vector3.zero, Mathf.Infinity, _grableMask);
            if (hit.collider != null)
            {
                _grabbedObject = hit.transform.GetComponent<Rigidbody2D>();
                _transPoint = hit.point;
                _transPoint.z = 0;

                _moveObjectXAxis = hit.transform.gameObject.name.Contains(_moveXObject);
                _moveObjectYAxis = hit.transform.gameObject.name.Contains(_moveYObject);
                _canPushYourself = hit.transform.gameObject.name.Contains(_pushPlayerObject);
                _canSwing = hit.transform.gameObject.name.Contains(_swingPlayerObject);
                _rotateTowardsAPoint = hit.transform.gameObject.name.Contains(_rotateAroundAPointObject);
                _rotateItself = hit.transform.gameObject.name.Contains(_rotateItselfObject);

                _grabbedObject.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), _grabbedObject.GetComponent<Collider2D>(), true);
            }
        }

        if (Input.GetMouseButton(0) &&_grabbedObject != null)
        {
            _armLine.enabled = true;
            Vector3 _mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 _currentObjectPos = _grabbedObject.transform.position;
            _mousePos.z = _currentObjectPos.z;
            Vector3 _newPos = _mousePos;
            Vector2 _objectBaseSpeed = Vector2.zero;
            float _dist = Vector3.Distance(_newPos, _currentObjectPos);

            if (_moveObjectXAxis)
            {
                _newPos.x = _mousePos.x;
            }
            if (_moveObjectYAxis)
            {
                _newPos.y = _mousePos.y;
            }
            
            if (_dist > _followThreshold)
            {
                Vector3 _diff = (_newPos - _currentObjectPos).normalized;
                float _threshold = (_dist - _followThreshold) / 720;
                float _objectMoveSpeed = Mathf.Lerp(50f, 500f, _threshold);
                _objectBaseSpeed.x = _diff.x * _objectMoveSpeed;
                _objectBaseSpeed.y = _diff.y * _objectMoveSpeed;
            }

            if (_moveObjectXAxis) { _grabbedObject.linearVelocity = new Vector2(_objectBaseSpeed.x, _grabbedObject.linearVelocity.y); }
            if (_moveObjectYAxis) { _grabbedObject.linearVelocity = new Vector2(_grabbedObject.linearVelocityX, _objectBaseSpeed.y); }
            if (_moveObjectXAxis && _moveObjectYAxis) { _grabbedObject.linearVelocity = _objectBaseSpeed; }

            
            if (Input.GetMouseButton(1))
            {
                if (_canPushYourself)
                {
                    _playerMov.enabled = false;
                    Vector3 direction = -(gameObject.transform.position - _currentObjectPos);
                    Vector3 moveDir = direction.normalized;
                    float x = moveDir.x * _pushYourselfVelocity.x;
                    float y = moveDir.y * _pushYourselfVelocity.y;
                    float z = 0f;
                    _playerRigidBody2D.linearVelocity = new Vector3(x, y, z);
                }

                if (_canSwing)
                {
                    _playerMov.enabled = false;
                    _joint2D.connectedAnchor = _transPoint;
                    _joint2D.enabled = true;
                    _joint2D.distance = _lenghtOfTheArmToSwing;
                }
            }

            if (_rotateTowardsAPoint)
            {
                _thisGameObjectDoesntNeedConstraits = true;

                if (Input.GetKey(KeyCode.Q))
                {
                    Collider2D _rotateAroundPoint = Physics2D.OverlapCircle(transform.position, _rotateDetectorRadius, _rotatePointLayer);
                    _grabbedObject.transform.RotateAround(_rotateAroundPoint.transform.position, Vector3.forward, _rotateSpeed * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.E))
                {
                    Collider2D _rotateAroundPoint = Physics2D.OverlapCircle(transform.position, _rotateDetectorRadius, _rotatePointLayer);
                    _grabbedObject.transform.RotateAround(_rotateAroundPoint.transform.position, Vector3.forward, -_rotateSpeed * Time.deltaTime);
                }
            }

            if (_rotateItself)
            {
                if (Input.GetKey(KeyCode.Q))
                {
                    _grabbedObject.transform.Rotate(Vector3.forward, _rotateSpeed * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.E))
                {
                    _grabbedObject.transform.Rotate(Vector3.forward, -_rotateSpeed * Time.deltaTime);
                }
            }

            UpdateLineRenderer();
        }

        if (Input.GetMouseButtonUp(0))
        {
            _playerMov.enabled = true;
            _joint2D.enabled = false;
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), _grabbedObject.GetComponent<Collider2D>(), false);
            _grabbedObject.linearVelocity = Vector2.zero;
            if (!_thisGameObjectDoesntNeedConstraits)
                _grabbedObject.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
            _thisGameObjectDoesntNeedConstraits = false;
            _grabbedObject = null;
            _armLine.enabled = false;
        }

        void UpdateLineRenderer()
        {
            if (_grabbedObject != null)
            {
                _armLine.SetPosition(0, _arm.transform.position);
                _armLine.SetPosition(1, _grabbedObject.transform.position);
            }
        }
    }
     
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _rotateDetectorRadius);
    }
}

