using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int currentFloor { get; private set; }
    public int targetFloor { get; private set; }
    private MonsterManager manager;

    public void Initiate(int currentFloor, int targetFloor, MonsterManager manager)
    {
        this.currentFloor = currentFloor;
        this.targetFloor = targetFloor;
        this.manager = manager;
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
        yield return new WaitForSeconds(2); 

        currentFloor = targetFloor;

        if (currentFloor == targetFloor)
        {
            Debug.Log($"Monstro desativado ao chegar no andar {currentFloor}");
            manager.RemoveMonsterFromElevator(gameObject, currentFloor);
            Destroy(gameObject);
        }
    }
}
