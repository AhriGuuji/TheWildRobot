using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyPlayer : MonoBehaviour
{
    [SerializeField] private Player _player;
    void OnTriggerEnter2D(Collider2D collision)
    {
        _player = collision.GetComponent<Player>();

        if (_player)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
