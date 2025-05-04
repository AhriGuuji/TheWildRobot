using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyPlayer : MonoBehaviour
{
    private Player playerInstance;
    private GameObject playerGO;

    void Start()
    {
        playerInstance = FindAnyObjectByType<Player>();
        playerGO = playerInstance.GameObject();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(playerGO);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
