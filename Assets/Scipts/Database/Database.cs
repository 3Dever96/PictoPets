using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Database", menuName = "Database/ Database")]
public class Database : ScriptableObject
{
    [Header("Stat Ring")]
    public Stat[] Stats;

    [Header("Type Ring")]
    public ElementType[] Types;

    [Header("Trait Ring")]
    public PhyscialTrait[] PhyscialTraits;

    [Header("Beastiary")]
    public List<PhotomorphData> PhotomorphData = new List<PhotomorphData>();
}
