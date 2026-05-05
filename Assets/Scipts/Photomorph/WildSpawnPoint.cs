using System.Collections.Generic;
using UnityEngine;

public class WildSpawnPoint : MonoBehaviour
{
    [SerializeField] int minLevel;
    [SerializeField] int maxLevel;
    [SerializeField] List<int> wildIndex = new List<int>();
    public bool hasObject;
    [SerializeField] float spawnTime;
    float currentTime;

    WildPhotomorphPool pool;

    private void Start()
    {
        pool = FindAnyObjectByType<WildPhotomorphPool>();

        currentTime = 0f;
        hasObject = false;
    }

    private void Update()
    {
        if (!hasObject)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0f)
            {
                hasObject = true;
                SpawnWild();
            }
        }
        else
        {
            currentTime = spawnTime;
        }
    }

    void SpawnWild()
    {
        int index = SetNewEncounter();

        WildPhotomorphController newWild = pool.GetGameObject().GetComponent<WildPhotomorphController>();
        newWild.SpawnIn(transform, new PhotomorphEntity(minLevel, maxLevel, index));
    }

    int SetNewEncounter()
    {
        List<int> weight = new List<int>();

        int totalWeight = 0;

        for (var i = 0; i < wildIndex.Count; i++)
        {
            weight.Add(Database.instance.PhotomorphData[wildIndex[i]].weight);
            totalWeight += Database.instance.PhotomorphData[wildIndex[i]].weight;
        }

        int roll = Random.Range(0, totalWeight);
        int cursor = 0;

        foreach (int i in weight)
        {
            cursor += i;
            if (roll < cursor)
            {
                return wildIndex[weight.IndexOf(i)];
            }
        }

        return wildIndex[0];
    }
}
