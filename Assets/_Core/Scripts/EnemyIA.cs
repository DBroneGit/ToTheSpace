using UnityEngine;

[RequireComponent(typeof(Health))][RequireComponent(typeof(Rigidbody2D))]
public class EnemyIA : MonoBehaviour
{
    private Rigidbody2D rgbody;
    private Animator animator;
    private Health health;

    [SerializeField] private float walkVelocity = 4;
    [SerializeField] private int direction;
    [SerializeField] private PhysicsMaterial2D baseMaterial;
    [SerializeField] private PhysicsMaterial2D pickableMaterial;
    

    private float time = 0;
    private float timeToChangeDirection = 0;

    void Awake()
    {
        rgbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
    }

    void OnEnable()
    {
        TransformToEnemy();
    }

    void Update()
    {
        if(health.Empty) 
        {
            if(gameObject.layer != 9) TransformToPickable();
            return;
        }

        time += Time.deltaTime;

        if(time >= timeToChangeDirection) ChangeDirection();
        Move(direction);

        animator.SetFloat("Velocity", Mathf.Abs(rgbody.velocity.x));
        bool flying = false; if(Mathf.Abs(rgbody.velocity.y) > 0.5f) flying = true;
        animator.SetBool("Flying", flying);
    }

    /// <summary>
    /// Mueve al enemigo
    /// </summary>
    /// <param name="dir"></param>
    private void Move(int dir)
    {
        rgbody.velocity = new Vector2(dir * walkVelocity, rgbody.velocity.y);
        if(dir != 0) transform.localScale = new Vector3(dir ,1 ,1);
    }

    /// <summary>
    /// Cambia la direccion de movimiento del enemigo
    /// </summary>
    private void ChangeDirection()
    {
        direction = Random.Range(-1, 2);
        
        //Resetamos el tiempo
        time = 0;
        timeToChangeDirection = Random.Range(1f, 2f);
    }


    private void TransformToPickable()
    {
        rgbody.freezeRotation = false;
        rgbody.gravityScale = 1;
        rgbody.sharedMaterial = pickableMaterial;
        transform.eulerAngles = new Vector3(0,0,180);
        gameObject.layer = 9;
        animator.enabled = false;
        Move(0);
    
    }

    private void TransformToEnemy()
    {
        rgbody.freezeRotation = true;
        rgbody.gravityScale = 0.2f;
        rgbody.sharedMaterial = baseMaterial;
        transform.rotation = new Quaternion(0,0,0,0);
        gameObject.layer = 7;
        animator.enabled = true;
        rgbody.simulated = true;
    }
}
