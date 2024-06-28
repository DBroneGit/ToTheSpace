using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : EnemyIA
{
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
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
    }
    
    void Update()
    {
        if(health.Empty) 
        {
            if(gameObject.layer != 9) TransformToPickable();
            return;
        }

        moveTime += Time.deltaTime;

        if(moveTime >= timeToChangeDirection) ChangeDirection();
        Move(direction);

        animator.SetFloat("Velocity", Mathf.Abs(rgbody.velocity.x));
        animator.SetBool("Flying", IsGrounded() == false);
    }
}
