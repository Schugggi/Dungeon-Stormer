using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using UnityEngine.UI;

public class steamLobby : MonoBehaviour
{
    public static steamLobby Instance;

    protected Callback<LobbyCreated_t> LobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> LobbyJoinRequest;
    protected Callback<LobbyEnter_t> LobbyEntered;

    public ulong currentLobbyID;
    private const string HostAdressKey = "HostAdress";
    public customNetworkManager manager;

    private void Start()
    {
        if (!SteamManager.Initialized)
        {
            return;
        }

        if(Instance == null){
            Instance = this;
        }

        LobbyCreated = Callback<LobbyCreated_t>.Create(onLobbyCreated);
        LobbyJoinRequest = Callback<GameLobbyJoinRequested_t>.Create(onLobbyJoinRequest);
        LobbyEntered = Callback<LobbyEnter_t>.Create(onLobbyEntered);
    }

    public void HostLobby()
    {
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, manager.maxConnections);
    }

    private void onLobbyCreated(LobbyCreated_t callback)
    {
        if (callback.m_eResult != EResult.k_EResultOK)
        {
            return;
        }

        manager.StartHost();

        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAdressKey, SteamUser.GetSteamID().ToString());
        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "name", SteamFriends.GetPersonaName().ToString() + "'s Lobby");
    }

    private void onLobbyJoinRequest(GameLobbyJoinRequested_t callback)
    {
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    private void onLobbyEntered(LobbyEnter_t callback)
    {
        currentLobbyID = callback.m_ulSteamIDLobby;

        if (NetworkServer.active)
        {
            return;
        }

        manager.networkAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAdressKey);
        manager.StartClient();
    }
}