using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TypUnit", menuName = "ScriptableObjects/TypUnit", order = 1)]
public class UnitTypes : ScriptableObject
{
    public UnitType unitType;
    public BaseUnit baseUnit;
}

