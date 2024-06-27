using Unity.VisualScripting;
using UnityEngine;

public class ActivateUpgradeObject : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToActivate;

    public void Activate()
    {
        foreach(GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);
        }
    }

}
