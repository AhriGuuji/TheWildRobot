using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private string nextLevel; 
    private Collider2D playerCO;

    void Start()
    {
        playerCO = FindAnyObjectByType<Player>().GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == playerCO)
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}
