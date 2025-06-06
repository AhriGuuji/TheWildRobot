using UnityEngine;

public class BearAppear : MonoBehaviour
{
    [SerializeField] private GameObject _bear;
    [SerializeField] private Transform _bearPos;
    private Player _player;

    void OnTriggerEnter2D(Collider2D collision)
    {
        _player = collision.GetComponent<Player>();

        if (_player)
        {
            Instantiate(_bear, _bearPos);
            Destroy(gameObject);
        }
    }
}
