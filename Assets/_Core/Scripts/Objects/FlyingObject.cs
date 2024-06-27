using UnityEngine;

public class FlyingObject : MonoBehaviour
{
    [SerializeField] private int flyingPower = 1;
    void OnEnable()
    {
        Ship.Instance.AddRemoveFlyingObject(flyingPower);
    }

    void OnDisable()
    {
        Ship.Instance.AddRemoveFlyingObject(-flyingPower);
    }
}
