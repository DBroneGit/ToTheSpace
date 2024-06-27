using UnityEngine;

public class EnemiesCounter : MonoBehaviour
{
    public static EnemiesCounter Instance;

    [SerializeField] private int itemsInGame = 0; public int ItemsInGame { get => itemsInGame; set => itemsInGame = value; }
    [SerializeField] private int maximunObjectsInGame = 4; public int MaximunObjectsInGame => maximunObjectsInGame;
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update

}
