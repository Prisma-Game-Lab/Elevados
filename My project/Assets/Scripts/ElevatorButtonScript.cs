using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorButtonScript : MonoBehaviour 
{
    [SerializeField]
    private Sprite button_pushed;
    [SerializeField]
    private Sprite button_neutral;
    [SerializeField]
    private Sprite button_floor;

    [SerializeField]
    private GameObject andar;

    private Image button_image;
    private Image floor_image;
    private MonsterManager monster_manager;

    public Cronometro cronometro; // Referencia ao script do cronometro
    public GameObject background_floor;
    public GameObject monster_manager_object; // Referência ao objeto que possui MonsterManager
    public int floor; // adicione uma variável para o andar correspondente ao botão
    private bool isPressed; 
    public bool canPress = true;

    void Awake() //inicializa as referências a componentes que nao dependem de outros objetos
    {
        // Inicialize cronometro se necessário
        if (cronometro == null)
        {
            cronometro = FindObjectOfType<Cronometro>();
        }
        button_image = GetComponent<Image>();
        floor_image = background_floor.GetComponent<Image>();
        monster_manager = monster_manager_object.GetComponent<MonsterManager>(); // inicializa a referencia ao Monstro
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
            Debug.Log("Calma ai rapaz");
            return;
        }

        else
        {
            if (monster_manager.hold() == 1 && canPress)
            {
                SetButtonPressed();
                StartCoroutine(releaseButton());
                Debug.Log($"Notificando o MonsterManager sobre o andar pressionado: {floor}");
                monster_manager.OnButtonPress(floor,andar); // notifique o Monstro sobre o andar pressionado

                // Iniciar cronômetro quando o botão de um andar diferente do andar inicial for pressionado
                if (floor != 1)
                {
                    cronometro.ElevadorSaiuDoAndarInicial();
                }
            }
        }
    }

    private void SetButtonPressed()
    {
        isPressed = true;
        button_image.sprite = button_pushed;
        // floor_image.sprite = button_floor;
        Debug.Log("botao pressionado");
    }

    IEnumerator releaseButton()
    {
        Debug.Log("Iniciando corrotina releaseButton");
        yield return new WaitForSeconds(2);
        Debug.Log("Executando ResetButton");
        ResetButton();
        monster_manager.release();
        Debug.Log("Ja pode pressionar");
    }

    private void ResetButton()
    {
        isPressed = false;
        button_image.sprite = button_neutral;
    }
}
