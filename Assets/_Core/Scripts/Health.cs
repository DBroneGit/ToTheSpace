using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    private SpriteRenderer sprite;

    [SerializeField] private int healthPoints = 3; 
    public int HealthPoints => healthPoints;
    [SerializeField] private int maxHealthPoints = 3; 
    public int MaxHealthPoints => maxHealthPoints;

    bool empty; public bool Empty => empty;

    void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    void OnEnable()
    {
        empty = false;
        healthPoints = maxHealthPoints;
        StartCoroutine(SetColor(Color.white, 0));
    }

    /// <summary>
    /// Aplica el daño sobre el objeto
    /// </summary>
    /// <param name="cantity">cantidad de daño a aplicar</param>
    public void ApplyDamage(int cantity)
    {
        if(empty) return;

        healthPoints -= cantity;    
        if(healthPoints <= 0)
        {
            empty = true;
        }
        else
        {
            DamagedAnimation();
        }
    }

    #region Animacion

    private void DamagedAnimation()
    {
        StartCoroutine(SetColor(Color.red, 0));
        StartCoroutine(SetColor(Color.white, 0.1f));
    }
    
    IEnumerator SetColor(Color color, float time)
    {
        yield return new WaitForSeconds(time);
        sprite.color = color;
    }
    #endregion

}
