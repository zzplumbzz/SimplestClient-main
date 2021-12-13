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
        gameSystemManger.buttonList[gridSpace].GetComponentInChildren<Text>().text = gameSystemManger.currentPlayer;
        // gameSystemManger.GridSpace0.GetComponentInChildren<Text>().text = gameSystemManger.currentPlayer;
        // gameSystemManger.GridSpace1.GetComponentInChildren<Text>().text = gameSystemManger.currentPlayer;
        // gameSystemManger.GridSpace2.GetComponentInChildren<Text>().text = gameSystemManger.currentPlayer;
        // gameSystemManger.GridSpace3.GetComponentInChildren<Text>().text = gameSystemManger.currentPlayer;
        // gameSystemManger.GridSpace4.GetComponentInChildren<Text>().text = gameSystemManger.currentPlayer;
        // gameSystemManger.GridSpace5.GetComponentInChildren<Text>().text = gameSystemManger.currentPlayer;
        // gameSystemManger.GridSpace6.GetComponentInChildren<Text>().text = gameSystemManger.currentPlayer;
        // gameSystemManger.GridSpace7.GetComponentInChildren<Text>().text = gameSystemManger.currentPlayer;
        // gameSystemManger.GridSpace8.GetComponentInChildren<Text>().text = gameSystemManger.currentPlayer;
       // gameSystemManger.buttonList[gridSpace].GetComponentInChildren<Text>().text = gameSystemManger.currentPlayer;
        //gameSystemManger.buttonList[gridSpace].GetComponent<Button>().interactable = false;
        //makes the button pressed not interactable anymore
        // gameSystemManger.GridSpace0.GetComponent<Button>().interactable = false;
        // gameSystemManger.GridSpace1.GetComponent<Button>().interactable = false;
        // gameSystemManger.GridSpace2.GetComponent<Button>().interactable = false;
        // gameSystemManger.GridSpace3.GetComponent<Button>().interactable = false;
        // gameSystemManger.GridSpace4.GetComponent<Button>().interactable = false;
        // gameSystemManger.GridSpace5.GetComponent<Button>().interactable = false;
        // gameSystemManger.GridSpace6.GetComponent<Button>().interactable = false;
        // gameSystemManger.GridSpace7.GetComponent<Button>().interactable = false;
        // gameSystemManger.GridSpace8.GetComponent<Button>().interactable = false;
    

        buttonText.text = gameSystemManger.GetCurrentPlayer();
        button.interactable = false;
        gameSystemManger.EndTurn(gameSystemManger.currentPlayer);
        
        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.PlayerO + "," + gridSpace + "," + gameSystemManger.currentPlayer);
        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.PlayerX + "," + gridSpace + "," + gameSystemManger.currentPlayer);

        
        
    }
    
}
