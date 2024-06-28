using UnityEngine;

[RequireComponent(typeof(EnemyIA))] [RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int price = 1; public int Price { get => price; set => price = value; }
    [SerializeField] private bool isOviparous; public bool IsOviparous => isOviparous;
    [SerializeField] private GameObject egg; public GameObject Egg { get => egg; set => egg = value; }
    [SerializeField] private float timePerCicle = 10; public float TimePerCicle => timePerCicle;
    [SerializeField] private int eggsPerCicle = 1; public int EggsPerCicle => eggsPerCicle;

}
