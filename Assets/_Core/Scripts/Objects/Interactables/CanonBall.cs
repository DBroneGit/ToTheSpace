using UnityEngine;

public class CanonBall : MonoBehaviour
{
    [SerializeField] private int damage;

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == 7)
        {
            other.gameObject.GetComponent<Health>().ApplyDamage(damage);
            other.gameObject.GetComponent<EnemyIA>().ShotDown = true;
        }

        PoolsManager.Instance.SearchPool(gameObject).Return(gameObject);   
    }
}
