using System.Linq.Expressions;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Player _player;
    private Vector2 _storedPos;

    void OnTriggerEnter2D(Collider2D collision)
    {
        _player = collision.GetComponent<Player>();

        if (_player)
        {
            _storedPos = _player.transform.position;
            CheckPointManager.Instance.SetSpawnPoint(_storedPos);
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
}

