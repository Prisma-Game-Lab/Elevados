using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimatorEsquerda;
    public Animator doorAnimatorDireita;
    public MonsterManager monsterManager;

    private bool isMoving = false;

    void Start()
    {
        // Certifique-se de que a porta começa aberta
        doorAnimatorEsquerda.SetTrigger("Abrir 0");
        doorAnimatorDireita.SetTrigger("Abrir 0");
    }

    public void MoveElevator(int targetFloor)
    {
        if (!isMoving)
        {
            StartCoroutine(MoveToFloor(targetFloor));
        }
    }

    IEnumerator MoveToFloor(int targetFloor)
    {
        isMoving = true;
        
        // Fechar a porta antes de mover
        doorAnimatorEsquerda.SetTrigger("Fechar");
        doorAnimatorDireita.SetTrigger("Fechar");
        yield return new WaitForSeconds(2); // Tempo da animação de fechar a porta

        // Simula a movimentação do elevador (ajuste conforme necessário)
        Debug.Log($"Movendo para o andar {targetFloor}");
        yield return new WaitForSeconds(2); // Tempo de movimentação do elevador

        // Abrir a porta ao chegar no andar
        doorAnimatorEsquerda.SetTrigger("Abrir");
        doorAnimatorDireita.SetTrigger("Abrir");
        yield return new WaitForSeconds(2); // Tempo da animação de abrir a porta

        // Notifique o MonsterManager sobre o andar que foi alcançado
        monsterManager.OnButtonPress(targetFloor);

        isMoving = false;
    }
}
