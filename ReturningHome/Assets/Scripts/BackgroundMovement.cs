using Unity.VisualScripting;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] private GameObject _background;
    [SerializeField] private CameraFollow _cam;
    [SerializeField] private float _backgroundSpeed;

    void Start()
    {
        _background.transform.position = FindAnyObjectByType<Player>().transform.position;
    }
    void Update()
    {
        _background.transform.position = Vector3.Lerp(_background.transform.position, _cam.transform.position, _backgroundSpeed * Time.deltaTime);
    }
}
