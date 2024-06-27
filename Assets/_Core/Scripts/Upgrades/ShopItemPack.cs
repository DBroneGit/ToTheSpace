using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/ItemPack")]
public class ShopItemPack : ScriptableObject
{
    [SerializeField] private ShopItem[] item; public ShopItem[] Item => item;
}
