using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorButtonScript : MonoBehaviour 
{
    [SerializeField] private Sprite button_pushed;
    [SerializeField] private Sprite button_neutral;

    [SerializeField] private GameObject andar;

    private AudioManager audioManager;
    private Image button_image;
    private MonsterManager monster_manager;

    public Cronometro cronometro; // Referência ao script do cronômetro
    public GameObject background_floor;
    public GameObject monster_manager_object; // Referência ao objeto que possui MonsterManager
    public DoorController doorController; // Referência ao DoorController
    public int floor; // Número do andar correspondente ao botão
    private bool isPressed; 
    public bool canPress = true;

    void Awake() 
    {
        if (cronometro == null)
        {
            cronometro = FindObjectOfType<Cronometro>();
        }

        // Pega o número do andar atual correto a partir do número escrito no botão
        floor = int.Parse(transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text);

        button_image = GetComponent<Image>();
        monster_manager = monster_manager_object.GetComponent<MonsterManager>(); // Inicializa a referência ao MonsterManager

        // Inicializa a referência ao AudioManager
        audioManager = AudioManager.instance;

        if (audioManager == null)
        {
            Debug.LogError("AudioManager instance is null! Ensure AudioManager is properly initialized.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetButton();
    }

    public void pressButton()
    {
        if (isPressed)
        {
            Debug.Log("Botão já pressionado.");
            return;
        }
        else
        {
            if (monster_manager.hold() == 1 && canPress)
            {
                // Toca o som de clique do botão, se possível
                if (audioManager != null)
                {
                    audioManager.PlayButtonClickSFX();
                }
                
                SetButtonPressed();
                StartCoroutine(releaseButton());

                StartCoroutine(HandleElevatorMovement());
            }
        }
    }

    private IEnumerator HandleElevatorMovement()
    {
        if (doorController != null)
        {
            // Chama o DoorController para iniciar o movimento do elevador com a animação das portas
            doorController.MoveElevator(floor, andar);

            // Espera a animação de movimentação do elevador e troca do fundo do andar
            yield return new WaitForSeconds(6); // Ajuste o tempo de acordo com a duração das animações
        }

        // Notifica o MonsterManager após a animação do elevador
        monster_manager.OnElevatorArrived(floor);

        // Inicia o cronômetro quando o botão de um andar diferente do andar inicial é pressionado
        if (floor != 1)
        {
            cronometro.ElevadorSaiuDoAndarInicial();
        }
    }

    private void SetButtonPressed()
    {
        isPressed = true;
        button_image.sprite = button_pushed;
        Debug.Log("Botão pressionado.");
    }

    IEnumerator releaseButton()
    {
        Debug.Log("Iniciando corrotina releaseButton");
        yield return new WaitForSeconds(2);
        Debug.Log("Executando ResetButton");
        ResetButton();
        monster_manager.release();
        Debug.Log("Botão liberado.");
    }

    private void ResetButton()
    {
        isPressed = false;
        button_image.sprite = button_neutral;
    }
}
