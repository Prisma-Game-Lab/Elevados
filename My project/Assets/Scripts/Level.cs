using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Novo nivel", menuName = "Niveis")]
public class Level : ScriptableObject
{
    public int qtd_monsters;
    public int total_time;
}
