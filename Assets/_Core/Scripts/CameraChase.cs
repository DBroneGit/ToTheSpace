using UnityEngine;

public class CameraChase : MonoBehaviour
{
    [SerializeField] private GameObject objectToChase;
    private AudioSource SFX;
    [SerializeField] private AudioClip music1;
    [SerializeField] private AudioClip music2;

    void Awake()
    {
        SFX = GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.position = new Vector3(0, objectToChase.transform.position.y + 6f, -10);
    
        
        if(Ship.Instance.transform.position.y <= 50)
        {
            if(SFX.clip == music2)
            {
                SFX.clip = music1;
                SFX.Play();
            }
        } 
        else
        {
            if(SFX.clip == music1)
            {
                SFX.clip = music2;
                SFX.Play();
            }
        }
    }
}
