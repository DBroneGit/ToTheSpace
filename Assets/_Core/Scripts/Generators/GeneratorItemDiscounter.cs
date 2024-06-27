using UnityEngine;

public class GeneratorItemDiscounter : MonoBehaviour
{
    private bool created = false;


    void OnDisable()
    {
        if(created) EnemiesCounter.Instance.ItemsInGame--;
        created = true;
        
    }
}
