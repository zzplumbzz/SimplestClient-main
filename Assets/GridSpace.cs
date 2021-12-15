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
    public GameObject networkedClient;
    public int currentGrid = 0;
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

        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            if (go.name == "NetworkedClient")
            {
                networkedClient = go;
            }   
        }

    }

    public void SetSpace(int gridSpace)
    {
        if(gameSystemManger.currentPlayer == "X")
        {
            buttonText.text = gameSystemManger.GetCurrentPlayer();
            button.interactable = false;
            gameSystemManger.EndTurn(gameSystemManger.currentPlayer);
            networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.TicTacToePlay + "," + currentGrid);

           if (gameSystemManger.currentPlayer == "O")
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
            buttonText.text = gameSystemManger.GetCurrentPlayer();
            button.interactable = false;
            gameSystemManger.EndTurn(gameSystemManger.currentPlayer);
           networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.TicTacToePlay + "," + currentGrid);
    
            if (gameSystemManger.currentPlayer == "O")
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
    