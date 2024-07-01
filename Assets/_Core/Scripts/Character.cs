using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    private Rigidbody2D rgbody;
    private PlayerInput input;
    private Animator animator;
    private Camera cam;

    [SerializeField] private GameObject hand;
    [SerializeField] private GameObject pickedObject; public GameObject PickedObject { get => pickedObject; set => pickedObject = value; }

    [SerializeField] private float velocity = 4f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float throwForce = 10f; public float ThrowForce { get => throwForce; set => throwForce = value; }  
    [SerializeField] private float AttackColdown = 0.5f;

    private bool watchingShop;

    private float attackTime = 0;

    void Awake()
    {
        
        rgbody = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        animator = GetComponentInChildren<Animator>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update()
    {
        if(watchingShop) return;
        //Coldowns
        attackTime += Time.deltaTime;

        Move(input.actions["Move"].ReadValue<Vector2>().x);
        if(input.actions["Jump"].WasPerformedThisFrame()) Jump();
        if(input.actions["Attack"].WasPerformedThisFrame()) Attack();
        if(input.actions["Interact"].WasPerformedThisFrame()) Interact();
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
        if(IsGrounded())
        {
            rgbody.velocity = Vector2.zero;
            rgbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void Attack()
    {
        //Si tiene algo en la mano, lo lanza
        if(pickedObject != null)
        {
            Throw();
            return;
        }

        if(attackTime <= AttackColdown) return;
        
        hand.transform.position = transform.position;
        hand.transform.rotation = Quaternion.Euler(0, 0, GetAngleTowardsMouse());
        hand.SetActive(true);

        attackTime = 0;
    }
    private void Interact()
    {
        Vector2 mouseWorldPosition = GetMouseWorldPosition();

        if(Physics2D.OverlapPoint(mouseWorldPosition) && Vector2.Distance(transform.position, mouseWorldPosition) <= 1.5f) //Si hay algo en donde damos clic...
        {
            Collider2D col = Physics2D.OverlapPoint(mouseWorldPosition); //Lo obtenemos

            //Si es Interactable...
            if(col.gameObject.layer == 9)
            {
                if(col.tag == "Enemy") PickUp(col.gameObject);
                else if(col.tag == "Object"){ InteractWithObject(col.gameObject);}
                
            }
            else //Si no, lo tiramos
            {
                Drop(); 
            }
        }
        else //Si no, lo tiramos
        {
            Drop(); 
        }
    }
    
    public void PickUp(GameObject obj)
    {
        if(pickedObject != null) return;

        obj.transform.parent = transform;
        obj.transform.position = transform.position + Vector3.up;
        obj.GetComponent<Rigidbody2D>().simulated = false;
        pickedObject = obj;
    }

    private void InteractWithObject(GameObject interactable)
    {
        interactable.GetComponent<InteractableObject>().Interact(this);
    }
    private void Throw()
    {
        float posX = Mathf.Cos(Mathf.Deg2Rad * GetAngleTowardsMouse());
        float posY = Mathf.Sin(Mathf.Deg2Rad * GetAngleTowardsMouse());

        pickedObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        pickedObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(posX * throwForce, posY * throwForce), ForceMode2D.Impulse);
        Drop();
    }

    public void Drop()
    {
        if(pickedObject == null) return;

        pickedObject.transform.parent = null;
        pickedObject.GetComponent<Rigidbody2D>().simulated = true;
        pickedObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        pickedObject = null;
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
        Vector3 mouseWorldPosition = GetMouseWorldPosition();

        Vector3 mouseDirection = mouseWorldPosition - transform.position;
        mouseDirection.z = 0;

        float angle = (Vector3.SignedAngle(Vector3.right, mouseDirection, Vector3.forward) + 360) % 360;

        return angle;
    }

    private Vector3 GetMouseWorldPosition()
    {
        return cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }
    #endregion

    #region EventsListener
    public void ShopWasOpen(){watchingShop = true; rgbody.velocity = Vector2.zero;}

    public void ShopWasClose(){watchingShop = false;}
    #endregion
}
