using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWeight : MonoBehaviour
{
    public enum WeightCategory { Leve, Medio, Pesado }
    public WeightCategory weightCategory;

    public int GetWeightPoints()
    {
        switch (weightCategory)
        {
            case WeightCategory.Leve:
                return 1;
            case WeightCategory.Medio:
                return 2;
            case WeightCategory.Pesado:
                return 3;
            default:
                return 0;
        }
    }
}
