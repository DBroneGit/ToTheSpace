using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolsManager : MonoBehaviour
{
    public static PoolsManager Instance;

    [SerializeField] private GameObject defaultPool;
    private List<Pool> pools; /// <summary>Lista de todas las piscinas del juego</summary>
    public List<Pool> Pools { get => pools; set => pools = value; }
    void Awake()
    {
        Instance = this;
        DetectPools();
    }

    private void DetectPools()
    {
        pools = GetComponentsInChildren<Pool>().ToList();
    }

    public Pool SearchPool(GameObject obj)
    {
        if(SearchPool(GetObjectName(obj)) == null) return CreatePool(obj);
        else return SearchPool(GetObjectName(obj));
    }

    public Pool SearchPool(String name)
    {
        for(int i = 0; i < pools.Count; i++)
        {
            if(name + "Pool" == pools[i].name) return pools[i];
        }
        return null;
    }
    private Pool CreatePool(GameObject obj)
    {
        GameObject newPool = Instantiate(defaultPool);
        newPool.transform.parent = transform;
        newPool.name = GetObjectName(obj) + "Pool";
        newPool.GetComponent<Pool>().Prefab = obj;
        pools.Add(newPool.GetComponent<Pool>());
        
        return newPool.GetComponent<Pool>();
    }

    private String GetObjectName(GameObject obj)
    {
        char[] separatorsChar = { ' ','(', ')' };
        string[] n = obj.name.Split(separatorsChar);
        return n[0];
    }

}
