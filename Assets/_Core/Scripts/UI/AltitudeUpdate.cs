using TMPro;
using UnityEngine;

public class AltitudeUpdate : MonoBehaviour
{
    private TMP_Text TXT_cantity;
    void Awake()
    {
        TXT_cantity = GetComponent<TMP_Text>();
    }
    // Update is called once per frame
    void Update()
    {
        TXT_cantity.text = (int) Ship.Instance.transform.position.y + " Mts";
    }
}
