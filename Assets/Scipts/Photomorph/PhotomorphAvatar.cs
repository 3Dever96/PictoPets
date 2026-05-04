using UnityEngine;

public class PhotomorphAvatar : MonoBehaviour
{
    public PhotomorphEntity entity;

    [SerializeField] GameObject[] avatars;

    public void Initialize(PhotomorphEntity newEntity)
    {
        entity = newEntity;

        foreach (GameObject g in avatars)
        {
            g.SetActive(false);
        }

        avatars[entity.data.index].SetActive(true);
    }
}
