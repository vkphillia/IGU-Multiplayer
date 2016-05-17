using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityStandardAssets.Network;


public class GameManager : NetworkBehaviour
{
    static public List<PlayerCombat> sFighters = new List<PlayerCombat>();
    static public GameManager sInstance = null;
    int i;
    
    //public bool _gameRunning;
     
    void Awake()
    {
        sInstance = this;
    }

    void Start()
    {
        for (int i = 0; i < sFighters.Count; ++i)
        {
            sFighters[i].Init();
        }
        
        if (isServer)
        {
            RpcCountDown();
            //_gameRunning = true;
        }
    }

    [ClientRpc]
    void RpcCountDown()
    {
        Debug.Log("Inside rpc L=" + isLocalPlayer + "s=" + isServer);
        StartCoroutine(UIManager.Instance.CountdownToGame());
    }

    public IEnumerator ReturnToLoby()
    {
        yield return new WaitForSeconds(3.0f);
        LobbyManager.s_Singleton.ServerReturnToLobby();
    }

    //public void GameOver()
    //{
    //    if(sFighters[0].health>sFighters[1].health)
    //    {
    //        //sFighters[0] won
    //        sFighters[0].roundWon = true;
    //    }
    //    else
    //    {
    //        //sFighters[1] won
    //        sFighters[1].roundWon = true;
    //    }
    //}
}
