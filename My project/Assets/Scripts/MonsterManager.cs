using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using System.Linq;

public class MonsterManager : MonoBehaviour
{
    //Monitora o estado do elevador: 0 se tds os botoes estiverem em posicao neutra
    //1 se algum botao estiver apertado
    private int buttonsReleased;

    [SerializeField] private GameObject andarAtual;

    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private GameObject balão;
    [SerializeField] private Level[] levels;

    private List<GameObject> elevator; // Lista de monstros na cena
    [SerializeField] private int maxFloor = 6;

    private List<GameObject>[] floors;
    private int current_floor;

    private int current_level;

    void Start()
    {
        buttonsReleased = 0;
        current_level = 0;

        floors = new List<GameObject>[maxFloor];
        current_floor = 0;

        elevator = new List<GameObject>();

        for(int i = 0; i < maxFloor; i++)
        {
            floors[i] = new List<GameObject>();
        }

        InitializeMonsters();
    }

    void InitializeMonsters()
    {
        for (int i = 0; i < levels[current_level].qtd_monsters; i++)
        {
            GameObject monster = Instantiate(monsterPrefab, new Vector3(-5f, 0, 0), Quaternion.identity, transform);

            int current_floor = Random.Range(1, maxFloor);
            int targetFloor = Random.Range(1, maxFloor);

            while (targetFloor == current_floor)
            {
                targetFloor = Random.Range(1, maxFloor);
            }

            monster.GetComponent<Monster>().Initiate(current_floor, targetFloor);

            floors[current_floor].Add(monster);

            monster.SetActive(false);

        }
    }

    public void OnButtonPress(int floor, GameObject novo)
    {
        //andarAtual.SetActive(false);
        novo.SetActive(true);
        andarAtual = novo;
        current_floor = floor;
        //TO DO: MUDAR TAMBEM O SPRITE DO ANDAR

        // Ativar monstros no andar pressionado
        ActivateMonstersOnFloor(floor);

        // Verificar se algum monstro deseja ir para o andar pressionado
        foreach (GameObject monsterObject in elevator)
        {
            Monster monster = monsterObject.GetComponent<Monster>();

            if(monster.targetFloor == current_floor)
            {
                Debug.Log($"Monstro {monster.name} desativado ao chegar no andar {floor}");
                
                elevator.Remove(monsterObject);
                Destroy(monsterObject);
            }
        }
        // Adicionar pontos baseados no peso do monstro (POSSIVEL PARTE DO SCRIPT para
        // FAZER FUNCIONAR A PONTUAÇAO DO JOGO, CHECAR ENTRE OS PROGS)
        // MonsterWeight weight = monster.GetComponent<MonsterWeight>();
        // if (weight != null)
        // {
        //     int points = weight.GetWeightPoints();
        //     ScoreManager.instance.AddScore(points);
        // }
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

    public IEnumerator MoveMonster(Monster monster)
    {
        // Somente mover o monstro se ele estiver ativo e se o andar atual for diferente do andar desejado
        if (monster.gameObject.activeSelf && monster.currentFloor != monster.targetFloor)
        {
            yield return new WaitForSeconds(2); // Simula o tempo de espera para o monstro entrar no elevador
            monster.MoveToTargetFloor(); // Iniciar a movimentação do monstro
            Debug.Log($"Monstro {monster.name} está se movendo para o andar {monster.targetFloor}");
        }
    }

    public void ActivateMonstersOnFloor(int floor)
    {
        // Ativar monstros no andar especificado
        foreach (GameObject monsterObject in floors[current_floor])
        {
            monsterObject.SetActive(true);
            Debug.Log($"Monstro ativado no andar {floor}");
            balão.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = monsterObject.GetComponent<Monster>().targetFloor.ToString();
            balão.SetActive(true);

            //TO DO: LEVAR EM CONTA PESO DO MONSTRO E VER QUAL MONSTRO NA FILA PODE ENTRAR
            //O PRIMEIRO E SEMPRE O QUE ENTRA E SE ELE PASSAR O PESO O ELEVADOR FICA PESADO E N ENTRA MAIS NINGUEM
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
