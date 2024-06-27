
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BirdsCollector : MonoBehaviour
{
    [SerializeField] private List<GameObject> birdsList = new List<GameObject>(0); 
    public List<GameObject> BirdsList => birdsList;

    [SerializeField] private GameEvent callOvni;



    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 9)
        {  
            callOvni.Raise();   
            if(IsInTheList(other.gameObject) == false) birdsList.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == 9)
        {     
            birdsList.Remove(other.gameObject);
        }
    }


    public bool IsInTheList(GameObject obj)
    {
        foreach(GameObject bird in birdsList)
        {
            if(bird == obj)
            {
                return true;
            }
        }
        return false;
    }
}
