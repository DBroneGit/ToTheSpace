
using UnityEngine;

public class Egg : InteractableObject
{
    [SerializeField] private int damage;

    void OnEnable()
    {
        gameObject.layer = 9;
    }
    public override void Interact(Character character)
    {
        Debug.Log("interact");
        character.PickUp(gameObject);
        gameObject.layer = 8;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == 7)
        {
            other.gameObject.GetComponent<Health>().ApplyDamage(damage);
        }

        PoolsManager.Instance.SearchPool(gameObject).Return(gameObject);   
    }
}
