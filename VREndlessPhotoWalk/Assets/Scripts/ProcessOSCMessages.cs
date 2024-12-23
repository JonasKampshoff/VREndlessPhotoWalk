using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpOSC;

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
        if (oscMessage.Address == "/speed")
        {
            playerSpaceMovement.aimedSpeed = float.Parse(oscMessage.Arguments[0].ToString());
        }
        else
        {
            Debug.Log(oscMessage.Address);
        }
    }
}