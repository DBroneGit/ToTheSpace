using Unity.VisualScripting;
using UnityEngine;

public class Ovni : MonoBehaviour
{
    private Rigidbody2D rgbody;

    [SerializeField] private GameObject ray;
    [SerializeField] private BirdsCollector collector;

    private Vector2 startPosition;
    private Vector2 objetivePosition;

    private float time = 0;
    private float comeOutTime = 20;
    private bool thereAreSomething;
    private bool finishAsk;
    void Awake()
    {
        rgbody = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        time = 0;
        comeOutTime = 20;
        finishAsk = false;
        transform.position = collector.transform.position + (Vector3.up * 8) + (Vector3.left * 15);
        SetPosition(collector.transform.position + (Vector3.up * 8));
    }

    private void Update()
    {
        //Contador
        time += Time.deltaTime;

        //Nos movemos a la posicion
        if(time <= 1f)
        {
            rgbody.MovePosition(Vector2.Lerp(startPosition, collector.transform.position + (Vector3.up * 8), time));
        }

        //Lanza el rayo si hay algo
        if(time >= 1 && time <= 2)
        {
            ray.SetActive(true);
            ray.transform.localScale = new Vector3(Mathf.Lerp(0, 1, time - 1),8.5f ,1 );

        }
        
        //Pregunta si hay algo
        if(time >= 2 && time < comeOutTime && finishAsk == false )
        {
            thereAreSomething = ray.GetComponent<Ray>().ThereAreSomething;

            if(thereAreSomething == false)
            {
                comeOutTime = time + 1;
                finishAsk = true;
            } 
        }
        //Cierra el laser
        if(time >= comeOutTime && time <= comeOutTime + 1)
        {
            ray.transform.localScale = new Vector3(Mathf.Lerp(1, 0, time - comeOutTime),8.5f ,1 );

            SetPosition(collector.transform.position + (Vector3.up * 8) + (Vector3.right * 15));
        }

        //Se va
        if(time >= (comeOutTime + 1) && time <= (comeOutTime + 2))
        {
            ray.SetActive(false);
            rgbody.MovePosition(Vector2.Lerp(startPosition, objetivePosition, time - comeOutTime - 1));
        }

        //Desaparece
        if(time >= comeOutTime + 2)
        {
            gameObject.SetActive(false);

            if(collector.BirdsList.Count > 0) Call();
        }
    }

    private void SetPosition(Vector2 pos)
    {
        startPosition = transform.position;
        objetivePosition = pos;
    }

    public void Call()
    {
        if(gameObject.activeSelf == false) gameObject.SetActive(true);
    }
    
}
