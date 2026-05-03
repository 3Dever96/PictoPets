using UnityEngine;

[CreateAssetMenu(fileName = "New Trait", menuName = "Database/ Trait")]
public class PhyscialTrait : ScriptableObject
{
    public string traitName;
    public int ringPosition;
    public float[] weaknessRates = { 0f, 0.75f, 0.5f, 0.45f, 0.4f, 0.35f, 0.3f, 0.25f, 0.2f, 0.15f, 0.125f, 0.1f };
}
