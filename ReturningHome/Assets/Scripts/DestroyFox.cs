using UnityEngine;

public class DestroyFox : MonoBehaviour
{
    private Fox _fox;
    void OnTriggerEnter2D(Collider2D collision)
    {
        _fox = collision.GetComponent<Fox>();

        if (_fox)
        {
            Destroy(_fox.gameObject);
        }
    }
}
