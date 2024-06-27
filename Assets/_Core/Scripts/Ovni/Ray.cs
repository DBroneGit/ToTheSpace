using UnityEngine;

public class Ray : MonoBehaviour
{
    [SerializeField] private bool thereAreSomething; public bool ThereAreSomething => thereAreSomething;

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.layer == 7 || other.gameObject.layer == 8 || other.gameObject.layer == 9)
        {
            thereAreSomething = true;
        }
        else
        {
            thereAreSomething = false;    
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
         if(other.gameObject.layer == 7 || other.gameObject.layer == 9)
        {
            thereAreSomething = false;    
        }   
    }
}
