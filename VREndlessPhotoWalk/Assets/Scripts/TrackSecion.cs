using UnityEngine;

public class TrackSecion : MonoBehaviour
{
    public Transform nextSpawnPoint;
    public TrackController controller;

    public void SpawnNext()
    {
        controller.SpawnNext();
    }

}
