using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterC : Monster
{
    private List<GameObject> buttons = new();
    private ElevatorButtonScript lockedButton;
    private int numButtons;
    void Start()
    {
        var buttonManager = GameObject.Find("ButtonManager").transform;
        numButtons = buttonManager.transform.childCount;
        foreach (Transform child in buttonManager)
        {
            buttons.Add(child.gameObject);
        }
        
        int button = Random.Range(0, buttons.Count - 1);
        lockedButton = buttons[button].GetComponent<ElevatorButtonScript>();

        Debug.Log($"Botão {button} travado");

    }

    public void TravaBotao()
    {
        lockedButton.canPress = false;
    }
    public void DestravaBotao()
    {
        lockedButton.canPress = true;
    }
}
