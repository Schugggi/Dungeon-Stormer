using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;

public class PlayerObjectController : NetworkBehaviour
{
    [SyncVar] public int connectionID;
    [SyncVar] public int playerIdNumber;
    [SyncVar] public ulong playerSteamID;
    [SyncVar(hook = nameof(PlayerNameUpdate))] public string playerName;
    [SyncVar(hook = nameof(PlayerUpdateReady))] public bool playerReadyState;

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

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public override void OnStartAuthority()
    {
        CmdSetPlayerName(SteamFriends.GetPersonaName().ToString());
        gameObject.name = "LocalGamePlayer";
        LobbyController.Instance.FindLocalPlayer();
        LobbyController.Instance.UpdateLobbyName();
    }

    public override void OnStartClient()
    {
        Manager.GamePlayers.Add(this);
        LobbyController.Instance.UpdateLobbyName();
        LobbyController.Instance.UpdatePlayerList();
    }

    public override void OnStopClient()
    {
        Manager.GamePlayers.Remove(this);
        LobbyController.Instance.UpdatePlayerList();
    }

    [Command]
    private void CmdSetPlayerName(string PlayerName)
    {
        this.PlayerNameUpdate(this.playerName, PlayerName);  
    }

    public void PlayerNameUpdate(string oldValue, string newValue)
    {
        if (isServer)
        {
            this.playerName = newValue;
        }
        if (isServer)
        {
            LobbyController.Instance.UpdatePlayerList();
        }
    }

    public void PlayerUpdateReady(bool oldValue, bool newValue)
    {
        if (isServer)
        {
            this.playerReadyState = newValue;
        }
        if (isClient)
        {
            LobbyController.Instance.UpdatePlayerList();
        }
    }

    [Command]
    private void CmdChangeReady()
    {
        PlayerUpdateReady(this.playerReadyState, !this.playerReadyState);
    }

    public void ChangePlayerReady()
    {
        CmdChangeReady();
    }

    public void CanStartGame(string sceneName)
    {
        if (hasAuthority)
        {
            CmdCanStartGame(sceneName);
        }
    }

    [Command]
    public void CmdCanStartGame(string sceneName)
    {
        manager.StartGame(sceneName);
    }
}