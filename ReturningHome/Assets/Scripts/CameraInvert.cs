using Unity.VisualScripting;
using UnityEngine;

public class CameraInvert : MonoBehaviour
{
    [SerializeField] private GameObject bear;
    private GameObject playerGO;
    private Collider2D playerCO;
    private CameraFollow cam;

    void Start()
    {
        playerGO = FindAnyObjectByType<Player>().GameObject();
        playerCO = playerGO.GetComponent<Collider2D>();
        cam = FindAnyObjectByType<CameraFollow>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == playerCO)
        {
            cam.Invert = true;
            Instantiate(bear,gameObject.transform);
            Destroy(gameObject);
        }
    }
}
