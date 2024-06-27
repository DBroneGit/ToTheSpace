using UnityEngine;

public class ShopDataBase : MonoBehaviour
{
    private UIShop UI;

    [SerializeField] private int shipLvl = 0;
    [SerializeField] private ShopItemPack[] itemPack;  
    [SerializeField] private Character player;
    private bool itemsLoaded;

    private void Awake()
    {
        UI = GetComponent<UIShop>();

        //Desactivamos todas las mejoras
        for(int i = 0; i < itemPack.Length; i++)
        {
            for(int j = 0; j < itemPack[i].Item.Length; j++)
            {
                itemPack[i].Item[j].Desactivate();
            }
        }
    }
    /// <summary>
    /// Carga todas las mejoras
    /// </summary>
    private void LoadUpgrades()
    {
        for(int i = 0; i < itemPack[shipLvl].Item.Length; i++)
        {
            if(itemPack[shipLvl].Item[i] != null) UI.AddItem(itemPack[shipLvl].Item[i], i);
        }
        itemsLoaded = true;
    }
    
    /// <summary>
    /// Abre la tienda desde un evento
    /// </summary>
    public void OpenShop()
    {
        if(itemsLoaded == false) LoadUpgrades();

        player.ShopWasOpen();
        UI.OpenShop(itemPack[shipLvl]);
    }

    /// <summary>
    /// Cierra la tienda desde un evento
    /// </summary>
    public void CloseShop()
    {
        player.ShopWasClose();
        UI.CloseShop();
    }
    
    public void Buy(int id)
    {
        try{itemPack[shipLvl].Item[id].Activate();} catch {Debug.Log("Esta vacio");}
        CloseShop();
    }
    
}
