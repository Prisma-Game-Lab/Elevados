using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterManager : MonoBehaviour
{
    //Monitora o estado do elevador: 0 se tds os botoes estiverem em posicao neutra
    //1 se algum botao estiver apertado
    private int buttonsReleased;

    public List<GameObject> monsters; // Lista de monstros na cena
    public int maxFloor = 8;

    [SerializeField]
    private Level[] levels;

    private int current_level;

    void Start()
    {
        buttonsReleased = 0;
        current_level = 0;

        InitializeMonsters();
    }

    void InitializeMonsters()
    {
        foreach (GameObject monsterObject in monsters)
        {
            Monster monster = monsterObject.GetComponent<Monster>();
            int spawnFloor = Random.Range(1, maxFloor); // Andar de origem aleatório
            int targetFloor = Random.Range(1, maxFloor); // Andar de destino aleatório

            // Garantir que o andar de destino é diferente do andar de origem
            while (targetFloor == spawnFloor)
            {
                targetFloor = Random.Range(1, maxFloor);
            }

            monster.Initialize(spawnFloor, targetFloor);
            monsterObject.SetActive(false); // Começar com todos os monstros desativados
        }
    }

    public void OnButtonPress(int floor)
    {
        // Ativar monstros no andar pressionado
        ActivateMonstersOnFloor(floor);

        // Verificar se algum monstro deseja ir para o andar pressionado
        foreach (GameObject monsterObject in monsters)
        {
            Monster monster = monsterObject.GetComponent<Monster>();
            if (monster.CurrentFloor == floor && !monsterObject.activeSelf)
            {
                Debug.Log($"Monstro {monster.name} está no andar {floor} e deseja ir para o andar {monster.TargetFloor}");
                // monsterObject.SetActive(true); // Ativar o monstro
                StartCoroutine(MoveMonster(monster));
            }
            else if (monster.TargetFloor == floor && monsterObject.activeSelf)
            {
                // Desativar monstro se o andar pressionado for o destino desejado
                Debug.Log($"Monstro {monster.name} desativado ao chegar no andar {floor}");
                monsterObject.SetActive(false);
            }
        }
    }

    // IEnumerator MonsterLoop()
    // {
    //     while (true)
    //     {
    //         if (!isProcessing)
    //         {
    //             isProcessing = true;

    //             if (currentMonsterIndex >= monsters.Length)
    //             {
    //                 currentMonsterIndex = 0;
    //             }

    //             GameObject currentMonster = monsters[currentMonsterIndex];
    //             currentMonster.SetActive(true);

    //             requestedFloor = Random.Range(1, maxFloor + 1);
    //             Debug.Log("Monstro " + currentMonster.name + " quer ir para o andar " + requestedFloor);

    //             while (isProcessing)
    //             {
    //                 yield return null;
    //             }

    //             currentMonsterIndex = (currentMonsterIndex + 1) % monsters.Length;
    //         }

    //         yield return null;
    //     }
    // }

    // public void OnButtonPress(int floor)
    // {
    //     if (currentMonsterIndex < monsters.Length)
    //     {
    //         GameObject currentMonster = monsters[currentMonsterIndex];
    //         currentMonster.SetActive(false); // Desativa o monstro atual
    //         currentMonsterIndex++;

    //         if (currentMonsterIndex >= monsters.Length)
    //         {
    //             currentMonsterIndex = 0; // Reseta o índice se todos os monstros já foram ativados
    //         }

    //         GameObject nextMonster = monsters[currentMonsterIndex];
    //         StartCoroutine(ActivateNextMonster(nextMonster, 2f)); // Ativa o próximo monstro após 2 segundos
    //     }
    
    //     if (isProcessing && floor == requestedFloor)
    //     {
    //         GameObject currentMonster = monsters[currentMonsterIndex];
    //         currentMonster.SetActive(false);
    //         isProcessing = false;
    //         Debug.Log("Monstro " + currentMonster.name + " saiu no andar " + floor);
    //     }
    // }

    IEnumerator MoveMonster(Monster monster)
    {
        // Somente mover o monstro se ele estiver ativo e se o andar atual for diferente do andar desejado
        if (monster.gameObject.activeSelf && monster.CurrentFloor != monster.TargetFloor)
        {
            yield return new WaitForSeconds(2); // Simula o tempo de espera para o monstro entrar no elevador
            monster.MoveToTargetFloor(); // Iniciar a movimentação do monstro
            Debug.Log($"Monstro {monster.name} está se movendo para o andar {monster.TargetFloor}");
        }
    }

    public void ActivateMonstersOnFloor(int floor)
    {
        // Ativar monstros no andar especificado
        foreach (GameObject monsterObject in monsters)
        {
            Monster monster = monsterObject.GetComponent<Monster>();
            if (monster.CurrentFloor == floor)
            {
                monsterObject.SetActive(true);
                Debug.Log($"Monstro {monster.name} ativado no andar {floor}");
            }
        }
    }

    public int hold()
    {
        //Retorna 1 se for possivel apertar o botao
        if (buttonsReleased == 0)
        {
            buttonsReleased = 1;
            return 1;
        }

        //Retorno 0 caso contrario
        return 0;
    }

    public void release()
    {
        buttonsReleased = 0;
    }
}
