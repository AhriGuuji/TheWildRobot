using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager Instance;
    private Vector2 _spawnPoint;
    [SerializeField] private Player _playerPrefab;

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
    }

    public void SetSpawnPoint(Vector2 _newSpawnPoint)
    {
        _spawnPoint = _newSpawnPoint;
    }

    public Vector2 GetSpawnPoint()
    {
        return _spawnPoint;
    }
}