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

    private Image button_image;
    private Image floor_image;
    private ButtonManager button_manager;
    private MonsterManager monster_manager;

    public GameObject background_floor;
    public GameObject manager_object;
    public GameObject monster_manager_object; // Referência ao objeto que possui MonsterManager
    public int floor; // adicione uma variável para o andar correspondente ao botão
    private bool isPressed;

    void Awake() //inicializa as referências a componentes que nao dependem de outros objetos
    {
        button_image = GetComponent<Image>();
        floor_image = background_floor.GetComponent<Image>();
        button_manager = manager_object.GetComponent<ButtonManager>();
        monster_manager = monster_manager_object.GetComponent<MonsterManager>(); // inicializa a referencia ao Monstro
    }

    // Start is called before the first frame update
    void Start()
    {
        //Botao comeca nao pressionado, no estado 0
        //state = 0;

        //button_image = GetComponent<Image>();
        //button_image.sprite = button_neutral;

        //floor_image = background_floor.GetComponent<Image>();

        //button_manager = manager_object.GetComponent<ButtonManager>();
        ResetButton();
    }

    public void pressButton()
    {
        if (isPressed)
        {
            Debug.Log("Calma ai rapaz");
            return;
        }

        if (button_manager.hold() == 1)
        {
            SetButtonPressed();
            StartCoroutine(releaseButton());
            Debug.Log($"Notificando o MonsterManager sobre o andar pressionado: {floor}");
            monster_manager.OnButtonPress(floor); // notifique o Monstro sobre o andar pressionado
        }

        /*/
        if (state == 0)
        {
            if (button_manager.hold() == 1)
            {
                state = 1;
                button_image.sprite = button_pushed;
                floor_image.sprite = button_floor;
                print("Pressionado");

                StartCoroutine(releaseButton());
            }
        }

        else { 
            print("Calma ai rapaz"); 
        }
        /*/
    }

    private void SetButtonPressed()
    {
        isPressed = true;
        button_image.sprite = button_pushed;
        floor_image.sprite = button_floor;
        Debug.Log("botao pressionado");
    }

    IEnumerator releaseButton()
    {
        yield return new WaitForSeconds(2);
        ResetButton();
        /*/
        state = 0;
        button_image.sprite = button_neutral;

        print("Solto");
        /*/
        button_manager.release();
        Debug.Log("Ja pode pressionar");
    }

    private void ResetButton()
    {
        isPressed = false;
        button_image.sprite = button_neutral;
    }
}
