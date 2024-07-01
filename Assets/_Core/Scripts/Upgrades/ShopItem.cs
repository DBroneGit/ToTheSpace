
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Item")]
[Serializable]
public class ShopItem : ScriptableObject
{
    [SerializeField] protected string itemName; public string ItemName => itemName;
    [SerializeField] protected Sprite itemIcon; public Sprite ItemIcon => itemIcon;
    [SerializeField] protected int price; public int Price => price;
    [SerializeField] private int altitudeRequired = 0; public int AltitudeRequired => altitudeRequired;
    [SerializeField] private ShopItem[] itemsRequired; public ShopItem[] ItemsRequired => itemsRequired;
    [SerializeField] protected bool sold; public bool Sold => sold;

    [SerializeField] private GameEvent upgradeEvent;

    public void Desactivate()
    {
        sold = false;
    }
    public void Activate(){
        if(Money.Cantity >= Price && sold == false)
        {
            upgradeEvent.Raise();
            Money.Cantity -= Price;
            sold = true;
        }
    }
}
