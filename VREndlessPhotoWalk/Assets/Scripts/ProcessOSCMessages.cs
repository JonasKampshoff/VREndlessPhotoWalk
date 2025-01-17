using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpOSC;
using System.Net;

public class ProcessOSCMessages : MonoBehaviour
{
    public OscReceiver oscReceiver;
    [SerializeField] private PlayerSpaceMovement playerSpaceMovement;

    void Update()
    {
        if (oscReceiver.HasMessagesWaiting())
        {
            OscMessage oscMessage = oscReceiver.GetNextMessage();
            ProcessMessage(oscMessage);
        }
    }
    void ProcessMessage(OscMessage oscMessage)
    {
        Debug.Log(oscMessage);
        if (oscMessage.Address == "/speed/0")
        {
            float readValue = float.Parse(oscMessage.Arguments[0].ToString());
            playerSpaceMovement.aimedSpeed = readValue * Mathf.PI * 5 /6000;
        }
        else
        {
            Debug.Log(oscMessage.Address);
        }
    }
}