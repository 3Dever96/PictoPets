using UnityEngine;

[CreateAssetMenu(fileName = "New Stat", menuName = "Database/ Stat")]
public class Stat : ScriptableObject
{
    public string statName;
    public string statAbb;
    [Multiline(3)] public string statDescription;
    public int ringPosition;
    public float[] offsetValues = { 16f, 15f, 14f, 13f, 12f, 11f, 10f };
}
