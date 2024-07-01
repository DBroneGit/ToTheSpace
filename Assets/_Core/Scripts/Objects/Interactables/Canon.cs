using UnityEngine;

public class Canon : InteractableObject
{
    [SerializeField] private GameObject canonBall;
    [SerializeField] private GameObject goldCanonBall;
    [SerializeField] private SpriteRenderer sprite;
    
    [Header("Stats")]
    [SerializeField] private int canonForce = 30;
    [SerializeField] private float canonColdown = 5;
    private float time = 0;
    

    public override void Interact(Character character)
    {
        Shot();    
    }

    void Update()
    {
        time += Time.deltaTime;
        
        if(time < canonColdown) sprite.color = Color.red;
        else sprite.color = Color.white;
    }

    private void Shot()
    {
        if(time < canonColdown) return;

        float prob = Random.Range(0,1f);
        GameObject obj = null;
        
        if(prob < 0.9)
        {
            obj = PoolsManager.Instance.SearchPool(canonBall).GetObject();
        }
        else
        {
             obj = PoolsManager.Instance.SearchPool(goldCanonBall).GetObject();
        }

        obj.transform.rotation = sprite.transform.rotation;
        obj.transform.position = transform.position;
        obj.SetActive(true);
        obj.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * canonForce, ForceMode2D.Impulse);

        time = 0;
    }
}
