using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine.SceneManagement;

public class MonsterManager : MonoBehaviour
{
    //Monitora o estado do elevador: 0 se tds os botoes estiverem em posicao neutra
    //1 se algum botao estiver apertado
    private int buttonsReleased;

    [SerializeField] private GameObject andarAtual;
    [SerializeField] private GameObject victoryMessage; // Associe esse GameObject no inspector do Unity
    [SerializeField] private GameObject[] monsterPrefabs; // Array de prefabs de monstros
    [SerializeField] private GameObject balão;
    [SerializeField] private Level[] levels;

    private List<GameObject> elevator; // Lista de monstros na cena
    [SerializeField] public int maxFloor;

    private List<GameObject>[] floors;
    private int current_floor;
    private int current_level;

    void Start()
    {
        current_level = 0;
        floors = new List<GameObject>[maxFloor];
        elevator = new List<GameObject>();

        for (int i = 0; i < maxFloor; i++)
        {
            floors[i] = new List<GameObject>();
        }

        InitializeMonsters();
    }

    void InitializeMonsters()
    {
        for (int i = 0; i < levels[current_level].qtd_monsters; i++)
        {
            // Sorteia aleatoriamente o tipo de monstro a ser gerado
            GameObject monsterPrefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];
            GameObject monster = Instantiate(monsterPrefab, new Vector3(-5f, 0, 0), Quaternion.identity, transform);

            int current_floor = Random.Range(1, maxFloor);
            int targetFloor = Random.Range(1, maxFloor);

            while (targetFloor == current_floor)
            {
                targetFloor = Random.Range(1, maxFloor);
            }

            monster.GetComponent<Monster>().Initiate(current_floor, targetFloor, this);

            floors[current_floor].Add(monster);
            monster.SetActive(false);
        }
    }

    public void RemoveMonsterFromElevator(GameObject monster, int floor)
    {
        if (elevator.Contains(monster))
        {
            elevator.Remove(monster);
            Debug.Log($"Monstro {monster.name} removido do elevador");
        }

        RemoveMonsterFromFloor(floor, monster);
    }

    void RemoveMonsterFromFloor(int floor, GameObject monster)
    {
        if (floors[floor].Contains(monster))
        {
            floors[floor].Remove(monster);
            Debug.Log($"Monstro {monster.name} removido da lista do andar {floor}");
        }
    }

    public void OnElevatorArrived(int floor)
    {
        Debug.Log($"Elevador chegou ao andar {floor}");
        ActivateMonstersOnFloor(floor);

        List<GameObject> monstersToRemove = new List<GameObject>();

        foreach (GameObject monsterObject in elevator)
        {
            Monster monster = monsterObject.GetComponent<Monster>();

            if (monster.targetFloor == floor)
            {
                Debug.Log($"Monstro {monster.name} desativado ao chegar no andar {floor}");
                
                monstersToRemove.Add(monsterObject);
                Destroy(monsterObject);
                RemoveMonsterFromFloor(floor, monsterObject); // Adicione isto
            }
        }

        foreach (GameObject monsterToRemove in monstersToRemove)
        {
            elevator.Remove(monsterToRemove);
        }

        UpdateBalloon();
        CheckForVictory();  // Verifica se todos os monstros foram entregues
    }

    public void AddToElevator(GameObject monster)
    {
        elevator.Add(monster);
        UpdateBalloon();
    }
    
    void UpdateBalloon()
    {
        if (elevator.Count > 0)
        {
            balão.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = elevator[0].GetComponent<Monster>().targetFloor.ToString();
            balão.SetActive(true);
        }
        else
        {
            balão.SetActive(false);
        }
    }
    
    public void ActivateMonstersOnFloor(int floor)
    {
        if (floor > 0 && floor < floors.Length)
        {
            foreach (GameObject monsterObject in floors[floor])
            {
                if (monsterObject != null)
                {
                    monsterObject.SetActive(true);
                    AddToElevator(monsterObject);
                }
            }
        }
        else
        {
            Debug.LogError($"Floor index {floor} is out of bounds!");
        }
    }

    public IEnumerator RespawnMonster(GameObject monsterPrefab, int respawnFloor)
    {
        yield return new WaitForSeconds(6); // Tempo de respawn de 6 segundos

        GameObject monster = Instantiate(monsterPrefab, new Vector3(-5f, 0, 0), Quaternion.identity, transform);
        monster.GetComponent<Monster>().Initiate(respawnFloor, Random.Range(1, maxFloor), this);

        floors[respawnFloor].Add(monster);
        monster.SetActive(false);
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

    void CheckForVictory()
    {
        bool allMonstersDelivered = true;
        
        for (int i = 0; i < floors.Length; i++)
        {
            if (floors[i].Count > 0)
            {
                Debug.Log($"Ainda há {floors[i].Count} monstros no andar {i}");
                allMonstersDelivered = false;
            }
        }

        if (elevator.Count > 0)
        {
            Debug.Log($"Ainda há {elevator.Count} monstros no elevador");
            allMonstersDelivered = false;
        }

        if (allMonstersDelivered)
        {
            StartCoroutine(HandleVictory());
        }
    }

    IEnumerator HandleVictory()
    {
        // Exibir uma mensagem de vitória na tela
        ShowVictoryMessage();
        print("Todos os monstros foram entregues");

        // Aguarde alguns segundos antes de trocar a cena
        yield return new WaitForSeconds(5);

        // Carrega a cena do menu principal
        SceneManager.LoadScene("Menu");
    }

    void ShowVictoryMessage()
    {
        if (victoryMessage != null)
        {
            victoryMessage.SetActive(true);
        }
    }
}
