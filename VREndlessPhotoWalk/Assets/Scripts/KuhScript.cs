using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Splines;

public class KuhScript : MonoBehaviour
{
    [SerializeField] private SplineAnimate splineAnimate;
    [SerializeField] private Material newCowMat;
    [SerializeField] private MeshRenderer meshRenderer;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioResource[] resources;
    [SerializeField] private AudioResource nightHorror;


    public void Trigger()
    {
        if (SunRotater.instance.IsDay())
        {
            source.resource = resources[Random.Range(0, resources.Length)];
        }
        else {
            meshRenderer.material = newCowMat;
            source.resource = nightHorror;
            splineAnimate.MaxSpeed = 10;
        }
        source.Play();
    }
}
