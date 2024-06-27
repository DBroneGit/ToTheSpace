using UnityEngine;

public class ChickenHouse : InteractableObject
{

    [SerializeField] private Transform birdPosition;
    [SerializeField] private Transform eggPosition;

    private GameObject bird;
    private GameObject egg;
    private float timePerCicle = 0;
    private int eggsPerCicle = 0;
    private float time = 0;

    private void Update()
    {
        if(egg != null) //Si hay huevos para generar
        {
            time += Time.deltaTime;
            
            if(time >= timePerCicle)
            {
                GenerateEgg();
                time = 0;
            }
        }
    }

    private void GenerateEgg()
    {
        GameObject eggGenerated = PoolsManager.Instance.SearchPool(egg).GetObject();
        eggGenerated.transform.position = eggPosition.position;
        eggGenerated.transform.parent = eggPosition;
        eggGenerated.GetComponent<Rigidbody2D>().gravityScale = 0;

        eggGenerated.SetActive(true);
    }

    public override void Interact(Character character)
    {
        if(requireEnemy == false) //No requiere enemigo
        {
            return;
        } 
        else //Requiere enemigo
        {
            if(character.PickedObject == null) //Si no tiene enemigo en mano
            {
                if(bird != null) //Si hay pajaro en la casa
                {
                    GetBirdFromTheHouse(character); //Hay un pajaro
                }
            } 
            else //Si tiene enemigo en mano
            {
                Enemy enemy = character.PickedObject.GetComponent<Enemy>(); 

                if(enemy.IsOviparous == false) return; //Requiere que sea oviparo
                PutBirdOnTheHouse(enemy);   
                character.Drop();

                enemy.transform.parent = birdPosition;
                enemy.GetComponent<Rigidbody2D>().simulated = false;
            }
        }

    }

    private void PutBirdOnTheHouse(Enemy enemy)
    {
        enemy.transform.parent = birdPosition;
        enemy.transform.position = birdPosition.position;
        enemy.transform.rotation = birdPosition.rotation;
        bird = enemy.gameObject;
        
        //Activamos la generadora de huevos
        egg = enemy.Egg;
        timePerCicle = enemy.TimePerCicle;
        eggsPerCicle = enemy.EggsPerCicle;
    }

    private void GetBirdFromTheHouse(Character player)
    {
        player.PickUp(bird);
        egg = null;
        time = 0;
    }
}
