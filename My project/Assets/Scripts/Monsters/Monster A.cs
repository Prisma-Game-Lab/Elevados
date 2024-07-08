using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterA : Monster
{
    private List<GameObject> buttons = new();
    private int numButtons;
    [SerializeField]
    private float tempoParaIniciar;

    void Start()
    {
        var buttonManager = GameObject.Find("Canvas").transform.GetChild(0);
        numButtons = buttonManager.transform.childCount;
        foreach (Transform child in buttonManager)
        {
            buttons.Add(child.gameObject);
        }

        StartCoroutine(EmbaralhaBotoes());
    }

    private IEnumerator EmbaralhaBotoes()
    {
        yield return new WaitForSeconds(tempoParaIniciar);
        int button1 = 0, button2 = 0;

        while (button1 == button2)
        {
            button1 = Random.Range(0, numButtons - 1);
            button2 = Random.Range(0, numButtons - 1);
        }

        buttons[button1].transform.SetSiblingIndex(button2);
        Debug.Log("trocou os botões");
    }
}
