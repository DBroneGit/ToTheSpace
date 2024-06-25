using UnityEngine;

[RequireComponent(typeof(Health))][RequireComponent(typeof(Rigidbody2D))]
public class EnemyIA : MonoBehaviour
{
    private Rigidbody2D rgbody;
    private Animator animator;
    private Health health;

    [SerializeField] private float walkVelocity = 4;
    [SerializeField] private int direction;

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
        transform.localEulerAngles = new Vector3(0,0,0);
        animator.enabled = true;
    }

    void Update()
    {
        if(health.Death) 
        {
            animator.enabled = false;

            Move(0);
            Stunned();
            return;
        }
        time += Time.deltaTime;

        if(time >= timeToChangeDirection) ChangeDirection();
        Move(direction);

        animator.SetFloat("Velocity", Mathf.Abs(rgbody.velocity.x));
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

    private void Stunned()
    {
        transform.localEulerAngles = new Vector3(0,0,180);
    }
}
