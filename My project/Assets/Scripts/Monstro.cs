using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Monstro : MonoBehaviour
{
    public GameObject[] monsters;
    public int maxFloor = 8;

    private int currentMonsterIndex = 0;
    private bool isProcessing = false;
    private int requestedFloor;

    void Start()
    {
        foreach (GameObject monster in monsters)
        {
            monster.SetActive(false);
        }

        StartCoroutine(MonsterLoop());
    }

    IEnumerator MonsterLoop()
    {
        while (true)
        {
            if (!isProcessing)
            {
                isProcessing = true;

                if (currentMonsterIndex >= monsters.Length)
                {
                    currentMonsterIndex = 0;
                }

                GameObject currentMonster = monsters[currentMonsterIndex];
                currentMonster.SetActive(true);

                requestedFloor = Random.Range(1, maxFloor + 1);
                Debug.Log("Monstro " + currentMonster.name + " quer ir para o andar " + requestedFloor);

                while (isProcessing)
                {
                    yield return null;
                }

                currentMonsterIndex = (currentMonsterIndex + 1) % monsters.Length;
            }

            yield return null;
        }
    }

    public void OnButtonPress(int floor)
    {
         if (currentMonsterIndex < monsters.Length)
        {
            GameObject currentMonster = monsters[currentMonsterIndex];
            currentMonster.SetActive(false); // Desativa o monstro atual
            currentMonsterIndex++;

            if (currentMonsterIndex >= monsters.Length)
            {
                currentMonsterIndex = 0; // Reseta o índice se todos os monstros já foram ativados
            }

            GameObject nextMonster = monsters[currentMonsterIndex];
            StartCoroutine(ActivateNextMonster(nextMonster, 2f)); // Ativa o próximo monstro após 2 segundos
        }
    
        if (isProcessing && floor == requestedFloor)
        {
            GameObject currentMonster = monsters[currentMonsterIndex];
            currentMonster.SetActive(false);
            isProcessing = false;
            Debug.Log("Monstro " + currentMonster.name + " saiu no andar " + floor);
        }
    }

    private IEnumerator ActivateNextMonster(GameObject monster, float delay)
    {
        yield return new WaitForSeconds(delay);
        monster.SetActive(true); // Ativa o próximo monstro
    }
}
