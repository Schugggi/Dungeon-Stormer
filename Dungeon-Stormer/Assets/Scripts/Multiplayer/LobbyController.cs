using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using UnityEngine.UI;
using System.Linq;

public class LobbyController : MonoBehaviour
{
    public static LobbyController Instance;

    public Text lobbyNameText;

    public GameObject PlayerListViewContent;
    public GameObject PlayerListItemPrefab;
    public GameObject LocalPlayerObject;

    public bool allReady = false;
    public Button GameLaunchButton;

    public ulong currentLobbyID;
    public bool playerItemCreated = false;
    private List<PlayerListItem> playerListItem = new List<PlayerListItem>();
    public PlayerObjectController LocalPlayerController;

    public Text playerReadyButtonText;

    private customNetworkManager manager;
    private customNetworkManager Manager
    {
        get
        {
            if(manager != null)
            {
                return manager;
            }
            return manager = customNetworkManager.singleton as customNetworkManager;
        }
    }

    public void ChangePlayerState()
    {
        LocalPlayerController.ChangePlayerReady();
    }

    private void UpdateButton()
    {
        if (LocalPlayerController.playerReadyState)
        {
            playerReadyButtonText.text = "Unready";
        }
        else if (!LocalPlayerController.playerReadyState)
        {
            playerReadyButtonText.text = "Ready";
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void UpdateLobbyName()
    {
        currentLobbyID = Manager.GetComponent<steamLobby>().currentLobbyID;
        lobbyNameText.text = SteamMatchmaking.GetLobbyData(new CSteamID(currentLobbyID), "name");
    }

    public void UpdatePlayerList()
    {
        if (!playerItemCreated)
        {
            CreateHostPlayerItem();
        }
        if (playerListItem.Count < Manager.GamePlayers.Count)
        {
            CreateClientPlayerItem();
        }
        if (playerListItem.Count > Manager.GamePlayers.Count)
        {
            RemovePlayerItem();
        }
        if (playerListItem.Count == Manager.GamePlayers.Count)
        {
            UpdatePlayerItem();
        }
    }
    public void FindLocalPlayer()
    {
        LocalPlayerObject = GameObject.Find("LocalGamePlayer");
        LocalPlayerController = LocalPlayerObject.GetComponent<PlayerObjectController>();
    }

    public void CreateHostPlayerItem()
    {
        foreach(PlayerObjectController player in Manager.GamePlayers)
        {
            GameObject NewPlayerItem = Instantiate(PlayerListItemPrefab) as GameObject;
            PlayerListItem NewPlayerItemScript = NewPlayerItem.GetComponent<PlayerListItem>();

            NewPlayerItemScript.playerName = player.playerName;
            NewPlayerItemScript.connectionID = player.connectionID;
            NewPlayerItemScript.playerSteamID = player.playerSteamID;
            NewPlayerItemScript.ready = player.playerReadyState;
            NewPlayerItemScript.SetPlayerValues();

            NewPlayerItem.transform.SetParent(PlayerListViewContent.transform);
            NewPlayerItem.transform.localScale = Vector3.one;

            playerListItem.Add(NewPlayerItemScript);
        }
        playerItemCreated = true;
    }

    public void CreateClientPlayerItem()
    {
        foreach (PlayerObjectController player in Manager.GamePlayers)
        {
            if (!playerListItem.Any(b => b.connectionID == player.connectionID))
            {
                GameObject NewPlayerItem = Instantiate(PlayerListItemPrefab) as GameObject;
                PlayerListItem NewPlayerItemScript = NewPlayerItem.GetComponent<PlayerListItem>();

                NewPlayerItemScript.playerName = player.playerName;
                NewPlayerItemScript.connectionID = player.connectionID;
                NewPlayerItemScript.playerSteamID = player.playerSteamID;
                NewPlayerItemScript.ready = player.playerReadyState;
                NewPlayerItemScript.SetPlayerValues();

                NewPlayerItem.transform.SetParent(PlayerListViewContent.transform);
                NewPlayerItem.transform.localScale = Vector3.one;

                playerListItem.Add(NewPlayerItemScript);
            }
        }
    }

    public void UpdatePlayerItem()
    {
        foreach (PlayerObjectController player in Manager.GamePlayers)
        {
            foreach (PlayerListItem PlayerListItemScript in playerListItem)
            {
                if(PlayerListItemScript.connectionID == player.connectionID)
                {
                    PlayerListItemScript.playerName = player.playerName;
                    PlayerListItemScript.ready = player.playerReadyState;
                    PlayerListItemScript.SetPlayerValues();
                    if (player == LocalPlayerController)
                    {
                        UpdateButton();
                    }
                }
            }
        }
        CheckAllReady();
    }

    public void RemovePlayerItem()
    {
        List<PlayerListItem> playerListItemToRemove = new List<PlayerListItem>();

        foreach(PlayerListItem playerListItem in playerListItem)
        {
            if(!Manager.GamePlayers.Any(b => b.connectionID == playerListItem.connectionID))
            {
                playerListItemToRemove.Add(playerListItem);
            }
        }

        if (playerListItemToRemove.Count > 0)
        {
            foreach (PlayerListItem playerlistItemToRemove in playerListItemToRemove)
            {
                GameObject ObjectToRemove = playerlistItemToRemove.gameObject;
                playerListItem.Remove(playerlistItemToRemove);
                Destroy(ObjectToRemove);
                ObjectToRemove = null;
            }
        }
    }

    public void CheckAllReady()
    {
        foreach(PlayerObjectController player in Manager.GamePlayers)
        {
            if (!player.playerReadyState)
            {
                allReady = false;
                break;
            }

            else
            {
                allReady = true;
            }
        }

        if (allReady)
        {
            GameLaunchButton.interactable = true;
        }
        else
        {
            GameLaunchButton.interactable = false;
        }
    }

    public void StartGame(string sceneName)
    {
        LocalPlayerController.CanStartGame(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit(0);
    }
}