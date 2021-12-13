using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;

[System.Serializable] public class Player
{
    public Image panel;
    public Text text;
    public Button button;
}

[System.Serializable] public class PlayerColor
{
    public Color PanelColor;
    public Color textColor;
}

public class GameSystemManger : MonoBehaviour
{

    // public Board boardScript;
    // public Box boxScript;

    GameObject submitButton, userNameInput, passwordInput, createToggle, loginToggle;

    GameObject textNameInfo, textPasswordInfo;

    GameObject joinGameRoomButton;

    GameObject networkedClient;

    public GameObject restartButton;

    GameObject quitButton;

    GameObject menuCanvas;

    GameObject board;

    GameObject box;

    GameObject HelloCB;
    GameObject GGCB;

    GameObject ReplayButton;
    public GameObject gameOverPanel;
   public Text gameOverText;

    Text HelloTF;
    Text GoodGameTF;
    MessageScript MS;
//tic tac toe stuff below
    public Text[] buttonList;
    public string currentPlayer;
    GameObject TTTBoard;
    GameObject Grid0;
    GameObject Grid1;
    GameObject Grid2;
    GameObject Grid3;
    public GameObject GridSpace0;
    public GameObject GridSpace1;
    public GameObject GridSpace2;
    public GameObject GridSpace3;
    public GameObject GridSpace4;
    public GameObject GridSpace5;
    public GameObject GridSpace6;
    public GameObject GridSpace7;
    public GameObject GridSpace8;
    public GameObject PlayerX;
    public GameObject PlayerO;
    public GameObject StartInfo;


    private int moveCount;

    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;

    public GameObject startInfo;

    public bool currentClientsTurn;
    

    void Awake()
    {
        SetGameSystemManagerReferanceOnButtons();
        //currentPlayer = "X";
        
        gameOverPanel.SetActive(false);
        moveCount = 0;

        restartButton.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {


        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();


        foreach (GameObject go in allObjects)
        {
            //setting up all game objects in editor
            if (go.name == "UserNameInput")
                userNameInput = go;
            else if (go.name == "PasswordInput")
                passwordInput = go;
            else if (go.name == "SubmitButton")
                submitButton = go;
            else if (go.name == "LoginToggle")
                loginToggle = go;
            else if (go.name == "CreateToggle")
                createToggle = go;
            else if (go.name == "NetworkedClient")
                networkedClient = go;
            else if (go.name == "JoinGameRoomButton")
                joinGameRoomButton = go;
            else if (go.name == "UserName")
                textNameInfo = go;
            else if (go.name == "Password")
                textPasswordInfo = go;
            else if (go.name == "RestartButton")
                restartButton = go;
            else if (go.name == "QuitButton")
                quitButton = go;
            else if (go.name == "MenuCanvas")
                menuCanvas = go;
            // else if (go.name == "Board")
            //     board = go;
            // else if (go.name == "Box")
            //     box = go;
            else if (go.name == "HelloButton")
                HelloCB = go;
            else if (go.name == "GGButton")
                GGCB = go;
            else if (go.name == "ReplayButton")
                ReplayButton = go;
            else if (go.name == "GameOverPanel")
                gameOverPanel = go;
            // else if (go.name == "GameOverText")
            //     gameOverText = tx;
                //ttt board
                else if (go.name == "TTTBoard")
                TTTBoard = go;
                 else if (go.name == "Grid")
                Grid0 = go;
                 else if (go.name == "Grid1")
                Grid1 = go;
                 else if (go.name == "Grid2")
                Grid2 = go;
                 else if (go.name == "Grid3")
                Grid3 = go;
                else if (go.name == "GridSpace")
                GridSpace0 = go;
                else if (go.name == "GridSpace1")
                GridSpace1 = go;
                else if (go.name == "GridSpace2")
                GridSpace2 = go;
                else if (go.name == "GridSpace3")
                GridSpace3 = go;
                else if (go.name == "GridSpace4")
                GridSpace4 = go;
                else if (go.name == "GridSpace5")
                GridSpace5 = go;
                else if (go.name == "GridSpace6")
                GridSpace6 = go;
                else if (go.name == "GridSpace7")
                GridSpace7 = go;
                else if (go.name == "GridSpace8")
                GridSpace8 = go;
                else if (go.name == "PlayerX")
                PlayerX = go;
                else if (go.name == "PlayerO")
                PlayerO = go;
                else if (go.name == "StartInfo")
                StartInfo = go;



        }

        //button listeners
        submitButton.GetComponent<Button>().onClick.AddListener(SubmitButtonPressed);
        loginToggle.GetComponent<Toggle>().onValueChanged.AddListener(LoginToggleChanged);
        createToggle.GetComponent<Toggle>().onValueChanged.AddListener(CreateToggleChanged);
        joinGameRoomButton.GetComponent<Button>().onClick.AddListener(JoinGameRoomButtonPressed);
        restartButton.GetComponent<Button>().onClick.AddListener(RestartButtonPressed);
        quitButton.GetComponent<Button>().onClick.AddListener(QuitButtonPressed);
        HelloCB.GetComponent<Button>().onClick.AddListener(HelloCBPressed);
        GGCB.GetComponent<Button>().onClick.AddListener(GGCBPressed);
        ReplayButton.GetComponent<Button>().onClick.AddListener(ReplayButtonPressed);
        

        ChangeState(GameStates.Login);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetGameSystemManagerReferanceOnButtons()
    {
        for(int i = 0; i < buttonList.Length; i++)
        {
            //buttonList[i].GetComponent<GridSpace>().SetGameSystemManagerReferance(this);
            buttonList[i].GetComponentInParent<GridSpace>().SetGameSystemManagerReferance(this);
        }
    }

    public string GetCurrentPlayer()
    {
        Debug.Log("Getting current player");
        return currentPlayer;
        
    }

    public void EndTurn(string winningPlayer)
    {
        Debug.Log("Turn should End?");
        

        moveCount++;
        //win for rows
       if (buttonList[0].GetComponentInChildren<Text>().text == currentPlayer && buttonList[1].GetComponentInChildren<Text>().text == currentPlayer && buttonList[2].GetComponentInChildren<Text>().text == currentPlayer) 
        {
            networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.GameOver + "," + winningPlayer);
            GameOver(winningPlayer);//winningplayer was currentPlayer
            Debug.Log(winningPlayer + "won with 0, 1, 2!");
        }

        else if (buttonList [3].GetComponentInChildren<Text>().text == currentPlayer && buttonList [4].GetComponentInChildren<Text>().text == currentPlayer && buttonList [5].GetComponentInChildren<Text>().text == currentPlayer) 
        {
            networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.GameOver + "," + winningPlayer);
            GameOver(winningPlayer);//winningplayer was currentPlayer
            Debug.Log(winningPlayer + "won with 3, 4, 5!");
        }

        else if (buttonList [6].GetComponentInChildren<Text>().text == currentPlayer && buttonList [7].GetComponentInChildren<Text>().text == currentPlayer && buttonList [8].GetComponentInChildren<Text>().text == currentPlayer) 
        {
            networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.GameOver + "," + winningPlayer);
            GameOver(winningPlayer);//winningplayer was currentPlayer
            Debug.Log(winningPlayer + "won with 6, 7, 8!");
        }

        //win for columns
        else if (buttonList [0].GetComponentInChildren<Text>().text == currentPlayer && buttonList [3].GetComponentInChildren<Text>().text == currentPlayer && buttonList [6].GetComponentInChildren<Text>().text == currentPlayer) 
        {
            networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.GameOver + "," + winningPlayer);
            GameOver(winningPlayer);//winningplayer was currentPlayer
            Debug.Log(winningPlayer + "won with 0, 3, 6!");
        }

        else if (buttonList [1].GetComponentInChildren<Text>().text == currentPlayer && buttonList [4].GetComponentInChildren<Text>().text == currentPlayer && buttonList [7].GetComponentInChildren<Text>().text == currentPlayer) 
        {
            networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.GameOver + "," + winningPlayer);
            GameOver(winningPlayer);//winningplayer was currentPlayer
            Debug.Log(winningPlayer + "won with 1, 4, 7!");
        }

        else if (buttonList [2].GetComponentInChildren<Text>().text == currentPlayer && buttonList [5].GetComponentInChildren<Text>().text == currentPlayer && buttonList [8].GetComponentInChildren<Text>().text == currentPlayer) 
        {
            networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.GameOver + "," + winningPlayer);
            GameOver(winningPlayer);//winningplayer was currentPlayer
            Debug.Log(winningPlayer + "won with 2, 5, 8!");
        }
        // win for diagonals
        else if (buttonList [0].GetComponentInChildren<Text>().text == currentPlayer && buttonList [4].GetComponentInChildren<Text>().text == currentPlayer && buttonList [8].GetComponentInChildren<Text>().text == currentPlayer) 
        {
            networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.GameOver + "," + winningPlayer);
            GameOver(winningPlayer);//winningplayer was currentPlayer
            Debug.Log(winningPlayer + "won with 0, 4, 8!");
        }

        else if (buttonList [2].GetComponentInChildren<Text>().text == currentPlayer && buttonList [4].GetComponentInChildren<Text>().text == currentPlayer && buttonList [6].GetComponentInChildren<Text>().text == currentPlayer) 
        {
            networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.GameOver + "," + winningPlayer);
            GameOver(winningPlayer);//winningplayer was currentPlayer
            Debug.Log(winningPlayer + "won with 2, 4, 6!");
        }

        else if(moveCount >= 9)
        {
            
            GameOver("draw");
            //Debug.Log(GameOver + "Draw");
        }
       
            
           
        ChangeSides();
        Debug.Log("Change sides?? at the end of end turn!!!!!!!");
       
    }

    void ChangeSides()
    {
        Debug.Log("Change sides?? function start");
        //currentPlayer = (currentPlayer == "X") ? "O" : "X";
        if(currentPlayer == "X")
        {
            Debug.Log("Player X TURN");
            SetPlayerColors(playerX, playerO);
            networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.PlayerX + "," + currentPlayer + playerX);
            networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ServerToClientSignifiers.PlayerX + "," + currentPlayer + playerX);
            networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.PlayerX + "Player X turn?");
            if(currentPlayer != "X")
            {
            SetBoardInteractable(false);
            }
        }
        else if(currentPlayer == "O")
        {
            Debug.Log("Player O TURN");
            SetPlayerColors(playerO, playerX);
            networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.PlayerO + "," + currentPlayer + playerO);
            networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ServerToClientSignifiers.PlayerO + "," + currentPlayer + playerO);
            if(currentPlayer != "O")
            {
            SetBoardInteractable(false);
            }
        }
    }

    public void UpdateGridSpace(int gridSpace, string currentPlayer)
    {
        Debug.Log("Update grid space start of function!!!");
        //updates text for each button pressed
        buttonList[gridSpace].GetComponentInChildren<Text>().text = currentPlayer;
        // GridSpace0.GetComponentInChildren<Text>().text = currentPlayer;
        // GridSpace1.GetComponentInChildren<Text>().text = currentPlayer;
        // GridSpace2.GetComponentInChildren<Text>().text = currentPlayer;
        // GridSpace3.GetComponentInChildren<Text>().text = currentPlayer;
        // GridSpace4.GetComponentInChildren<Text>().text = currentPlayer;
        // GridSpace5.GetComponentInChildren<Text>().text = currentPlayer;
        // GridSpace6.GetComponentInChildren<Text>().text = currentPlayer;
        // GridSpace7.GetComponentInChildren<Text>().text = currentPlayer;
        // GridSpace8.GetComponentInChildren<Text>().text = currentPlayer;
        buttonList[gridSpace].GetComponent<Button>().interactable = false;
        //makes the button pressed not interactable anymore
        // GridSpace0.GetComponent<Button>().interactable = false;
        // GridSpace1.GetComponent<Button>().interactable = false;
        // GridSpace2.GetComponent<Button>().interactable = false;
        // GridSpace3.GetComponent<Button>().interactable = false;
        // GridSpace4.GetComponent<Button>().interactable = false;
        // GridSpace5.GetComponent<Button>().interactable = false;
        // GridSpace6.GetComponent<Button>().interactable = false;
        // GridSpace7.GetComponent<Button>().interactable = false;
        // GridSpace8.GetComponent<Button>().interactable = false;
        Debug.Log("Update grid space ending turn!");
        EndTurn(currentPlayer);
    }

    void SetPlayerColors(Player newPlayer, Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.PanelColor; 
        newPlayer.text.color = activePlayerColor.textColor; 
        oldPlayer.panel.color = inactivePlayerColor.PanelColor; 
        oldPlayer.text.color = inactivePlayerColor.textColor;

    }

    public void GameOver(string winningPlayer)
    {
      for (int i = 0; i < buttonList.Length; i++)
      {
          SetBoardInteractable(false);
      }
      gameOverPanel.SetActive(true);
        //SetBoardInteractable(false);
        if(winningPlayer == "draw")
        {
            
            SetGameOverText("Its A Draw!");
            SetPlayerColorsInactive();
            
        }
        else
        {
            SetGameOverText(winningPlayer + " Wins!");
           // networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ServerToClientSignifiers.Win + ",Wins");
        }
        //SetGameOverText(currentPlayer + " Wins!");
        //Debug.Log("SomeOne won!");

        restartButton.SetActive(true);
        //networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ServerToClientSignifiers.GameStart + ",RestartGame");
    }

    void SetGameOverText(string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }

    public void RestartGame()
    {
        //currentPlayer = "X";
        moveCount = 0;
        gameOverPanel.SetActive(false);
        //restartButton.SetActive(false);
        //SetPlayerButtons(true);
        //startInfo.SetActive(true);
        SetPlayerColors(playerX, playerO);
        //SetPlayerColors(playerX, playerO);
        SetBoardInteractable(true);
        for(int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = "";
        }

        //SetPlayerColorsInactive();
        //networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ServerToClientSignifiers.GameStart + ",RestartGame");
        // for (int i = 0; i < buttonList.Length; i++)
        // {
        //     //buttonList[i].GetComponentInParent<Button>().interactable = true;
        //     buttonList [i].text = ""; 
        // }

        
    }

    void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }

    public void SetStartingSide (string startingPlayer) 
    { 
        currentPlayer = startingPlayer; 
        //networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ServerToClientSignifiers.PlayerTurn + "starting player");
        if (currentPlayer == "X") 
        {
            SetPlayerColors(playerX, playerO); 
            networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.PlayerX + "Player X turn?");
            //PlayerO = SetBoardInteractable(false);
        } 
        else if(currentPlayer == "O")
        {
            SetPlayerColors(playerO, playerX); 
            networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.PlayerO + "player O turn?");
        }

        StartGame();
    }

    void StartGame()
    {
        SetBoardInteractable(true);
        //SetPlayerButtons(false);
        //startInfo.SetActive(false);
        
    }

    bool CurrentClientsTurn(bool ClientsTurn)
    {
        if(ClientsTurn)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // void SetPlayerButtons (bool toggle) 
    // {
    //     playerX.button.interactable = toggle; 
    //     playerO.button.interactable = toggle;
    //     if(playerX.button.interactable == true)//was = toggle
    //     {
    //         //networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ServerToClientSignifiers.PlayerTurn + "first players turn?");
    //         playerO.button.interactable = false;
    //     }
    //     else if(playerO.button.interactable == true)//was = toggle
    //     {
    //         networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ServerToClientSignifiers.OpponentPlay + "oppenents turn?");
    //         playerX.button.interactable = false;
    //     }
        
        
    //    // SetPlayerButtons (false);
    // }

    void SetPlayerColorsInactive()
    {
        playerX.panel.color = inactivePlayerColor.PanelColor; 
        playerX.text.color = inactivePlayerColor.textColor; 
        playerO.panel.color = inactivePlayerColor.PanelColor; 
        playerO.text.color = inactivePlayerColor.textColor;

        
    }

    public void SubmitButtonPressed()
    {
        Debug.Log("Submit button pressed");

        string p = passwordInput.GetComponent<InputField>().text;
        string n = userNameInput.GetComponent<InputField>().text;

        string msg;

        if (createToggle.GetComponent<Toggle>().isOn)
            msg = ClientToServerSignifiers.CreateAccount + "," + n + "," + p;
        else
            msg = ClientToServerSignifiers.Login + "," + n + "," + p;

        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(msg);

        Debug.Log(msg);

    }

    private void LoginToggleChanged(bool newValue)// setting toggle on press and disabling the other toggle
    {
        Debug.Log("Login Toggle");

        createToggle.GetComponent<Toggle>().SetIsOnWithoutNotify(!newValue);

    }

    private void CreateToggleChanged(bool newValue)
    {
        Debug.Log("create Toggle");
        loginToggle.GetComponent<Toggle>().SetIsOnWithoutNotify(!newValue);


    }

    private void JoinGameRoomButtonPressed()
    {
        Debug.Log("Joining Queue button pressed");
        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.JoinQueueForGameRoom + "");

        ChangeState(GameStates.WaitingForMatch);

    }
    private void RestartButtonPressed()
    {
        // Debug.Log(",Tic tac toe button pressed");
        // networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.TicTacToePlay + "");
        // ChangeState(GameStates.TicTacToe);


    }

    public void QuitButtonPressed()// quits game back to login screen
    {

        ChangeState(GameStates.Login);
    }
    public void HelloCBPressed()// when button pressed send to server to send to other client
    {
        //string[] csv = msg.Split(',');

        // int signifier = int.Parse(csv[0]);

        Debug.Log("Hello");
        //networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.HelloButtonPressed + ",Chazz says hi?");
        //networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ServerToClientSignifiers.SendHelloButtonPressed + ",Hi ChadS");
    }

    public void GGCBPressed()// when button pressed send to server to send to other client
    {
    
        //GoodGameTF.text = text;
        //networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.GGButtonPressed + ",Good Game host");
        //networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ServerToClientSignifiers.SendGGButtonPressed + ",GoogGame");
        Debug.Log("Good Game!");


    }

    public void ReplayButtonPressed()// supposed to replay the game when complete
    {

    }

    public void ChangeState(int newState)// changing the different states
    {
        joinGameRoomButton.SetActive(false);
        submitButton.SetActive(false);
        userNameInput.SetActive(false);
        passwordInput.SetActive(false);
        createToggle.SetActive(false);
        loginToggle.SetActive(false);
        textNameInfo.SetActive(false);
        textPasswordInfo.SetActive(false);
        joinGameRoomButton.SetActive(false);
        restartButton.SetActive(false);
        quitButton.SetActive(false);
        menuCanvas.SetActive(false);
        //board.SetActive(false);
       // box.SetActive(false);
        HelloCB.SetActive(false);
        GGCB.SetActive(false);
        ReplayButton.SetActive(false);
        gameOverPanel.SetActive(false);
        //gameOverText.SetActive(false);
        //TTTBoard stuff
        TTTBoard.SetActive(false);
        Grid0.SetActive(false);
        Grid1.SetActive(false);
        Grid2.SetActive(false);
        Grid3.SetActive(false);
        GridSpace0.SetActive(false);
        GridSpace1.SetActive(false);
        GridSpace2.SetActive(false);
        GridSpace3.SetActive(false);
        GridSpace4.SetActive(false);
        GridSpace5.SetActive(false);
        GridSpace6.SetActive(false);
        GridSpace7.SetActive(false);
        GridSpace8.SetActive(false);
        PlayerX.SetActive(false);
        PlayerO.SetActive(false);
        StartInfo.SetActive(false);



        if (newState == GameStates.Login)
        {
            Debug.Log("Login menu state");

            submitButton.SetActive(true);
            userNameInput.SetActive(true);
            passwordInput.SetActive(true);
            createToggle.SetActive(true);
            loginToggle.SetActive(true);
            menuCanvas.SetActive(true);
            textNameInfo.SetActive(true);
            textPasswordInfo.SetActive(true);

             //board.SetActive(false);
        //box.SetActive(false);
        HelloCB.SetActive(false);
        GGCB.SetActive(false);
        ReplayButton.SetActive(false);
        gameOverPanel.SetActive(false);
        //gameOverText.SetActive(false);

            TTTBoard.SetActive(false);
        Grid0.SetActive(false);
        Grid1.SetActive(false);
        Grid2.SetActive(false);
        Grid3.SetActive(false);
        GridSpace0.SetActive(false);
        GridSpace1.SetActive(false);
        GridSpace2.SetActive(false);
        GridSpace3.SetActive(false);
        GridSpace4.SetActive(false);
        GridSpace5.SetActive(false);
        GridSpace6.SetActive(false);
        GridSpace7.SetActive(false);
        GridSpace8.SetActive(false);

        }
        else if (newState == GameStates.MainMenu)
        {
            Debug.Log("Main menu state");
            quitButton.SetActive(true);
            joinGameRoomButton.SetActive(true);
            menuCanvas.SetActive(true);
        }
        else if (newState == GameStates.WaitingForMatch)
        {

            Debug.Log("In Queue state");
            quitButton.SetActive(true);





        }
        else if (newState == GameStates.TicTacToe)
        {

            Debug.Log("In Game state");
            //ticTacToeSquareButton.SetActive(false);
            quitButton.SetActive(true);
            GGCB.SetActive(true);
            HelloCB.SetActive(true);

            TTTBoard.SetActive(true);
            Grid0.SetActive(true);
            Grid1.SetActive(true);
            Grid2.SetActive(true);
            Grid3.SetActive(true);
            GridSpace0.SetActive(true);
            GridSpace1.SetActive(true);
            GridSpace2.SetActive(true);
            GridSpace3.SetActive(true);
            GridSpace4.SetActive(true);
            GridSpace5.SetActive(true);
            GridSpace6.SetActive(true);
            GridSpace7.SetActive(true);
            GridSpace8.SetActive(true);
            PlayerX.SetActive(true);
            PlayerO.SetActive(true);
            StartInfo.SetActive(true);


            joinGameRoomButton.SetActive(false);
            submitButton.SetActive(false);
            userNameInput.SetActive(false);
            passwordInput.SetActive(false);
            createToggle.SetActive(false);
            loginToggle.SetActive(false);

            textNameInfo.SetActive(false);
            textPasswordInfo.SetActive(false);

            currentPlayer = networkedClient.GetComponent<NetworkedClient>().GetCurrentPlayer();
            SetPlayerColors(playerX, playerO);
            StartGame();
        }

        // else if (newState == GameStates.OpponentPlay)
        // {
        //     Debug.Log("opponent play state");
        //     //ticTacToeSquareButton.SetActive(false);
        //     joinGameRoomButton.SetActive(false);
        //     quitButton.SetActive(true);
        //    // board.SetActive(true);
        //     //box.SetActive(true);
        //     GGCB.SetActive(true);
        //     HelloCB.SetActive(true);



        // }

        // else if (newState == GameStates.Win)
        // {

        //     quitButton.SetActive(true);
        //    // board.SetActive(false);
        //     //box.SetActive(false);
        //     GGCB.SetActive(true);
        //     HelloCB.SetActive(true);
        //     ReplayButton.SetActive(true);
        //     gameOverPanel.SetActive(true);
        //     //gameOverText.SetActive(true);



        //     joinGameRoomButton.SetActive(false);
        //     submitButton.SetActive(false);
        //     userNameInput.SetActive(false);
        //     passwordInput.SetActive(false);
        //     createToggle.SetActive(false);
        //     loginToggle.SetActive(false);

        //     textNameInfo.SetActive(false);
        //     textPasswordInfo.SetActive(false);

        //     joinGameRoomButton.SetActive(false);
        //     //ticTacToeSquareButton.SetActive(false);
        // }

    }
}










static public class GameStates
{
    public const int Login = 1;
    public const int MainMenu = 2;
    public const int WaitingForMatch = 3;
    public const int TicTacToe = 4;
    public const int OpponentPlay = 5;
    public const int Win = 6;
}
