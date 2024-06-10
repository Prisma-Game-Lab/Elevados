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

    public GameObject background_floor;
    public GameObject manager_object;

    //Variavel utilizada para verificar se o botao esta pressionado
    private int state;

    // Start is called before the first frame update
    void Start()
    {
        //Botao comeca nao pressionado, no estado 0
        state = 0;

        button_image = GetComponent<Image>();
        button_image.sprite = button_neutral;

        floor_image = background_floor.GetComponent<Image>();

        button_manager = manager_object.GetComponent<ButtonManager>();
    }

    public void pressButton()
    {
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
    }

    IEnumerator releaseButton()
    {
        yield return new WaitForSeconds(2);
        state = 0;
        button_image.sprite = button_neutral;

        print("Solto");

        button_manager.release();
    }
}
