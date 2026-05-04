using UnityEngine;

public class PhotomorphEntity
{
    public PhotomorphData data;

    public int level;

    public float maxHp;
    public float maxMp;
    public float maxSp;
    public float atk;
    public float def;
    public float mAtk;
    public float mDef;
    public float agi;
    public float luk;
    public float ins;
    public float res;
    public float loy;

    public float currentHp;
    public float currentMp;
    public float currentSp;

    public PhotomorphEntity(int minLevel, int maxLevel, int photomorphIndex)
    {
        Database database = Database.instance;

        data = database.PhotomorphData[photomorphIndex];

        level = Mathf.Max(1, (Random.Range(minLevel, maxLevel) + Random.Range(minLevel, maxLevel)) / 2);

        float baseDef = database.Stats[0].GetValue((int)data.elementType) + database.Stats[0].GetValue((int)data.physicalTrait);
        float baseIns = database.Stats[1].GetValue((int)data.elementType) + database.Stats[1].GetValue((int)data.physicalTrait);
        float baseMDef = database.Stats[2].GetValue((int)data.elementType) + database.Stats[2].GetValue((int)data.physicalTrait);
        float baseMp = database.Stats[3].GetValue((int)data.elementType) + database.Stats[3].GetValue((int)data.physicalTrait);
        float baseMAtk = database.Stats[4].GetValue((int)data.elementType) + database.Stats[4].GetValue((int)data.physicalTrait);
        float baseRes = database.Stats[5].GetValue((int)data.elementType) + database.Stats[5].GetValue((int)data.physicalTrait);
        float baseLuk = database.Stats[6].GetValue((int)data.elementType) + database.Stats[6].GetValue((int)data.physicalTrait);
        float baseSp = database.Stats[7].GetValue((int)data.elementType) + database.Stats[7].GetValue((int)data.physicalTrait);
        float baseAgi = database.Stats[8].GetValue((int)data.elementType) + database.Stats[8].GetValue((int)data.physicalTrait);
        float baseLoy = database.Stats[9].GetValue((int)data.elementType) + database.Stats[9].GetValue((int)data.physicalTrait);
        float baseAtk = database.Stats[10].GetValue((int)data.elementType) + database.Stats[10].GetValue((int)data.physicalTrait);
        float baseHp = database.Stats[11].GetValue((int)data.elementType) + database.Stats[11].GetValue((int)data.physicalTrait);

        maxHp = CalculateStat(baseHp, level);
        maxMp = CalculateStat(baseMp, level);
        maxSp = CalculateStat(baseSp, level);
        atk = CalculateStat(baseAtk, level);
        def = CalculateStat(baseDef, level);
        mAtk = CalculateStat(baseMAtk, level);
        mDef = CalculateStat(baseMDef, level);
        agi = CalculateStat(baseAgi, level);
        luk = CalculateStat(baseLuk, level);
        ins = CalculateStat(baseIns, level);
        res = CalculateStat(baseRes, level);
        loy = CalculateStat(baseLoy, level);

        currentHp = maxHp;
        currentMp = maxMp;
        currentSp = maxSp;
    }

    float CalculateStat(float baseStat, int level, float growthSpeed = 0.05f, float maxValue = 200f)
    {
        // As level increases, the 'decay' part gets smaller.
        // The stat 'climbs' toward the maxCap but never exceeds it.
        float growthValue = (maxValue - baseStat) * Mathf.Exp(-growthSpeed * (level - 1));

        return maxValue - growthValue;
    }
}
