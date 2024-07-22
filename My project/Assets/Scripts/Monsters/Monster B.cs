using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterB : Monster
{
    private GameObject[] objetosPraInverter;
    private void Start()
    {
        objetosPraInverter = GameObject.FindGameObjectsWithTag("Background");
    }

    public void InverteObjetos()
    {
        foreach (GameObject objeto in objetosPraInverter)
        {
            objeto.transform.Rotate(0.0f, 0.0f, 180.0f);
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
