using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    private Rigidbody2D rgbody;
    private PlayerInput input;
    private Animator animator;
    private Camera cam;

    [SerializeField] private GameObject hand;

    [SerializeField] private float velocity = 4f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float AttackColdown = 0.5f;

    private float attackTime = 0;

    void Awake()
    {
        rgbody = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update()
    {
        //Coldowns
        attackTime += Time.deltaTime;

        Move(input.actions["Move"].ReadValue<Vector2>().x);
        if(input.actions["Jump"].WasPerformedThisFrame()) Jump();
        if(input.actions["Attack"].WasPerformedThisFrame()) Attack();
    }

    void LateUpdate()
    {
        animator.SetFloat("Velocity", Mathf.Abs(rgbody.velocity.x));
    }

    /// <summary>
    /// Mueve al personaje
    /// </summary>
    /// <param name="dir">Direccion a mover (-1 o 1)</param>
    private void Move(float dir)
    {
        rgbody.velocity = new Vector2(dir * velocity, rgbody.velocity.y);
        if(dir != 0) transform.localScale = new Vector3(dir ,1 ,1);
    }

    /// <summary>
    /// Hace saltar al personaje
    /// </summary>
    private void Jump()
    {
        if(IsGrounded()) rgbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    private void Attack()
    {
        if(attackTime <= AttackColdown) return;
        
        hand.transform.position = transform.position;
        hand.transform.rotation = Quaternion.Euler(0, 0, GetAngleTowardsMouse());
        hand.SetActive(true);


        attackTime = 0;
    }
    
    /// <summary>
    /// Detecta si esta tocando solidos
    /// </summary>
    /// <returns>Retorne un booleano que dice si esta tocando el suelo</returns>
    private bool IsGrounded()
    {
        Vector2 pointA = new Vector2(transform.position.x - 0.24f, transform.position.y - 0.51f);
        Vector2 pointB = new Vector2(transform.position.x + 0.24f, transform.position.y - 0.49f);

        return Physics2D.OverlapArea(pointA, pointB, 1<<3);
    }

    #region Utilidades
    private float GetAngleTowardsMouse()
    {
        Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        Vector3 mouseDirection = mouseWorldPosition - transform.position;
        mouseDirection.z = 0;

        float angle = (Vector3.SignedAngle(Vector3.right, mouseDirection, Vector3.forward) + 360) % 360;

        return angle;
    }
    #endregion
}
