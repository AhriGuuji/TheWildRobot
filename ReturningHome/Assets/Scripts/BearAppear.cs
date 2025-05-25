using UnityEngine;

public class BearAppear : MonoBehaviour
{
    [SerializeField] private GameObject _bear;
    [SerializeField] private Transform _bearPos;
    [SerializeField] private GameObject _player;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == _player)
        {
            Instantiate(_bear, _bearPos);
            Destroy(gameObject);
        }
    }
}
