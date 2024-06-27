using UnityEngine;

public class CameraChase : MonoBehaviour
{
    [SerializeField] private GameObject objectToChase;

    void Update()
    {
        transform.position = new Vector3(0, objectToChase.transform.position.y + 3.5f, -10);
    }
}
