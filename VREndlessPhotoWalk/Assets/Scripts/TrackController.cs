using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour
{
    [Header("TrackParts")]
    [SerializeField] private List<GameObject> trackPart; // 250m
    [SerializeField] private GameObject startLine; // 125m
    [SerializeField] private GameObject finishLine; // 125m

    [Header("CurrentGameParameter")]
    [SerializeField] private int length;
    private int amountOfParts;

    private GameObject[] spawned = new GameObject[5];
    private int nextTrackIndex = 0;


    private void Start()
    {
        amountOfParts = (length / 250) + 1;
        GameObject newGameobject = Instantiate(startLine);
        newGameobject.GetComponent<TrackSecion>().controller = this;
        spawned[nextTrackIndex++] = newGameobject;
        for(int i = 0; i < 2; i++)
        {
            SpawnNext();
        }
    }

    public void SpawnNext()
    {
        TrackSecion last = spawned[(nextTrackIndex - 1) % spawned.Length].GetComponent<TrackSecion>();
        if(spawned[nextTrackIndex % spawned.Length] != null)
        {
            Destroy(spawned[nextTrackIndex % spawned.Length]);
        }
        GameObject newGameobject;
        if (amountOfParts == nextTrackIndex - 1)
        {
            newGameobject = Instantiate(finishLine, last.nextSpawnPoint.position, last.nextSpawnPoint.rotation);
        }
        else
        {
            newGameobject = Instantiate(trackPart[Random.Range(0, trackPart.Count)], last.nextSpawnPoint.position, last.nextSpawnPoint.rotation);
        }
        newGameobject.GetComponent<TrackSecion>().controller = this;
        spawned[nextTrackIndex++ % spawned.Length] = newGameobject;
    }
}
