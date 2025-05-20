using Unity.VisualScripting;
using UnityEngine;

public class BearAppear : MonoBehaviour
{
    [SerializeField] private GameObject bearPrefab;
    [SerializeField] private Transform instantPos;
    private GameObject playerGO;
    private Collider2D playerCO;

    void Start()
    {
        playerGO = FindAnyObjectByType<Player>().GameObject();
        playerCO = playerGO.GetComponent<Collider2D>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == playerCO)
        {
            Instantiate(bearPrefab, instantPos);
            Destroy(gameObject);
        }
    }
}
