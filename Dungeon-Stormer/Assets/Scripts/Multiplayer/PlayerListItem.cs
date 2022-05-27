using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using UnityEngine.UI;

public class PlayerListItem : MonoBehaviour
{
    public string playerName;
    public int connectionID;
    public ulong playerSteamID;
    private bool AvatarReceived;

    public Text playerText;
    public RawImage playerIcon;

    public Text playerReadyButtonText;
    public bool ready;

    protected Callback<AvatarImageLoaded_t> ImageLoaded;

    private void Start()
    {
        ImageLoaded = Callback<AvatarImageLoaded_t>.Create(OnImageLoaded);
    }

    public void ChangePlayerReadyState()
    {
        if (ready)
        {
            playerReadyButtonText.text = "o";
            playerReadyButtonText.color = Color.green;
        }
        else if(!ready)
        {
            playerReadyButtonText.text = "x";
            playerReadyButtonText.color = Color.red;
        }
    }

    private void OnImageLoaded(AvatarImageLoaded_t callback)
    {
        if(callback.m_steamID.m_SteamID == playerSteamID)
        {
            playerIcon.texture = GetSteamImageAsTexture(callback.m_iImage);
        }
        else
        {
            return;
        }
    }

    public void SetPlayerValues()
    {
        playerText.text = playerName;
        ChangePlayerReadyState(); 
        if(!AvatarReceived) { GetPlayerIcon(); }
    }

    void GetPlayerIcon()
    {
        int imageID = SteamFriends.GetLargeFriendAvatar((CSteamID)playerSteamID);

        if (imageID == -1)
        {
            return;
        }

        playerIcon.texture = GetSteamImageAsTexture(imageID);
    }

    private Texture2D GetSteamImageAsTexture(int iImage)
    {
        Texture2D texture = null;

        bool isValid = SteamUtils.GetImageSize(iImage, out uint width, out uint height);
        if (isValid)
        {
            byte[] image = new byte[width * height * 4];

            isValid = SteamUtils.GetImageRGBA(iImage, image, (int)(width * height * 4));

            if (isValid)
            {
                texture = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false, true);
                texture.LoadRawTextureData(image);
                texture.Apply();
            }
        }
        AvatarReceived = true;
        return texture;
    }
}
