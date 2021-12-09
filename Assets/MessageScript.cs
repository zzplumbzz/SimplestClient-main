using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageScript : MonoBehaviour
{
    GameObject networkedClient;

    public Text TextField1;
    

    public void SetText(string text1)
    {
        TextField1.text = text1;

        //networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.GGButtonPressed + "" + text1);
        //networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.GGButtonPressed + "" + TextField1);
        
       // networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.HelloButtonPressed + "" + text1);
        //networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.HelloButtonPressed + "" + TextField1);

        //networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ServerToClientSignifiers.SendGGButtonPressed + text1);
        //networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ServerToClientSignifiers.SendHelloButtonPressed + text1);
    }

    public void SendtextToServer(string text, string msg, int id)
    {
        string[] csv = msg.Split(',');

        int signifier = int.Parse(csv[0]);
       
        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.GGButtonPressed + ",trying to send to both clients" + TextField1 + id);

       
        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.HelloButtonPressed + ",trying to send to both clieents" + TextField1 + id);

        if(signifier == ServerToClientSignifiers.SendGGButtonPressed || signifier == ServerToClientSignifiers.SendHelloButtonPressed)
        {
            TextField1.text = text;
        }
    }

    //private void Update()
    //{
    //    if (Time.deltaTime == 1)
    //    {
    //        TextField1.text = null;

    //    }
    //}
}
