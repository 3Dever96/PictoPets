using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    public static Database instance { get; private set; }

    [Header("Stat Ring")]
    public Stat[] Stats;

    [Header("Type Ring")]
    public ElementType[] Types;

    [Header("Trait Ring")]
    public PhyscialTrait[] PhyscialTraits;

    [Header("Beastiary")]
    public List<PhotomorphData> PhotomorphData = new List<PhotomorphData>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }
}
