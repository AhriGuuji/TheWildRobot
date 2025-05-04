using UnityEngine;

public class DeleteYourself : MonoBehaviour
{
    private CameraFollow cam;
    void Start()
    {
        cam = FindAnyObjectByType<CameraFollow>();
    }
    void Update()
    {
        if(cam.Invert) {Destroy(gameObject);}
    }
}
