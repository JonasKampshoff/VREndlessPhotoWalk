using UnityEngine;
using TMPro;
using System.Net;


public class IPUI : MonoBehaviour
{
    [SerializeField] TMP_Text m_Text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string strHostName = System.Net.Dns.GetHostName();
        IPHostEntry hostEntry = System.Net.Dns.GetHostEntry(strHostName);

        IPAddress[] addressList = hostEntry.AddressList;

        m_Text.text = addressList[^1].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
