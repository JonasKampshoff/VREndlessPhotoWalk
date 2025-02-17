using UnityEngine;
using UnityEngine.Events;

public class TriggerToEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent onTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<CharacterController>(out _))
        {
            Debug.Log("Trigger");
            onTrigger.Invoke();
        }
    }
}
