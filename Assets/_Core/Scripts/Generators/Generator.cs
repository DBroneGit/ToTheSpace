using UnityEngine;

public class Generator : MonoBehaviour
{

    [SerializeField] private GameObject[] objectsToGenerate;
    [SerializeField] private int[] rarity;
    [SerializeField] private int itemsGeneratedPerLoop = 1;


    [Header("Tiempos de Generación")]
    [SerializeField] private int cantityOfGeneratesOnStart = 1;
    [SerializeField] private float minimunTimeToGenerate = 3;
    [SerializeField] private float maximunTimeToGenerate = 7;

    [Header("Limites de generación")]
    [SerializeField] private float lefterGeneratorPoint = -10;
    [SerializeField] private float righterGeneratorPoint = 10;
    [SerializeField] private float upperGeneratorPoint = 7;
    [SerializeField] private float downerGeneratorPoint = 7;
    
    private int nextObjectToGenerate;
    private float nextTimeToGenerate = 0;
    private Vector2 nextPositionToGenerate;
    private float timer = 0;


    private void Start()
    {
        Generate(cantityOfGeneratesOnStart);
        SetNextTime();
    }

    void Update()
    {
        if(timer >= nextTimeToGenerate)
        {
            Generate(itemsGeneratedPerLoop);
            
            SetNextTime();
        }

        timer += Time.deltaTime;
    }
        
    /// <summary>Determina cuanto será el tiempo que se esperará para la generación del proximo objeto de manera aleatorea</summary>
    private void SetNextTime()
    {
        timer = 0;
        nextTimeToGenerate = Random.Range(minimunTimeToGenerate, maximunTimeToGenerate);
    }

    /// <summary>Determina cual será el objeto que se generará en la siguiente generación</summary>
    private void SetNextObject()
    {
        nextObjectToGenerate = Random.Range(0, objectsToGenerate.Length);
    }
    /// <summary>Determina cual será la posición donde se generará el objeto en la siguiente generación</summary>
    private void SetNextPosition()
    {
        float positionX = Random.Range(transform.position.x + lefterGeneratorPoint, transform.position.x + righterGeneratorPoint);
        float positionY = Random.Range(transform.position.y + downerGeneratorPoint, transform.position.y + upperGeneratorPoint);
        nextPositionToGenerate = new Vector2(positionX, positionY);
    }

    /// <summary>Genera una cantidad de objetos, son aleatoreos en posiciones aleatoreas </summary>
    /// <param name="cantity">cantidad de objetos a generar</param>
    public void Generate(int cantity)
    {
        for(int i = 0; i < cantity; i++)
        {
            SetNextObject();
            SetNextPosition();
            Generate();
        }
    }

    /// <summary>Genera un objeto en posiciones aleatoreas ya determinadas</summary>
    private void Generate()
    {
        //Si llegamos al limite de objetos, nos detenemos y no generamos otro
        if(EnemiesCounter.Instance.ItemsInGame >= EnemiesCounter.Instance.MaximunObjectsInGame) return;

        //Aumentamos los contadores
        EnemiesCounter.Instance.ItemsInGame++;

        //Obtenemos la caja
        GameObject item = PoolsManager.Instance.SearchPool(objectsToGenerate[nextObjectToGenerate]).GetObject();

        //Posicionamos la caja en la posición
        item.transform.position = nextPositionToGenerate;
        item.SetActive(true);
    }

    #region Editor

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        
        //Area de generación
        Gizmos.DrawLine(new Vector3(transform.position.x + lefterGeneratorPoint, transform.position.y + upperGeneratorPoint, 0),
                        new Vector3(transform.position.x + righterGeneratorPoint, transform.position.y + upperGeneratorPoint, 0));
        Gizmos.DrawLine(new Vector3(transform.position.x + righterGeneratorPoint, transform.position.y + upperGeneratorPoint, 0),
                        new Vector3(transform.position.x + righterGeneratorPoint, transform.position.y + downerGeneratorPoint, 0));
        Gizmos.DrawLine(new Vector3(transform.position.x + righterGeneratorPoint, transform.position.y + downerGeneratorPoint, 0),
                        new Vector3(transform.position.x + lefterGeneratorPoint, transform.position.y + downerGeneratorPoint, 0));
        Gizmos.DrawLine(new Vector3(transform.position.x + lefterGeneratorPoint, transform.position.y + downerGeneratorPoint, 0),
                        new Vector3(transform.position.x + lefterGeneratorPoint, transform.position.y + upperGeneratorPoint, 0));
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Centro del generador
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
    #endregion

}
