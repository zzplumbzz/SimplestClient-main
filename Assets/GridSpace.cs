using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GridSpace : MonoBehaviour
{
    [Header("Tic Tac Toe")]
    public Button button;
    public Text buttonText;
    //public string playerSide;
    private GameSystemManger gameSystemManger;
    private NetworkedClient networkedClient;

    public void SetGameSystemManagerReferance(GameSystemManger manager)
    {
        gameSystemManger = manager;
    }

    public void SetSpace()
    {
        buttonText.text = gameSystemManger.GetCurrentPlayer();
        button.interactable = false;
        gameSystemManger.EndTurn();
        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.OpponentPlay + ", switch Turns?????");
        
        
    }
    
}
