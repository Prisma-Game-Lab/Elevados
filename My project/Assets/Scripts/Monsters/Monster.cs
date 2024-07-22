using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Monster : MonoBehaviour
{
    public int currentFloor { get; private set; }
    public int targetFloor { get; private set; }

    public int monster_type;
    public int weight;
    private GameObject balão;

    public void Initiate(int currentFloor, int targetFloor)
    {
        this.currentFloor = currentFloor;
        this.targetFloor = targetFloor;
        balão = GameObject.Find("Balão");
        balão.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = targetFloor.ToString();
        Debug.Log($"Monstro criado no andar {currentFloor} e deseja ir para o andar {targetFloor}");
    }

    public void MoveToTargetFloor()
    {
        if (currentFloor != targetFloor)
        {
            StartCoroutine(MoveAndCheckArrival());
        }
    }

    private IEnumerator MoveAndCheckArrival()
    {
        // Simular o tempo de viagem do elevador para o andar desejado
        yield return new WaitForSeconds(2); 

        // Atualizar a posição do monstro para o andar desejado
        currentFloor = targetFloor;

        if (currentFloor == targetFloor)
        {
            gameObject.SetActive(false); // Desativar o monstro ao chegar no andar desejado
            Debug.Log($"Monstro desativado ao chegar no andar {currentFloor}");
        }
    }
}
