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
    public bool on = true;

    void Start()
    {
        on = true;
    }

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
            on = false;
            buttonText.text = gameSystemManger.GetCurrentPlayer();
            gameSystemManger.EndTurn(gameSystemManger.currentPlayer);
            networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.TicTacToePlay + "," + currentGrid);
            
            gameSystemManger.SetBoardInteractable(false);

           if (gameSystemManger.currentPlayer == "O")
           {
               
               gameSystemManger.currentPlayer = "X";
           }
           else  if(gameSystemManger.currentPlayer == "X")
           {
               gameSystemManger.currentPlayer = "O";
           }
        }
        else if(gameSystemManger.currentPlayer == "O")
        {
            on = false;
            buttonText.text = gameSystemManger.GetCurrentPlayer();
            gameSystemManger.EndTurn(gameSystemManger.currentPlayer);
            networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.TicTacToePlay + "," + currentGrid);
            gameSystemManger.SetBoardInteractable(false);
    
            if (gameSystemManger.currentPlayer == "O")
           {
               gameSystemManger.currentPlayer = "X";
               
           }
           else  if(gameSystemManger.currentPlayer == "X")
           {
               gameSystemManger.currentPlayer = "O";
           }
        }
   
    }
}
    