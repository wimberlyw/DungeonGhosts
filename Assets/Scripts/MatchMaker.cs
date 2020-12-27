using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using System.Security.Cryptography;
using System.Text;

public class MatchMaker : NetworkBehaviour
{
    [System.Serializable]
    public class Match
    {
        public string matchID;
        //when a match is created create a new network sync list of players
        public SynclistGameObject players = new SynclistGameObject();

        public Match(string MatchID, GameObject player)
        {
            this.matchID = MatchID;
            players.Add(player);
        }

        public Match() { }

    }

    [System.Serializable]
    public class SynclistGameObject : SyncList<GameObject>
    {

    }

    public class SyncListMatch : SyncList<Match>
    {

    }

    public static MatchMaker instance;

    public SyncListMatch matches = new SyncListMatch();
    public SyncList<string> matchIDs = new SyncList<string>();
    private void Start()
    {
        instance = this;
    }
    public bool HostGame(string _matchID, GameObject _player, out int playerIndex)
    {
        playerIndex = -1;
        if (!matchIDs.Contains(_matchID)) {
            matchIDs.Add(_matchID);
            matches.Add(new Match(_matchID, _player));
            Debug.Log($"Match Generated");
            playerIndex = 1;
            return true;
        }
        else
        {
            Debug.Log($"Match Id Already Exists");
            return false;
        }
    }

    public bool JoinGame(string _matchID, GameObject _player, out int playerIndex)
    {
        playerIndex = -1;
        if (matchIDs.Contains(_matchID))
        {
            for(int i = 0; i< matches.Count; i++)
            {
                if (matches[i].matchID == _matchID)
                {
                    matches[i].players.Add(_player);
                    playerIndex = matches[i].players.Count ;
                    break;
                }
                    
                }
            
            Debug.Log($"Match Joined");
            return true;
        }
        else
        {
            Debug.Log($"Match Id Does NOT Exists");
            return false;
        }
    }

    public void BeginGame()
    {
        //Turn Manager 
    }


    public static string GetRandomMatchId()
    {
        string _id = string.Empty;
        for (int i = 0; i < 5; i++)
        {
            //Random 0 to 36 to include letters and numbers
            int random = UnityEngine.Random.Range(0, 36);
            if (random < 26)
            {
                //+65 will make the letter capital
                _id += (char)(random + 65);
            }
            else
            {

                _id += (random - 26).ToString();
            }
        }
        Debug.Log($"Random Match ID: {_id}");
        return _id;
    }
    }

    public static class MatchExtensions
        {
        public static Guid ToGUID(this string id)
            {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            byte[] inputBytes = Encoding.Default.GetBytes(id);
            byte[] hashBytes = provider.ComputeHash(inputBytes);
            return new Guid(hashBytes);
        }
    }

