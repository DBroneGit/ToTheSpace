using System.Collections.Generic;
using UnityEngine;

/// <summary>"Piscina" de objetos (Para optimizar el juego)</summary>
public class Pool : MonoBehaviour
{
    
    /// <summary>Objeto prefabicado a administrar</summary>
    [SerializeField] private GameObject prefab; public GameObject Prefab { get => prefab; set => prefab = value; } 
    /// <summary>Lista de los objetos creados a partir del objeto prefabricado</summary>
    [SerializeField] private List<GameObject> objectsInPool = new List<GameObject>();

    /// <summary>Crea un objeto del prefabricado</summary>
    /// <returns>Objeto creado</returns>
    private GameObject CreateObject()
    {
        GameObject obj = Instantiate(prefab); //Crea un objeto
        obj.transform.SetParent(transform); //Lo hacemos hijo de la piscina de objetos
        obj.SetActive(false); //Lo desactivamos
        objectsInPool.Add(obj); //Lo añadimos a la piscina de objetos

        return obj;
    }

    /// <summary>Pide un objeto y lo quita de la lista</summary>
    /// <returns>Gameobject pedido</returns>
    public GameObject GetObject()
    {
        for (int i = 0; i < objectsInPool.Count; i++)
        {
            //Si hay un objeto desactivado y que no este tomado, lo activamos
            if (objectsInPool[i].activeInHierarchy == false && objectsInPool[i].transform.parent == transform) return objectsInPool[i];
        }
        //Si estan ocupados, creamos 1  
        GameObject obj = CreateObject();
        return obj;
    }
    /// <summary>Regresa un objeto al Pool</summary>
    /// <param name="obj">Objeto a regresar</param>
    public void Return(GameObject obj)
    {
        //Valores a por defecto
        obj.transform.parent = transform;
        if(obj.GetComponent<Rigidbody2D>() != null) obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero; 
        
        obj.SetActive(false);
    }
}