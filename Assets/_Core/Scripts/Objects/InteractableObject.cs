using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    [SerializeField] protected bool requireEnemy;

    public abstract void Interact(Character character);

}
