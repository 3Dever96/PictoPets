using UnityEngine;

public class WildPhotomorphController : MonoBehaviour
{
    Transform spawnPoint;
    PhotomorphAvatar avatar;

    public void SpawnIn(Transform newSpawnPoint, PhotomorphEntity entity)
    {
        spawnPoint = newSpawnPoint;
        avatar.Initialize(entity);
        transform.position = spawnPoint.position;
    }

    public void OnEnable()
    {
        if (avatar == null)
        {
            avatar = GetComponentInChildren<PhotomorphAvatar>();
        }
    }
}
