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
        var buttonManager = GameObject.Find("ButtonManager").transform;
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

        for (int i = 0; i < buttons.Count; i += 2)
        {
            buttons[i].transform.SetSiblingIndex(buttons[i+1].transform.GetSiblingIndex());
        }
    }
}
