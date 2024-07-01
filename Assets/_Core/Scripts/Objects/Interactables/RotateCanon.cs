

using UnityEngine;

public class RotateCanon : InteractableObject
{
    [SerializeField] private Transform canon;

    public override void Interact(Character character)
    {
        Rotate();
    }

    private void Rotate()
    {
        int extraRotation = 45;

        canon.localEulerAngles = new Vector3(0,0, canon.localEulerAngles.z + extraRotation);
        if(canon.localEulerAngles.z > 90 && canon.localEulerAngles.z < 270) canon.localEulerAngles = new Vector3(0,0, -90);
    }
    
}
