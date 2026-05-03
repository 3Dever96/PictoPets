using UnityEngine;

[CreateAssetMenu(fileName = "New Photomorph", menuName = "Database/ Photomorph")]
public class PhotomorphData : ScriptableObject
{
    public enum Type
    {
        Earth,
        Lunar,
        Poison,
        Gem,
        Water,
        Wind,
        Null,
        Wood,
        Solar,
        Fire,
        Thunder,
        Ice
    }

    public enum Trait
    {
        Shell,
        Sticky,
        Stinger,
        Digger,
        Fin,
        Wing,
        Fang,
        Horn,
        Camouflage,
        Claw,
        Stomp,
        Beak
    }

    public string photomorphName;
    public string index;
    public Type elementType;
    public Trait physicalTrait;
}
