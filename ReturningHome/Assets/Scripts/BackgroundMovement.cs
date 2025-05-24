using Unity.VisualScripting;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] private GameObject _background;
    [SerializeField] private CameraFollow _cam;
    [SerializeField] private float _backgroundSpeed;
    private float _backgroundNewPos;

    void Awake()
    {
        _background.transform.position = new Vector2(_cam.transform.position.x,_cam.transform.position.y);
    }
    void Update()
    {
        _backgroundNewPos = Mathf.Lerp(_background.transform.position.x, _cam.transform.position.x, _backgroundSpeed * Time.deltaTime);
        _background.transform.position = new Vector2(_backgroundNewPos,0);
    }
}
