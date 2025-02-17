using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] Transform movePos;
    [SerializeField] Transform moveNeg;


    private bool isOpen = false;
    private float value = 0;

    public bool enterRoom = true;

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            if (value < 0.6f)
            {
                value += Time.deltaTime;
            }
            movePos.localPosition = new Vector3(value, 0, 0);
            moveNeg.localPosition = new Vector3(-value, 0, 0);
        }
    }

    public void Open()
    {
        if (!isOpen)
        {
            isOpen = true;
            RenderSettings.fog = !enterRoom;
            SunRotater.instance.OnOff(!enterRoom);
        }
    }    
}
