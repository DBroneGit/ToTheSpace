
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    [SerializeField] private GameObject panelIntro;
    [SerializeField] private GameObject panelInstructions;

    private float time = 0;
    
    void Update()
    {
        time += Time.deltaTime;

        //Entrada Panel Intro
        if(time > 0 && time <= 3f) 
        {
            panelIntro.SetActive(true); 
        }
        //Entrada de panel Instructions
        if(time >= 3f)
        {
            panelIntro.SetActive(false);
            panelInstructions.SetActive(true);
            if(Input.anyKeyDown) SceneManager.LoadScene("GameScene");
        }
    }

    
}
