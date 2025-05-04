using Unity.VisualScripting;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager Instance;
    private Vector2 spawnPoint;

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
        
        spawnPoint = FindAnyObjectByType<Player>().GameObject().transform.position;
    }

    public void SetSpawnPoint(Vector2 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }

    public Vector2 GetSpawnPoint()
    {
        return spawnPoint;
    }

    public void RespawnPlayer()
    {
        GameObject player = FindAnyObjectByType<Player>().GameObject();
        player.transform.position = spawnPoint;
    }
}