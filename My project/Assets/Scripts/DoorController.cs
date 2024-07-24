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

    public void MoveElevator(int targetFloor, GameObject novo)
    {
        if (!isMoving)
        {
            StartCoroutine(MoveToFloor(targetFloor, novo));
        }
    }

    IEnumerator MoveToFloor(int targetFloor, GameObject novo)
    {
        isMoving = true;
        
        // Fechar a porta e a grade antes de mover
        doorAnimatorEsquerda.SetTrigger("Fechar");
        doorAnimatorDireita.SetTrigger("Fechar");
        yield return new WaitForSeconds(2); // Tempo da animação de fechar a porta e a grade

        // Simula a movimentação do elevador (ajuste conforme necessário)
        Debug.Log($"Movendo para o andar {targetFloor}");
        yield return new WaitForSeconds(2); // Tempo de movimentação do elevador

        // Abrir a porta e a grade ao chegar no andar
        doorAnimatorEsquerda.SetTrigger("Abrir");
        doorAnimatorDireita.SetTrigger("Abrir");
        yield return new WaitForSeconds(2); // Tempo da animação de abrir a porta e a grade

        // Notifique o MonsterManager sobre o andar que foi alcançado
        monsterManager.OnButtonPress(targetFloor, novo);

        isMoving = false;
    }
}
