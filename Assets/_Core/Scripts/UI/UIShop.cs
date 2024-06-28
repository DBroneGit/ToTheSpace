using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : MonoBehaviour
{
    [SerializeField] GameObject PNL_Shop;
    [SerializeField] GameObject PNL_ClicProtector;

    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private GameObject[] buttonlist;
    [SerializeField] private GameObject defaultButton;
    [SerializeField] private Sprite empty;
    

    public void OpenShop(ShopItemPack itemPack)
    {
        PNL_ClicProtector.SetActive(true);
        PNL_Shop.SetActive(true);

        for(int i = 0; i < itemPack.Item.Length; i++)
        {
            CanBuy(itemPack.Item[i], buttonlist[i].GetComponent<Button>());
        }
    }

    public void CloseShop()
    {
        PNL_ClicProtector.SetActive(false);
        PNL_Shop.SetActive(false);
    }

    public void AddItem(ShopItem item, int id)
    {
        buttonlist[id].GetComponentInChildren<TMP_Text>().text = item.Price + "";
        buttonlist[id].GetComponentInChildren<Image>().sprite = item.ItemIcon;
        buttonlist[id].GetComponent<ButtonId>().Id = id;
        buttonlist[id].transform.localScale = Vector3.one;
        
    }

    public void CanBuy(ShopItem item, Button button)
    {
        
        bool canBuy = true;
        //Si algunos de sus requerimentos no esta cubierto, no se puede comprar
        for(int i = 0; i < item.ItemsRequired.Length; i++)
        {
            if(item.ItemsRequired[i].Sold == false) canBuy = false;
        }
      
        //Si ya se vendio, no se puede comprar
        if(item.Sold == true){ canBuy = false; button.GetComponentInChildren<TMP_Text>().text = "SOLD";} 
        //Si no tienes dinero suficiente, no se puede comprar
        if(Money.Cantity < item.Price) canBuy = false; 

        if(canBuy == false)
        {
            button.GetComponent<Image>().sprite = empty;
        }
        else button.GetComponent<Image>().sprite = item.ItemIcon;
        
        button.enabled = canBuy;
    }
}
