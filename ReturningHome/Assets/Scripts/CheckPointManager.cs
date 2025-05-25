using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager Instance;
    private Vector2 _spawnPoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        _spawnPoint = FindAnyObjectByType<Player>().gameObject.transform.position;
    }

    public void SetSpawnPoint(Vector2 _newSpawnPoint)
    {
        _spawnPoint = _newSpawnPoint;
    }

    public Vector2 GetSpawnPoint()
    {
        return _spawnPoint;
    }

    public void RespawnPlayer()
    {
        GameObject _player = FindAnyObjectByType<Player>().gameObject;
        _player.transform.position = _spawnPoint;
    }
}