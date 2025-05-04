using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyPlayer : CheckPoint
{
    private Player playerInstance;
    private Collider2D playerCo;
    private GameObject playerGO;

    void Start()
    {
        playerInstance = FindAnyObjectByType<Player>();
        playerGO = playerInstance.GameObject();
        playerCo = playerGO.GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == playerCo)
        {
            if (HaveCheck == true)
            {
                playerGO.transform.position = CheckPointManager.Instance.GetSpawnPoint();
            }
            else if (HaveCheck == false)
            {
                playerGO.transform.position = CheckPointManager.Instance.GetSpawnPoint();
            }
        }
    }
}
