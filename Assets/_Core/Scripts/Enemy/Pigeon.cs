using UnityEngine;

public class Pigeon : EnemyIA
{
    [SerializeField] protected int flyDirection;
    [SerializeField] protected float flyVelocity = 4;

    protected bool canFly;
    
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        
        if(transform.position.x < 0) flyDirection = 1;
        else flyDirection = -1;
        
        canFly = true;
        rgbody.gravityScale = 0;
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);

        shotDown = true;
    }
    
    protected override void Update()
    {
        base.Update();

        if(health.Empty) //No tiene vida
        {
            if(gameObject.layer != 9) TransformToPickable();
            return;
        }

        if(shotDown == true)
        {
            shotDownTime += Time.deltaTime;
            rgbody.gravityScale = 1;
            animalSound.Stop();

            if(shotDownTime >= 3){
                shotDownTime = 0;
                shotDown = false;

                rgbody.AddForce(new Vector2(flyVelocity * flyDirection, flyVelocity), ForceMode2D.Impulse);
            }

            if(IsGrounded)
            {
                moveTime += Time.deltaTime;

                if(moveTime >= timeToChangeDirection) ChangeDirection();
                Move(direction);
            }
        }

        if(shotDown == false)
        {
            if(IsGrounded)
            {
                animalSound.Stop();
            }
            else
            {
                rgbody.gravityScale = 0;
                Fly(flyDirection);
                if(animalSound.isPlaying == false) animalSound.Play();
            }
        }
       
        animator.SetFloat("Velocity", Mathf.Abs(rgbody.velocity.x));
        animator.SetBool("Flying", IsGrounded == false);
    }

    /// <summary>
    /// Mueve al enemigo
    /// </summary>
    /// <param name="dir"></param>

    private void Fly(int dir)
    {
        rgbody.velocity = new Vector2(dir * flyVelocity, rgbody.velocity.y);
        if(dir != 0) transform.localScale = new Vector3(dir ,1 ,1);
    }

    public void Shotdown()
    {
        shotDownTime = 0;
        shotDown = true;
    }

}
