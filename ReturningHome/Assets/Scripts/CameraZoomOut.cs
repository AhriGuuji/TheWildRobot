using Unity.Mathematics;
using UnityEngine;

public class CameraZoomOut : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private Camera _cam;
    [SerializeField] private CameraFollow _offset;
    [Header("Zoom")]
    [SerializeField] private int _zoomMin;
    [SerializeField] private int _zoomMax;
    [SerializeField] private float _zoomMultiplayer;
    [SerializeField] private float _zoomSpeed;
    [SerializeField] private float _zoomTimer;
    private float _currentZoomSize;
    [Header("OffSet When Zoom")]
    [SerializeField] float _offsetMax;
    [SerializeField] float _getToOffsetTargetSpeed;
    private float _targetOffset;
    private float _originalOffsetY;
    //Input Component
    private float _scroll;

    void Start()
    {
        _currentZoomSize = _cam.orthographicSize;
        _originalOffsetY = _offset.offset.y;
    }
    void Update()
    {
        _scroll = Input.GetAxis("Mouse ScrollWheel");
        _currentZoomSize += _scroll * _zoomMultiplayer;

        if (_scroll > 0) _targetOffset = _originalOffsetY + _offsetMax;
        else if (_scroll < 0) _targetOffset = _originalOffsetY - _offsetMax;

        _currentZoomSize = Mathf.Clamp(_currentZoomSize, _zoomMin, _zoomMax);
        _targetOffset = Mathf.Clamp(_targetOffset, _originalOffsetY, _zoomMax);

        _cam.orthographicSize = Mathf.SmoothDamp(_cam.orthographicSize, _currentZoomSize, ref _zoomSpeed, _zoomTimer);
        _offset.offset.y = Mathf.Lerp(_offset.offset.y, _targetOffset, _getToOffsetTargetSpeed*Time.deltaTime);

    }
}
