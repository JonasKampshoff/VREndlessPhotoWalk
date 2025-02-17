using UnityEngine;

public class SunRotater : MonoBehaviour
{
    [SerializeField] private float rotation = 0;

    [SerializeField] private float ambientIntensity = 0;
    [SerializeField] private Light sunLight;

    public static SunRotater instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotation = (rotation + Time.deltaTime * (rotation < 180 ? 2 : 1)) % 360;

        if (rotation < 180)
        {
            //Nacht
            ambientIntensity = Mathf.Abs((rotation - 90) / 180);
        }
        else
        {
            //Tag
            ambientIntensity = 1f - Mathf.Abs((rotation - 270) / 180);
        }
        RenderSettings.fogColor = Color.white * ambientIntensity * ambientIntensity;
        sunLight.intensity = ambientIntensity + 0.1f;
        RenderSettings.ambientIntensity = ambientIntensity + 0.1f;
        transform.localRotation = Quaternion.Euler(0, rotation, 0);
    }

    public bool IsDay()
    {
        return rotation >= 180;
    }

    public void OnOff(bool value)
    {
        sunLight.enabled = value;
    }
}
