using Unity.Collections;
using UnityEngine;

public class PlayerSpaceMovement : MonoBehaviour
{
    [SerializeField] private float maxAcceleration = 1; // in m/s^2
    [SerializeField] public float aimedSpeed = 0; // in m/s
    [SerializeField] private float currentSpeed = 0; // in m/s

    void Update()
    {
        float speedDif = aimedSpeed - currentSpeed;
        currentSpeed += Mathf.Min(Mathf.Abs(speedDif),maxAcceleration * Time.deltaTime) * Mathf.Sign(speedDif);
    }

    private void FixedUpdate()
    {
        transform.Translate(transform.forward * currentSpeed * Time.fixedDeltaTime);
    }
}
