using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    public static Player localPlayer;

    [SyncVar]public string matchID;

    NetworkMatchChecker networkMatchChecker;

    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            localPlayer = this;
            
        }
        networkMatchChecker = GetComponent<NetworkMatchChecker>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HostGame()
    {
        string matchID = MatchMaker.GetRandomMatchId();
        CmdHostGame(matchID);
    }

    [Command]
    void CmdHostGame(string _matchID)
    {
        matchID = _matchID;
        if (MatchMaker.instance.HostGame(_matchID, gameObject))
        {
            Debug.Log($"<color = green>Game Hosted Successfully</color>");
            networkMatchChecker.matchId = _matchID.ToGUID();
            TargetHostGame(true, _matchID);
        }
        else
        {
            Debug.Log($"<color = red>Game Hosted Failed</color>");
            TargetHostGame(false, _matchID);
        }
    }

    [TargetRpc]
    void TargetHostGame(bool success, string _matchID)
    {
        Debug.Log($"Match Id: (matchID)== (_matchID)");
        UILobby.instance.HostSuccess(success);
    }

}
