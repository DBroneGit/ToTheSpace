using UnityEngine;

public class ShopDataBase : MonoBehaviour
{
    private UIShop UI;

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
        for(int i = 0; i < itemPack.Length; i++)
        {
            for(int j = 0; j < itemPack[i].Item.Length; j++)
            {
                if(itemPack[i].Item[j] != null) UI.AddItem(itemPack[i].Item[j], (i * 4) + j);
            }
        }
    }
    
    /// <summary>
    /// Abre la tienda desde un evento
    /// </summary>
    public void OpenShop()
    {
        LoadUpgrades();

        player.ShopWasOpen();
        UI.OpenShop(itemPack);
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
        int level = id / 4;
        try{itemPack[level].Item[id % 4].Activate();} catch {Debug.Log("Esta vacio");}
        CloseShop();
    }
    
}
