using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private string _nextLevel;
    private Player _player;

    void OnTriggerEnter2D(Collider2D collision)
    {
        _player = collision.GetComponent<Player>();

        if (_player)
        {
            SceneManager.LoadScene(_nextLevel);
        }
    }
}
