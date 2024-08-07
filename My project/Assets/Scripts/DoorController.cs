using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimatorEsquerda;
    public Animator doorAnimatorDireita;
    public MonsterManager monsterManager;

    public GameObject[] floorBackgrounds; // Array de GameObjects que representam os fundos dos andares
    private int currentFloor = 1; // Controle do andar atual
    private bool isMoving = false;

    void Start()
    {
        // Certifique-se de que a porta começa aberta e o fundo do andar inicial está ativo
        doorAnimatorEsquerda.SetTrigger("Abrir 0");
        doorAnimatorDireita.SetTrigger("Abrir 0");
        UpdateFloorBackground(1);
    }

    public void MoveElevator(int targetFloor, GameObject novo)
    {
        if (!isMoving)
        {
            StartCoroutine(MoveToFloor(targetFloor, novo));
        }
    }

    IEnumerator MoveToFloor(int targetFloor, GameObject novo)
    {
        if (targetFloor < 0 || targetFloor > monsterManager.maxFloor)
        {
            Debug.LogError($"Tentativa de mover para andar inválido: {targetFloor}");
            yield break;
        }
        isMoving = true;

        // Fechar a porta antes de mover
        doorAnimatorEsquerda.SetTrigger("Fechar");
        doorAnimatorDireita.SetTrigger("Fechar");
        AudioManager.instance.PlayDoorSFX(); // Toca o som da porta fechando
        yield return new WaitForSeconds(3); // Tempo da animação de fechar a porta

        // Simula a movimentação do elevador
        Debug.Log($"Movendo para o andar {targetFloor}");
        yield return new WaitForSeconds(3); // Tempo de movimentação do elevador

        // Atualize o andar atual
        currentFloor = targetFloor;

        // Atualize o fundo do andar ao abrir a porta
        UpdateFloorBackground(currentFloor);

        // Abrir a porta ao chegar no andar
        doorAnimatorEsquerda.SetTrigger("Abrir");
        doorAnimatorDireita.SetTrigger("Abrir");
        AudioManager.instance.PlayDoorSFX(); // Toca o som da porta abrindo
        yield return new WaitForSeconds(2);
        // Notifique o MonsterManager sobre o andar que foi alcançado
        monsterManager.OnElevatorArrived(currentFloor);

        isMoving = false;
    }

    private void UpdateFloorBackground(int targetFloor)
    {
        // Desative todos os fundos
        foreach (var background in floorBackgrounds)
        {
            background.SetActive(false);
        }

        // Ative o fundo do andar alvo
        if (targetFloor > 0 && targetFloor <= floorBackgrounds.Length)
        {
            floorBackgrounds[targetFloor - 1].SetActive(true);
        }
    }
}
