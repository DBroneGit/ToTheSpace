using UnityEngine;

public class GeneratorManager : MonoBehaviour
{
    [SerializeField] private GameObject[] listOfGenerators;
    [SerializeField] private int[] altitudeRequerided;
    void Awake()
    {
        UpdateActivatedsGenerators();
    }
    public void UpdateActivatedsGenerators()
    {
        for(int i = 0; i < listOfGenerators.Length; i++)
        {
            if(Ship.Altitude >= altitudeRequerided[i]){listOfGenerators[i].SetActive(true);}
            else{listOfGenerators[i].SetActive(false);}
        }
    }
    //[SerializeField] private GameObject[] specialGenerator;

}
