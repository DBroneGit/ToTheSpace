using UnityEngine;

public class OutOfBorder : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 6)
        {
            other.transform.position = gameObject.transform.parent.parent.position + Vector3.up * 3;
            Money.Cantity = Money.Cantity - 10;
        }
        if(other.gameObject.layer >= 7 && other.gameObject.layer <= 9)
        {
            PoolsManager.Instance.SearchPool(other.gameObject).Return(other.gameObject);
        }
    }
}
