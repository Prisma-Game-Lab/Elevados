using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int CurrentFloor { get; private set; }
    public int TargetFloor { get; private set; }

    public int monster_type;
    public int weight;

    public void Initialize(int currentFloor, int targetFloor)
    {
        CurrentFloor = currentFloor;
        TargetFloor = targetFloor;
        Debug.Log($"Monstro {name} criado no andar {currentFloor} e deseja ir para o andar {targetFloor}");
    }

    public void MoveToTargetFloor()
    {
        if (CurrentFloor != TargetFloor)
        {
            StartCoroutine(MoveAndCheckArrival());
        }
    }

    private IEnumerator MoveAndCheckArrival()
    {
        // Simular o tempo de viagem do elevador para o andar desejado
        yield return new WaitForSeconds(2); 

        // Atualizar a posição do monstro para o andar desejado
        CurrentFloor = TargetFloor;

        if (CurrentFloor == TargetFloor)
        {
            gameObject.SetActive(false); // Desativar o monstro ao chegar no andar desejado
            Debug.Log($"Monstro {name} desativado ao chegar no andar {CurrentFloor}");
        }
    }
}
