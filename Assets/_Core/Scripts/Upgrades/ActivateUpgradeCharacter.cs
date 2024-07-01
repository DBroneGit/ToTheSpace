using UnityEngine;

public class ActivateUpgradeCharacter : MonoBehaviour
{
    [SerializeField] private Character character; public Character Character => character;

    public void ActivateAddForce(int cantity)
    {
        character.ThrowForce += cantity;
    }
}
