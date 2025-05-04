using System.Linq.Expressions;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool HaveCheck{get; private set; }
    private Collider2D playerCollider;
    public Vector2 StoredPos{get; private set;}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HaveCheck = false;
        playerCollider = FindAnyObjectByType<Player>().GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == playerCollider)
        {
            Debug.Log($"got ya: {HaveCheck}");
            if (HaveCheck == false)
            {
                HaveCheck = true;
                Debug.Log($"got in here: {HaveCheck}");
                StoredPos = playerCollider.transform.position;
                CheckPointManager.Instance.SetSpawnPoint(StoredPos);
                gameObject.GetComponent<Collider2D>().enabled = false;
            }

            if (HaveCheck == true)
            {
                Debug.Log($"tell me more: {HaveCheck}");
                StoredPos = new Vector3();
                StoredPos = playerCollider.transform.position;
                CheckPointManager.Instance.SetSpawnPoint(StoredPos);
                gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}
