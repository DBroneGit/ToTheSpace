using UnityEngine;

public class Shop : InteractableObject
{
    [SerializeField] private GameEvent OpenShop;

    public override void Interact(Character character)
    {
        OpenShop.Raise();
    }
}
