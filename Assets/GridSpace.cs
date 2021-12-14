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

    // GameObject GridSpace0;
    // GameObject GridSpace1;
    // GameObject GridSpace2;
    // GameObject GridSpace3;
    // GameObject GridSpace4;
    // GameObject GridSpace5;
    // GameObject GridSpace6;
    // GameObject GridSpace7;
    // GameObject GridSpace8;

    public void SetGameSystemManagerReferance(GameSystemManger manager)
    {
        gameSystemManger = manager;
    }

    public void SetSpace(int gridSpace)
    {
        if(gameSystemManger.currentPlayer == "X")
        {
             Debug.Log("PLAYER ID" + gameSystemManger.playerID1);
             gameSystemManger.buttonList[gridSpace].GetComponentInChildren<Text>().text = gameSystemManger.currentPlayer;
             //gameSystemManger.buttonList[gridSpace].GetComponent<Button>().interactable = false;
            buttonText.text = gameSystemManger.GetCurrentPlayer();
            button.interactable = false;
            gameSystemManger.ChangeSides();
            gameSystemManger.EndTurn(gameSystemManger.currentPlayer);
            networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ServerToClientSignifiers.OpponentPlay + ", Should be PLAYER O's TURN NOW");
             

          // gameSystemManger.currentPlayer = "O";
           //gameSystemManger.currentPlayer = gameSystemManger.playerID2;

           if(gameSystemManger.currentPlayer == "O")
           {
               gameSystemManger.SetBoardInteractable(true);
               gameSystemManger.currentPlayer = "X";
           }
           else  if(gameSystemManger.currentPlayer == "X")
           {
               gameSystemManger.currentPlayer = "O";
               gameSystemManger.SetBoardInteractable(false);
           }
        }
        else if(gameSystemManger.currentPlayer == "O")
        {
             Debug.Log("PLAYER ID" + gameSystemManger.playerID1);
             gameSystemManger.buttonList[gridSpace].GetComponentInChildren<Text>().text = gameSystemManger.currentPlayer;
            buttonText.text = gameSystemManger.GetCurrentPlayer();
            button.interactable = false;
            gameSystemManger.ChangeSides();
            gameSystemManger.EndTurn(gameSystemManger.currentPlayer);

          // gameSystemManger.currentPlayer = "O";
           //gameSystemManger.currentPlayer = gameSystemManger.playerID2;
        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.OpponentPlay + ", Should be PLAYER X's TURN NOW");
           if(gameSystemManger.currentPlayer == "O")
           {
               gameSystemManger.currentPlayer = "X";
               gameSystemManger.SetBoardInteractable(false);
           }
           else  if(gameSystemManger.currentPlayer == "X")
           {
               gameSystemManger.currentPlayer = "O";
               gameSystemManger.SetBoardInteractable(true);
           }
        }
        
        
        
        
        
    }
    
}
