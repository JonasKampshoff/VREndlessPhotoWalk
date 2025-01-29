using UnityEngine;

public class CopyPosition : MonoBehaviour
{
    [SerializeField] private Transform from;
    private Vector3 offset;

    private void Start()
    {
        offset = transform.position;
    }

    void Update()
    {

        transform.position = from.position + offset;
    }
}
