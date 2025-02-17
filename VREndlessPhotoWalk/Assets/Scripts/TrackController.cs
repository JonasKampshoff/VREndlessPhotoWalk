using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class TrackController : MonoBehaviour
{
    [Header("TrackParts")]
    [SerializeField] private List<GameObject> trackPart;
    [SerializeField] private List<GameObject> skyPart;

    [SerializeField] private GameObject startLine;

    private GameObject[] spawned = new GameObject[4];
    private int nextTrackIndex = 0;

    private int nextTrackControllerIndex = 0;

    public void SetUpTrack()
    {
        GameObject newGameobject = Instantiate(startLine);
        spawned[nextTrackIndex++] = newGameobject;
        for (int i = 0; i < 1; i++)
        {
            SpawnNext();
        }
    }

    public void SpawnNext()
    {
        TrackSecion last = spawned[(nextTrackIndex - 1) % spawned.Length].GetComponent<TrackSecion>();
        if(spawned[nextTrackIndex % spawned.Length] != null)
        {
            Destroy(spawned[nextTrackIndex % spawned.Length],1);
        }
        GameObject newGameobject;
        if (nextTrackIndex % 2 == 0)
        {
            newGameobject = Instantiate(trackPart[Random.Range(0, trackPart.Count)], last.nextSpawnPoint.position, last.nextSpawnPoint.rotation);
        }
        else {
            newGameobject = Instantiate(skyPart[Random.Range(0, skyPart.Count)], last.nextSpawnPoint.position, last.nextSpawnPoint.rotation);
        }
        spawned[nextTrackIndex++ % spawned.Length] = newGameobject;
    }

    public SplineContainer NextTrackController()
    {
        SpawnNext();
        spawned[nextTrackControllerIndex % spawned.Length].GetComponent<TrackSecion>().onEnterEvent.Invoke();
        return spawned[nextTrackControllerIndex++%spawned.Length].GetComponent<TrackSecion>().splineContainer;
    }
}
