using TMPro;
using UnityEngine;

public class OvniCollector : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 7 || other.gameObject.layer == 9)
        {  
            if(other.gameObject.tag == "Enemy")
            {
                Money.Cantity += other.GetComponent<Enemy>().Price;
                PoolsManager.Instance.SearchPool(other.gameObject).Return(other.gameObject);
            }
        }
    }
}
