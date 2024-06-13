using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monstro : MonoBehaviour
{
    public int desired_floor { get; private set; }
    public void RequestRandomFloor(int totalFloors)
    {
        desired_floor = Random.Range(0, totalFloors);
    }
}
