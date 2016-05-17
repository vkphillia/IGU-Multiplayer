using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets.Network;

public class NetworkLobbyHook : LobbyHook
{
    int no;

    // for users to apply settings from their lobby player object to their in-game player object
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        //var cc = lobbyPlayer.GetComponent<ColorControl>();
        var player = gamePlayer.GetComponent<PlayerCombat>();
        player.playerNo =no;
        no++;
        Debug.Log(no);
    }
}
