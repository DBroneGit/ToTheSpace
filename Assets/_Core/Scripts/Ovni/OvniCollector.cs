using TMPro;
using UnityEngine;

public class OvniCollector : MonoBehaviour
{
    [SerializeField] private TMP_Text txt_Counter;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 7 || other.gameObject.layer == 9)
        {  
            Money.Cantity++;
            txt_Counter.text = Money.Cantity + "";
            PoolsManager.Instance.SearchPool(other.gameObject).Return(other.gameObject);
        }
    }
}
