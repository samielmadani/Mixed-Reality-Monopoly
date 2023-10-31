using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;
using UnityEngine.SceneManagement;
using Photon.Realtime;


public class PlayerSetup : MonoBehaviour
{

    // Used to see what players are in game
    List<bool> isInGame = new List<bool> {true, true, true, true};

    // Position dice roll options
    List<string> diceRollStrings = new List<string> {"2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"};

    List<string> buttonStrings = new List<string> {"ChooseDog", "ChooseCar", "ChooseBin", "ChooseBoat"};

    // Shows whos turn it is
    public TMP_Text turnMessage;

    // Dictates which players turn
    public static int currentPlayerIndex = 1;

    // End turn button
    public Button endTurn;

    // Shows roll dice panel
    public GameObject diceUI;

    // Shows dice roll title
    public TMP_Text diceNumberHeader;

    // Shows dice roll number
    public TMP_Text diceNumber;

    // Shows dice roll button
    public RawImage diceButton;

    // Holds player money value
    public int playerMoney;

    // Which player this device is playing as
    public static int pieceValue;

    // Shows player money
    [SerializeField]
    private Text Money;

    // Shows buy/sell property panel image
    [SerializeField]
    private Image PropertyPanel;

    // shows buy button
    [SerializeField]
    private Button BuyButton;

    // shows sell button
    [SerializeField]
    private Button SellButton;

    // PhotonView, used from PhotonPun2 and is used to connect all players together to the same lobby
    private PhotonView PV;

    // Main game feedback message text
    public TMP_Text displayMessage;

    // Chance/chest card options
    Dictionary<int, string> chanceCards = new Dictionary<int, string>();

    // Tile landed on
    public MonopolySpace tileToUse;

    // Player has scanned tile and made their turn
    public static bool OneChangeMade = false;

    // Shows property panel
    public PropertyPanel propertyPanel;

    public string meCurrent = "";

    public static string PropertyToBuySell = "";

    void Start()
    {
        // Initialise PhotonView
        PV = GetComponent<PhotonView>();

        // Add key-value pairs to the dictionary chance/chest
        chanceCards.Add(1, "Advance to Go. Collect $200.");
        chanceCards.Add(2, "Pay hospital fees of $100.");
        chanceCards.Add(3, "Bank error in your favor. Collect $75.");
        chanceCards.Add(4, "You have won a crossword competition. Collect $50.");
        chanceCards.Add(5, "Your building and loan matures. Collect $150.");
        chanceCards.Add(6, "Your building caught fire. Pay repair costs $200.");
        chanceCards.Add(7, "Falsely accused, almost ended up in jail. Collect $200 in compensation.");
        chanceCards.Add(8, "Pay school fees of $50.");
        chanceCards.Add(9, "Income tax refund. Collect $20.");
        chanceCards.Add(10, "Receive $25 consultancy fee.");
        chanceCards.Add(11, "Doctor's fees. Pay $50.");
        chanceCards.Add(12, "It is your birthday. Collect $100 from the bank.");
        chanceCards.Add(13, "Grand Opera Night. Collect $150 for opening night seats. The bank will cover the cost.");
        chanceCards.Add(14, "Life insurance matures. Collect $100.");
        chanceCards.Add(15, "You inherit $100.");
    }

    // If a player lands on tile and it is that players turn and they havnt made their turn yet and they are scanning the right piece. 
    // Then call the RPC function to dictate what tile they are on
    void OnTriggerEnter(Collider other)
    {       
        if (OneChangeMade == false && !diceButton.gameObject.activeInHierarchy) {

            if (gameObject.name == currentPlayerIndex.ToString()) {

                if (gameObject.name == pieceValue.ToString()) {
            
                    switch (other.gameObject.tag)
                    {                       
                        case "go":
                            PV.RPC("LandedOnGo", RpcTarget.AllBuffered, other.gameObject.name);
                            break;
                        case "chance":
                        case "chest":   
                            PV.RPC("LandedOnChestOrChance", RpcTarget.AllBuffered, other.gameObject.name);
                            break;
                        case "utility":
                        case "railroad":
                        case "property":
                            LandedOnProperty(other.gameObject.name, pieceValue, gameObject.name);
                            break;
                        case "tax":
                            PV.RPC("LandedOnTax", RpcTarget.AllBuffered, other.gameObject.name);
                            break;
                        case "jail":
                            PV.RPC("LandedOnJail", RpcTarget.AllBuffered, other.gameObject.name);
                            break;
                        case "parking":
                            PV.RPC("LandedOnParking", RpcTarget.AllBuffered, other.gameObject.name);
                            break;
                        default:
                            // Handle collisions with other tags if needed
                            break;
                    }
                }
            }
        }  
    }

    // Runs continuously to remove or show correct panels if they have been missed by an RPC call and check if there is only one player left
    void Update()
    {
        if (currentPlayerIndex != pieceValue) {
            if (endTurn != null)
            {
                endTurn.gameObject.SetActive(false);
            }
            if (diceButton != null)
            {            
            diceButton.gameObject.SetActive(false);
            }
        } else{
            if (endTurn != null && !diceButton.gameObject.activeInHierarchy && OneChangeMade == true && playerMoney > 0)
            {
            endTurn.gameObject.SetActive(true);
            }
            if (diceNumberHeader != null)
            {
            diceNumberHeader.gameObject.SetActive(true);
            }
            if (PiecePicker.clicked == false) {
                if (diceButton != null)
                {
                diceButton.gameObject.SetActive(true);
                }
            } else {
                if (diceButton != null)
                {
                diceButton.gameObject.SetActive(false);
                }
            }

        }

        if (isInGame.Count(b => b == true) <= 1) {
            displayMessage.text = "You have the MONOPOLY!";
            displayMessage.gameObject.SetActive(true);
            StartCoroutine(ElimiWait());
        }
    }

    // Reset dice roll

    [PunRPC]
    void changeString(string randoString) {
        diceNumber.text = randoString;
    }

    // show dice roll button etc

    [PunRPC]
    void rolling() {
        diceUI.gameObject.SetActive(true);
        diceNumber.gameObject.SetActive(true);
        diceNumberHeader.gameObject.SetActive(true);

    }

    // Deducts money depending on which of the tax tiles the user landed on and checks money is above zero

    [PunRPC]
    void LandedOnTax(string gameObjectName)
    {
        if (gameObjectName == "incometax")
        {
            playerMoney -= 40;
            Money.text = playerMoney.ToString();
            displayMessage.text = "A tax of $40 has been deducted.";
            displayMessage.gameObject.SetActive(true);
        }

        if (gameObjectName == "supertax")
        {
            playerMoney -= 80;
            Money.text = playerMoney.ToString();
            displayMessage.text = "A tax of $80 has been deducted.";
            displayMessage.gameObject.SetActive(true);
            
        }

        OneChangeMade = true;


        if (PV.IsMine)
        {

            if (playerMoney <= 0) {
                PV.RPC("Elimination", RpcTarget.AllBuffered, currentPlayerIndex);
                StartCoroutine(ElimiWait());

            } else {
                StartCoroutine(WaitTime());
            }

        }
        
       

    }  

    // If player clicks leave lobby button remove them and disconnect them from the lobby

    public void DisconnectPlayer()
    {
        StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;
        SceneManager.LoadScene(2);
    }

        

    // Adds money and shows message

    [PunRPC]
    void LandedOnGo(string gameObjectName)
    {
        if (gameObjectName == "startspot") {
            playerMoney += 200;
            Money.text = playerMoney.ToString();
            displayMessage.text = "$200 have been deposited.";
            displayMessage.gameObject.SetActive(true);
            StartCoroutine(WaitTime());

        }

        OneChangeMade = true;
    }



    // Adds or Deducts money depending on which of the chest/chance cards are returned and checks money is above zero

    [PunRPC]
    void LandedOnChestOrChance(string gameObjectName)
    {   
        // Generate a random number to simulate drawing a card
        System.Random random = new System.Random();
        int drawnCard = random.Next(1, chanceCards.Count + 1);

        // Get the card's message based on the drawn number
        string cardMessage = chanceCards[drawnCard];

        // Process the card's effect on playerMoney and displayText
        switch (drawnCard)
        {
            case 1:
                playerMoney += 200;
                break;
            case 2:
                playerMoney -= 100;
                break;
            case 3:
                playerMoney += 75;
                break;
            case 4:
                playerMoney += 50;
                break;
            case 5:
                playerMoney += 150;
                break;
            case 6:
                playerMoney -= 200;
                break;
            case 7:
                playerMoney += 200;
                break;
            case 8:
                playerMoney -= 50;
                break;
            case 9:
                playerMoney += 20;
                break;
            case 10:
                playerMoney += 25;
                break;
            case 11:
                playerMoney -= 50;
                break;
            case 12:
                playerMoney += 100;
                break;
            case 13:
                playerMoney += 150;
                break;
            case 14:
                playerMoney += 100;
                break;
            case 15:
                playerMoney += 100;
                break;
        }

        displayMessage.text = cardMessage;
        displayMessage.gameObject.SetActive(true);
        Money.text = playerMoney.ToString();

        OneChangeMade = true;


        if (playerMoney <= 0) {
            PV.RPC("Elimination", RpcTarget.AllBuffered, currentPlayerIndex);
            StartCoroutine(ElimiWait());
        } else {
            StartCoroutine(WaitTime());
        }

    }
    

    // If property is owned, rent is deducted if owned by someone else. If owned by current player sell option appears to sell for half buy price.
    // If it is not owned buy option appears which only works if user has enough money
    // Checks money is above zero
    void LandedOnProperty(string gameObjectName, int playerID, string meCur)
    {
        meCurrent = meCur;

        int index = MonopolyGameManager.boardSpaces.First(kv => kv.Value.name == gameObjectName).Key;

        var tile = MonopolyGameManager.boardSpaces[index];

        tileToUse = MonopolyGameManager.boardSpaces[index];

        print(pieceValue);

        print(playerID);

        propertyPanel.PanelName.text = tileToUse.name;
        switch (tileToUse.owner)
            {
                case 1:
                    propertyPanel.PanelOwner.text = "Owner: DOG";
                    break;
                case 2:
                    propertyPanel.PanelOwner.text = "Owner: CAR";
                    break;
                case 3:
                    propertyPanel.PanelOwner.text = "Owner: BIN";
                    break;
                case 4:
                    propertyPanel.PanelOwner.text = "Owner: BOAT";
                    break;
                case -1:
                    propertyPanel.PanelOwner.text = "No Owner";
                    break;
            }


        if (tile.owner != -1) {

            if (tile.owner != playerID) {
                PV.RPC("PayRent", RpcTarget.AllBuffered, tile.rent);

            } else if (tile.owner == playerID) {
                propertyPanel.BuyerSeller = currentPlayerIndex;
                PropertyPanel.gameObject.SetActive(true);
                SellButton.gameObject.SetActive(true);
            }
            
        } else {
            propertyPanel.BuyerSeller = currentPlayerIndex;
            PropertyPanel.gameObject.SetActive(true);
            BuyButton.gameObject.SetActive(true);
        }

        OneChangeMade = true;

        if (playerMoney <= 0) {
            PV.RPC("Elimination", RpcTarget.AllBuffered, currentPlayerIndex);
            StartCoroutine(ElimiWait());
        }
            
    }

    // Shows everyone money has been paid

    [PunRPC]
    void PayRent(int rent) {
        playerMoney -= rent;
        Money.text = playerMoney.ToString();
        displayMessage.text = "$" + rent + " rent has been paid.";
        displayMessage.gameObject.SetActive(true);
        StartCoroutine(WaitTime());
    }

    // Buys property and changes owner for everyone

    [PunRPC]
    public void Buy(PlayerSetup playerSetup) {

        if (playerSetup.meCurrent == currentPlayerIndex.ToString()) {

            int index = MonopolyGameManager.boardSpaces.First(kv => kv.Value.name == PropertyToBuySell).Key;

            var tile = MonopolyGameManager.boardSpaces[index];

            print(tile.name);

            if (playerSetup.playerMoney >= tile.purchasePrice) {

                switch (playerSetup.meCurrent)
                {
                    case "1":
                        playerSetup.PV.RPC("displayBuy", RpcTarget.AllBuffered, "DOG has purchased " + tile.name, tile.purchasePrice, index);
                        break;
                    case "2":
                        playerSetup.PV.RPC("displayBuy", RpcTarget.AllBuffered, "CAR has purchased " + tile.name, tile.purchasePrice, index);
                        break;
                    case "3":
                        playerSetup.PV.RPC("displayBuy", RpcTarget.AllBuffered, "BIN has purchased " + tile.name, tile.purchasePrice, index);
                        break;
                    case "4":
                        playerSetup.PV.RPC("displayBuy", RpcTarget.AllBuffered, "BOAT has purchased " + tile.name, tile.purchasePrice, index);
                        break;
                }

                playerSetup.PropertyPanel.gameObject.SetActive(false);
            } else {
                playerSetup.displayMessage.text = "You do not have enough money to buy this property.";
                playerSetup.PropertyPanel.gameObject.SetActive(false);
                playerSetup.displayMessage.gameObject.SetActive(true);
                playerSetup.StartCoroutine(BuySellWaitTime(playerSetup));
            }

        }

    }

    // display bought message

    [PunRPC]
    void displayBuy(string tileName, int tilePurchasePrice, int index) {
        playerMoney -= tilePurchasePrice;
        Money.text = playerMoney.ToString();
        MonopolyGameManager.boardSpaces[index].owner = currentPlayerIndex;
        displayMessage.text = tileName;
        displayMessage.gameObject.SetActive(true);
        StartCoroutine(WaitTime());
    }

    // Sells property and changes owner back to none for everyone

    [PunRPC]
    public void Sell(PlayerSetup playerSetup) {

        if (playerSetup.meCurrent == currentPlayerIndex.ToString()) {

            int index = MonopolyGameManager.boardSpaces.First(kv => kv.Value.name == PropertyToBuySell).Key;

            var tile = MonopolyGameManager.boardSpaces[index];

                switch (playerSetup.meCurrent)
                {
                    case "1":
                        playerSetup.PV.RPC("displaySell", RpcTarget.AllBuffered, "DOG has sold " + tile.name, tile.purchasePrice, index);
                        break;
                    case "2":
                        playerSetup.PV.RPC("displaySell", RpcTarget.AllBuffered, "CAR has sold " + tile.name, tile.purchasePrice, index);
                        break;
                    case "3":
                        playerSetup.PV.RPC("displaySell", RpcTarget.AllBuffered, "BIN has sold " + tile.name, tile.purchasePrice, index);
                        break;
                    case "4":
                        playerSetup.PV.RPC("displaySell", RpcTarget.AllBuffered, "BOAT has sold " + tile.name, tile.purchasePrice, index);
                        break;
                }

                playerSetup.PropertyPanel.gameObject.SetActive(false);

        }
        
    }

    // display sold message


    [PunRPC]
    void displaySell(string tileName, int tilePurchasePrice, int index) {
        playerMoney += tilePurchasePrice / 2;
        Money.text = playerMoney.ToString();
        MonopolyGameManager.boardSpaces[index].owner = -1;
        displayMessage.text = tileName;
        displayMessage.gameObject.SetActive(true);
        StartCoroutine(WaitTime());
    }

    // If landed on GOTO JAIL user money reduced by 50 and is told to move their piece to JAIL and start from there next turn.
    // If they land on JAIL they are just visiting and turn ends



    [PunRPC]
    void LandedOnJail(string gameObjectName)
    {


        if (gameObjectName == "gotojail") {
            playerMoney -= 50;
            Money.text = playerMoney.ToString();
            displayMessage.text = "Caught by the cops! Move your piece to JAIL. You have paid $50 bail.";
            displayMessage.gameObject.SetActive(true);
        } else if (gameObjectName == "jail") {
            displayMessage.text = "Just visiting. Do not panic.";
            displayMessage.gameObject.SetActive(true);
        }

        OneChangeMade = true;

        if  (playerMoney <= 0) {
            PV.RPC("Elimination", RpcTarget.AllBuffered, currentPlayerIndex);
            StartCoroutine(ElimiWait());
        } else {
            StartCoroutine(WaitTime());
        }        

    }

    // Money is added


    [PunRPC]
    void LandedOnParking(string gameObjectName)
    {
        playerMoney += 300;
        Money.text = playerMoney.ToString();
        displayMessage.text = "You collected $300 worth of parking.";
        displayMessage.gameObject.SetActive(true);

        OneChangeMade = true;
        StartCoroutine(WaitTime());

    }

    // Used to hide all messages after period of time


    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(5f);

        PropertyPanel.gameObject.SetActive(false);
        BuyButton.gameObject.SetActive(false);
        SellButton.gameObject.SetActive(false);
        displayMessage.gameObject.SetActive(false);

        
    }

    // Used to hide all buy/sell messages after period of time


    IEnumerator BuySellWaitTime(PlayerSetup playerSetup)
    {
        yield return new WaitForSeconds(5f);

        playerSetup.PropertyPanel.gameObject.SetActive(false);
        playerSetup.BuyButton.gameObject.SetActive(false);
        playerSetup.SellButton.gameObject.SetActive(false);
        playerSetup.displayMessage.gameObject.SetActive(false);

        
    }

    // Used to kick player if money is zero
    IEnumerator ElimiWait()
    {
        yield return new WaitForSeconds(5f);

        PropertyPanel.gameObject.SetActive(false);
        BuyButton.gameObject.SetActive(false);
        SellButton.gameObject.SetActive(false);
        displayMessage.gameObject.SetActive(false);
        DisconnectPlayer();

        
    }

    // Sets whos turn it is so everyone stays in sync

    [PunRPC]
    void setCurrentPlayerIndex()
    {
        int tempCur = currentPlayerIndex;

        if (tempCur == 4) {
            tempCur = 1;
        } else {
            tempCur += 1;
        }

        bool temp = true;
        while (temp == true) 
        {
            
            if (isInGame[tempCur - 1] == false) {
                if (tempCur == 4) {
                    tempCur = 1;
                } else {
                    tempCur += 1;
                }
            } else {
                temp = false;
            }

        }


        
        OneChangeMade = false;
        currentPlayerIndex = tempCur;

        diceNumber.text = "";
        diceNumberHeader.gameObject.SetActive(false);

        switch (currentPlayerIndex)
            {
                case 1:
                    turnMessage.text = "DOG's turn";
                    break;
                case 2:
                    turnMessage.text = "CAR's turn";
                    break;
                case 3:
                    turnMessage.text = "BIN's turn";
                    break;
                case 4:
                    turnMessage.text = "BOAT's turn";
                    break;
            }
    }

    // End turn functionality, changes to next players turn
    public void EndTurn() {

        PiecePicker.clicked = false;

        PropertyPanel.gameObject.SetActive(false);

        PV.RPC("setCurrentPlayerIndex", RpcTarget.AllBuffered);

    }

    // roll dice button is clicked

    public void rollDice() {
        PiecePicker.clicked = true;

        PV.RPC("rolling", RpcTarget.AllBuffered);

        StartCoroutine(RollingDiceEffect());

    }

    // Message shown that a player has been eliminated

    [PunRPC]
    void Elimination(int playerNumber) {
        switch (playerNumber)
            {
                case 1:
                    displayMessage.text = "DOG has filed for bankruptcy.";
                    break;
                case 2:
                    displayMessage.text = "CAR has filed for bankruptcy.";
                    break;
                case 3:
                    displayMessage.text = "BIN has filed for bankruptcy.";
                    break;
                case 4:
                    displayMessage.text = "BOAT has filed for bankruptcy.";
                    break;
            }
        isInGame[playerNumber - 1] = false;
        displayMessage.gameObject.SetActive(true);
        EndTurn();
    }

    

    // Visual effect of dice rolling on screen
    IEnumerator RollingDiceEffect()
    {
        // Duration for the rolling effect (in seconds)
        float rollDuration = 3.0f;

        // Randomly select a result
        int randomResult = UnityEngine.Random.Range(0, diceRollStrings.Count);
        string selectedResult = diceRollStrings[randomResult];

        // Simulate the rolling effect
        float elapsedTime = 0.0f;
        while (elapsedTime < rollDuration)
        {
            // Generate a random result string for the rolling effect
            int randomStringIndex = UnityEngine.Random.Range(0, diceRollStrings.Count);
            string randomResultString = diceRollStrings[randomStringIndex];

            // Display the random result string
            PV.RPC("changeString", RpcTarget.AllBuffered, randomResultString);

            // Increase elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Set the final selected result
        PV.RPC("changeString", RpcTarget.AllBuffered, selectedResult);


        // Deactivate the UI elements after the effect is complete (you can adjust the timing)
        yield return new WaitForSeconds(2.0f);
    }
}
