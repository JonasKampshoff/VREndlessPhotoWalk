using UnityEngine;
using UnityEngine.Events;

public class PhotoEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent eventToTrigger;

    private bool triggerd;

    public void TriggerEvent()
    {
        if (!triggerd)
        {
            Debug.Log("Trigger");
            triggerd = true;
            eventToTrigger.Invoke();
        }
    }
}
