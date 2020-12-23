using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class Player : NetworkBehaviour
{
    public static Player localPlayer;

    [SyncVar] public string matchID;
    [SyncVar] public int playerIndex;

    NetworkMatchChecker networkMatchChecker;

    // Start is called before the first frame update
    void Start()
    {
        networkMatchChecker = GetComponent<NetworkMatchChecker>();
        if (isLocalPlayer)
        {
            localPlayer = this;

        }
        else
        {
            UILobby.instance.SpawnPlayerUIPrefab(this);
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /* 
     
     HOST GAME

    */
    public void HostGame()
    {
        string matchID = MatchMaker.GetRandomMatchId();
        CmdHostGame(matchID);
    }

    [Command]
    void CmdHostGame(string _matchID)
    {
        matchID = _matchID;
        if (MatchMaker.instance.HostGame(_matchID, gameObject, out playerIndex))
        {
            Debug.Log($"<color=green>Game Hosted Successfully</color>");
            networkMatchChecker.matchId = _matchID.ToGUID();
            TargetHostGame(true, _matchID, playerIndex);
        }
        else
        {
            Debug.Log($"<color=red>Game Hosted Failed</color>");
            TargetHostGame(false, _matchID, playerIndex);
        }
    }

    [TargetRpc]
    void TargetHostGame(bool success, string _matchID, int _playerIndex)
    {
        this.playerIndex = _playerIndex;
        matchID = _matchID;
        Debug.Log($"Match Id:{matchID}==  {_matchID}");
        UILobby.instance.HostSuccess(success, _matchID);
    }


    /* 
     
     JOIN GAME

    */

    public void JoinGame(string _inputID)
    {
        
        CmdJoinGame(_inputID);
    }

    [Command]
    void CmdJoinGame(string _matchID)
    {
        matchID = _matchID;
        if (MatchMaker.instance.JoinGame(_matchID, gameObject, out playerIndex))
        {
            Debug.Log($"<color=green>Game Hosted Successfully</color>");
            networkMatchChecker.matchId = _matchID.ToGUID();
            TargetJoinGame(true, _matchID, playerIndex);
        }
        else
        {
            Debug.Log($"<color=red>Game Hosted Failed</color>");
            TargetJoinGame(false, _matchID, playerIndex);
        }
    }

    [TargetRpc]
    void TargetJoinGame(bool success, string _matchID, int _playerIndex)
    {
        this.playerIndex = _playerIndex;
        matchID = _matchID;
        Debug.Log($"Match Id:{matchID}==  {_matchID}");
        UILobby.instance.JoinSuccess(success, _matchID);
    }

    /* 
     
     BEGIN GAME

    */
    public void BeginGame()
    {

        CmdBeginGame();
    }

    [Command]
    void CmdBeginGame()
    {
        MatchMaker.instance.BeginGame();
        Debug.Log($"<color=red>Game Beginning </color>");
        TargetBeginGame();
       
    }

    [TargetRpc]
    void TargetBeginGame()
    {
        Debug.Log($"Match Id:{matchID} | Beginning");

        //Additively load game Scene
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }
}
