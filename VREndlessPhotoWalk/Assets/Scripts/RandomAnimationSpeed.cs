using UnityEngine;
using UnityEngine.Splines;

public class RandomAnimationSpeed : MonoBehaviour
{
    [SerializeField] private SplineAnimate splineAnimate;

    [SerializeField] private float min;
    [SerializeField] private float max;

    void Start()
    {
        splineAnimate.MaxSpeed = Random.Range(min, max);
    }

}
