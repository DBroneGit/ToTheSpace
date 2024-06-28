using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Punch : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Rigidbody2D rgbody;

    [SerializeField] private float punchForce = 10f;
    [SerializeField] private float lifetime = 0.3f;
    [SerializeField] private int damage = 1;
    private float time = 0;
    
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        rgbody = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        Move();
    }

    void Update()
    {
        time += Time.deltaTime;

        if(time >= lifetime)
        {
            Attack();
            time = 0;
        }
    }

    /// <summary>
    /// Mueve el puño
    /// </summary>
    private void Move()
    {
        rgbody.AddRelativeForce(new Vector2(punchForce, 0), ForceMode2D.Impulse);

    }
    
    private void Attack()
    {
        Collider2D obj = Physics2D.OverlapCircle(transform.position, 0.35f, 1<<7);
        if(obj != null) if(obj.gameObject.layer == 7)
        {
            obj.gameObject.GetComponent<Health>().ApplyDamage(damage);
            obj.gameObject.GetComponent<EnemyIA>().ShotDown = true;
        }
        gameObject.SetActive(false);
    }

}
