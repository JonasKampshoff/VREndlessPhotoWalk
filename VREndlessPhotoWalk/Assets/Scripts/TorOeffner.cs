using UnityEngine;

public class TorOeffner : MonoBehaviour
{
    private bool isOpen = false;

    private float winkel = 90;

    [SerializeField] AudioSource source;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            if(winkel > 0)
            {
                winkel -= Time.deltaTime * 20;
            }
            transform.rotation = Quaternion.Euler(winkel, 12f, 0f);
        }
    }

    public void Open()
    {
        isOpen = true;
        source.Play();
    }
}
