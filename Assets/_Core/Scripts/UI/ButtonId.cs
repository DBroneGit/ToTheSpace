using UnityEngine;

public class ButtonId : MonoBehaviour
{
    private ShopDataBase shop;
    
    [SerializeField] private int id; public int Id { get => id; set => id = value; }
    
    void Awake()
    {
        shop = GameObject.Find("ShopManager").GetComponent<ShopDataBase>();    
    }
    public void Buy()
    {
        shop.Buy(id);
    }
}
