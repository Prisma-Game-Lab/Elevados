using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterB : Monster
{
    private List<GameObject> objetosPraInverter = new();
    private void Start()
    {
        objetosPraInverter.Add(GameObject.FindWithTag("Background"));
        objetosPraInverter.Add(GameObject.Find("Canvas").transform.GetChild(0).gameObject);

        InverteObjetos();
    }

    public void InverteObjetos()
    {
        for (int i = 0; i < objetosPraInverter.Count; i++)
        {
            var atual = objetosPraInverter[i];
            atual.transform.localScale = new Vector3(atual.transform.localScale.x, atual.transform.localScale.y * -1, atual.transform.localScale.z);
        }
    }

    private void OnDisable()
    {
        InverteObjetos();
    }

    private void OnEnable()
    {
        InverteObjetos();
    }
}
