using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    [SerializeField] Camera _toDeactivate;
    [SerializeField] Camera _toActivate;
    private Player _player;
    void OnTriggerEnter2D(Collider2D collision)
    {
        _player = collision.GetComponent<Player>();

        if (_player)
        {
            _toDeactivate.enabled = false;
            _toActivate.enabled = true;
        }
    }
}
