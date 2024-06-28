using System;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private GeneratorManager generatorManager;
    public static Ship Instance;

    private Rigidbody2D rgbody;
    private static int altitude = 0; public static int Altitude { get => Altitude = altitude; set => altitude = value; }
    private int cantityOfFlyingObjects; public int CantityOfFlyingObject { get => cantityOfFlyingObjects; set => cantityOfFlyingObjects = value; }
    
    private Vector2 startPosition;
    private Vector2 objetivePosition;
    private float flyingSpeed = 5;
    private float time;
    void Awake()
    {
        Instance = this;
        rgbody = GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {
        UpdatePosition();
    }

    void Update()
    {
        time += Time.deltaTime;

        rgbody.MovePosition(Vector2.Lerp(startPosition, objetivePosition,
                            time/(Math.Abs(objetivePosition.y - startPosition.y) / flyingSpeed)));
    }

    private void UpdatePosition()
    {
        startPosition = transform.position;
        objetivePosition = new Vector2(0, altitude);
        time = 0;

        generatorManager.UpdateActivatedsGenerators();
    }

    public void AddRemoveFlyingObject(int cantity)
    {
        cantityOfFlyingObjects += cantity;
        Altitude = cantityOfFlyingObjects * 5;
        UpdatePosition();
    }
}
