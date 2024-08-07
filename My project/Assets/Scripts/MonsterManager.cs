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

    [SerializeField] private GameObject[] monsterPrefabs; // Array de prefabs de monstros
    [SerializeField] private GameObject balão;
    [SerializeField] private Level[] levels;

    private List<GameObject> elevator; // Lista de monstros na cena
    [SerializeField] private int maxFloor = 7;

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

            int current_floor = Random.Range(1, maxFloor-1);
            int targetFloor = Random.Range(1, maxFloor-1);

            while (targetFloor == current_floor)
            {
                targetFloor = Random.Range(1, maxFloor-1);
            }

            monster.GetComponent<Monster>().Initiate(current_floor, targetFloor, this);

            floors[current_floor].Add(monster);
            monster.SetActive(false);
        }
    }

    public void OnElevatorArrived(int floor)
    {
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
            }
        }

        foreach (GameObject monsterToRemove in monstersToRemove)
        {
            elevator.Remove(monsterToRemove);
        }

        UpdateBalloon();
    }

    // public void OnButtonPress(int floor, GameObject novo)
    // {
    //     novo.SetActive(true);

    //     ActivateMonstersOnFloor(floor);

    //     List<GameObject> monstersToRemove = new List<GameObject>();

    //     foreach (GameObject monsterObject in elevator)
    //     {
    //         Monster monster = monsterObject.GetComponent<Monster>();

    //         if(monster.targetFloor == floor)
    //         {
    //             Debug.Log($"Monstro {monster.name} desativado ao chegar no andar {floor}");
                
    //             monstersToRemove.Add(monsterObject);
    //             Destroy(monsterObject);
    //         }
    //     }

    //     foreach (GameObject monsterToRemove in monstersToRemove)
    //     {
    //         elevator.Remove(monsterToRemove);
    //     }

    //     UpdateBalloon();
    // }

    public void AddToElevator(GameObject monster)
    {
        elevator.Add(monster);
        UpdateBalloon();
    }
    
    public void RemoveFromElevator(GameObject monster)
    {
        elevator.Remove(monster);
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
        foreach (GameObject monsterObject in floors[floor])
        {
            monsterObject.SetActive(true);
            Debug.Log($"Monstro ativado no andar {floor}");
            AddToElevator(monsterObject);
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

    // public void ActivateMonstersOnFloor(int floor)
    // {
    //     // Ativar monstros no andar especificado
    //     foreach (GameObject monsterObject in floors[current_floor])
    //     {
    //         monsterObject.SetActive(true);
    //         Debug.Log($"Monstro ativado no andar {floor}");
    //         balão.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = monsterObject.GetComponent<Monster>().targetFloor.ToString();
    //         balão.SetActive(true);

    //         //TO DO: LEVAR EM CONTA PESO DO MONSTRO E VER QUAL MONSTRO NA FILA PODE ENTRAR
    //         //O PRIMEIRO E SEMPRE O QUE ENTRA E SE ELE PASSAR O PESO O ELEVADOR FICA PESADO E N ENTRA MAIS NINGUEM
    //     }
    // }

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
