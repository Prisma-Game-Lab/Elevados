using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine.SceneManagement;

public class MonsterManager : MonoBehaviour
{
    private int buttonsReleased;

    [SerializeField] private GameObject andarAtual;
    [SerializeField] private GameObject victoryMessage;
    [SerializeField] private GameObject[] monsterPrefabs;
    [SerializeField] private GameObject balão;
    [SerializeField] private Level[] levels;

    private List<GameObject> elevator;
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
                if (ScoreManager.instance != null)
                {
                    ScoreManager.instance.AddScore(1);
                }
                else
                {
                    Debug.LogError("ScoreManager instance is null! Ensure ScoreManager is properly initialized.");
                }
            }
        }

        foreach (GameObject monsterToRemove in monstersToRemove)
        {
            RemoveMonsterFromElevator(monsterToRemove, floor);
            Destroy(monsterToRemove);
        }

        UpdateBalloon();
        CheckForVictory();
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
        yield return new WaitForSeconds(6);

        GameObject monster = Instantiate(monsterPrefab, new Vector3(-5f, 0, 0), Quaternion.identity, transform);
        monster.GetComponent<Monster>().Initiate(respawnFloor, Random.Range(1, maxFloor), this);

        floors[respawnFloor].Add(monster);
        monster.SetActive(false);
    }

    public int hold()
    {
        if (buttonsReleased == 0)
        {
            buttonsReleased = 1;
            return 1;
        }

        return 0;
    }

    public void release()
    {
        buttonsReleased = 0;
    }

    public void TriggerVictory()
    {
        StopAllCoroutines(); // Para o cronômetro ou qualquer outra rotina em andamento
        ShowVictoryMessage();

        // Aguarda alguns segundos antes de trocar a cena para o menu
        StartCoroutine(ReturnToMenuAfterDelay());
    }

    IEnumerator ReturnToMenuAfterDelay()
    {
        yield return new WaitForSeconds(5); // Tempo de exibição da mensagem de vitória
        SceneManager.LoadScene("Menu"); // Carrega a cena do menu principal
    }
    
    void ShowVictoryMessage()
    {
        if (victoryMessage != null)
        {
            victoryMessage.SetActive(true);
        }
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
        ShowVictoryMessage();
        Debug.Log("Todos os monstros foram entregues");

        yield return new WaitForSeconds(5); // Tempo de espera antes de voltar ao menu
        SceneManager.LoadScene("Menu");
    }
}
