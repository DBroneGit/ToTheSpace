using UnityEngine;

[RequireComponent(typeof(Health))][RequireComponent(typeof(Rigidbody2D))] [RequireComponent(typeof(Enemy))]
public abstract class EnemyIA : MonoBehaviour
{
    protected Rigidbody2D rgbody;
    protected Animator animator;
    protected Health health;

    [SerializeField] protected float walkVelocity = 4;
    [SerializeField] protected int direction;

    [SerializeField] protected PhysicsMaterial2D baseMaterial;
    [SerializeField] protected PhysicsMaterial2D pickableMaterial;
    
    protected float moveTime = 0;
    protected float timeToChangeDirection = 0;
    protected float shotDownTime = 0;
    protected bool shotDown; public bool ShotDown { get => shotDown; set => shotDown = value; } 

    protected virtual void Awake()
    {
        rgbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
    }

    protected virtual void OnEnable()
    {
        TransformToEnemy();
    }

    protected virtual void OnDisable()
    {
        transform.rotation = new Quaternion(0,0,0,0);
    }
    
    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == 7)
        {
            other.gameObject.GetComponent<Health>().ApplyDamage(1);
            other.gameObject.GetComponent<EnemyIA>().ShotDown = true;
        }

    }
    /// <summary>
    /// Mueve al enemigo
    /// </summary>
    /// <param name="dir"></param>
    protected void Move(int dir)
    {
        rgbody.velocity = new Vector2(dir * walkVelocity, rgbody.velocity.y);
        if(dir != 0) transform.localScale = new Vector3(dir ,1 ,1);
    }

    /// <summary>
    /// Cambia la direccion de movimiento del enemigo
    /// </summary>
    protected void ChangeDirection()
    {
        direction = Random.Range(-1, 2);
        
        //Resetamos el tiempo
        moveTime = 0;
        timeToChangeDirection = Random.Range(1f, 2f);
    }


    protected void TransformToPickable()
    {
        rgbody.freezeRotation = false;
        rgbody.gravityScale = 1;
        rgbody.sharedMaterial = pickableMaterial;
        rgbody.velocity = new Vector2(0, rgbody.velocity.y);

        transform.eulerAngles = new Vector3(0,0,180);
        gameObject.layer = 9;
        animator.enabled = false;
    }

    protected void TransformToEnemy()
    {
        rgbody.freezeRotation = true;
        rgbody.gravityScale = 0.2f;
        rgbody.sharedMaterial = baseMaterial;
        rgbody.velocity = Vector2.zero;
        transform.rotation = new Quaternion(0,0,0,0);
        gameObject.layer = 7;
        animator.enabled = true;
        rgbody.simulated = true;
    }
    
    protected bool IsGrounded()
    {
        Vector2 pointA = new Vector2(transform.position.x - 0.24f, transform.position.y - 0.51f);
        Vector2 pointB = new Vector2(transform.position.x + 0.24f, transform.position.y - 0.49f);

        return Physics2D.OverlapArea(pointA, pointB, 3);
    }

}
