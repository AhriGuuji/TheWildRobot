using System.Linq.Expressions;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Player _player;
    public bool _HaveCheck { get; private set; }
    public Vector2 _StoredPos{ get; private set; }

    void Start()
    {
        _HaveCheck = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        _player = collision.GetComponent<Player>();

        if (_player)
        {
            if (_HaveCheck == false)
            {
                _HaveCheck = true;
                _StoredPos = _player.transform.position;
                CheckPointManager.Instance.SetSpawnPoint(_StoredPos);
                gameObject.GetComponent<Collider2D>().enabled = false;
            }

            if (_HaveCheck == true)
            {
                _StoredPos = new Vector3();
                _StoredPos = _player.transform.position;
                CheckPointManager.Instance.SetSpawnPoint(_StoredPos);
                gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}
