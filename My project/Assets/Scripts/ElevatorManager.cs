// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class ElevatorManager : MonoBehaviour
// {
//     public Monstro[] monstros;
//     public Transform elevator;
//     public Text scoreText;
//     public Text timer;
//     public Button[] floorButtons;

//     private int currentMonsterIndex = 0;
//     private int score = 0; // contador para a pontuação
//     private int currentFloor = 0;
//     private int totalFloors = 4;
//     private float gameTime = 60f; // duração de jogo em segundos

//     private bool isGameActive = true; // controlar estado do jogo

//     // Start is called before the first frame update
//     void Start()
//     {
//         // incializa os botoes dos andares
//         for (int i = 0; i < floorButtons.Length; i++)
//         {
//             int floor = i;
//             floorButtons[i].onClick.AddListener(() => OnFloorButtonClicked(floor));
//         }

//         // spawna primeiro monstro
//         SpawnNextMonster();
//     }

//     void Update()
//     {
//         if (isGameActive)
//         {
//             gameTime -= Time.deltaTime;
//             UpdateTimer();

//             if(gameTime <= 0)
//             {
//                 EndGame();
//             }
//         }
//     }

//     void SpawnNextMonster()
//     {
//         if (currentMonsterIndex < monstros.Length)
//         {
//             Monstro currentMonster = monstros[currentMonsterIndex];
//             currentMonster.gameObject.SetActive(true);
//             currentMonster.RequestRandomFloor(totalFloors);
//             Debug.Log("Monstro quer ir para o andar: " + currentMonster.desired_floor);
//         }
//         else
//         {
//             Debug.Log("Todos os monstros foram atendidos!");
//         }
//     }

//     public void OnFloorButtonClicked(int floor)
//     {
//         if(isGameActive && currentMonsterIndex < monstros.Length)
//         {
//             Monstro currentMonster = monstros[currentMonsterIndex];
//             if(currentMonster.desired_floor == floor)
//             {
//                 Debug.Log("Levando monstro para o andar: " + floor);
//                 currentFloor = floor;
//                 score += 10;
//                 UpdateScore();

//                 // desativa o monstro atual
//                 currentMonster.gameObject.SetActive(false);
//                 currentMonsterIndex++;

//                 // outro monstro entra no elevador
//                 SpawnNextMonster();
//             }
//             else
//             {
//                 Debug.Log("Andar errado! Monstro quer ir para o andar: "+ currentMonster.desired_floor);
//             }
//         }
//     }

//     // Update is called once per frame
//     void UpdateScore()
//     {
//         scoreText.text = "Score: " + score;
//     }

//     void UpdateTimer()
//     {
//         int minutes = Mathf.FloorToInt(gameTime / 60);
//         int seconds = Mathf.FloorToInt(gameTime % 60);
//         timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
//     }

//     void EndGame()
//     {
//         isGameActive = false;
//         Debug.Log("Fim de jogo! Pontuação final: " + score);
//     }
// }
