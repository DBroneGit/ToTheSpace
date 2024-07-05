    using System;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private GeneratorManager generatorManager;
    public static Ship Instance;

    private Rigidbody2D rgbody;
    private static int altitude = 0; public static int Altitude { get => Altitude = altitude; set => altitude = value; }
    private int cantityOfFlyingObjects = 0; public int CantityOfFlyingObject { get => cantityOfFlyingObjects; set => cantityOfFlyingObjects = value; }
    
    [SerializeField] private float flyingSpeed = 4;
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
        UpdatePosition();

        time += Time.deltaTime;

       
        if(altitude > (int) transform.position.y) rgbody.velocity = new Vector2(0, flyingSpeed);
        else if(altitude < (int) transform.position.y) rgbody.velocity = new Vector2(0, -flyingSpeed);
        else rgbody.velocity = Vector2.zero;
    }


    private void UpdatePosition()
    {
        if(cantityOfFlyingObjects >= 2) altitude = (cantityOfFlyingObjects * 10) - 20 + Money.Cantity;
        if(altitude > 100) altitude = 100;  
        time = 0;

        generatorManager.UpdateActivatedsGenerators();
    }

    public void AddRemoveFlyingObject(int cantity)
    {
        cantityOfFlyingObjects += cantity;
    }
}
