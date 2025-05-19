using UnityEngine;

public class DestroyFox : MonoBehaviour
{
    private GameObject foxy;
    private Collider2D foxCo;

    void Update()
    {
        if (foxy == null)
        {
            foxy = GameObject.FindGameObjectWithTag("Fox");
            foxCo = foxy.GetComponent<Collider2D>(); 
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == foxCo)
        {
            Destroy(foxy);
        }
    }
}
