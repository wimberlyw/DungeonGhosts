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
    public SyncListString matchIDs = new SyncListString();
    private void Start()
    {
        instance = this;
    }
    public bool HostGame(string _matchID, GameObject _player)
    {
        if (!matchIDs.Contains(_matchID)) {
            matchIDs.Add(_matchID);
            matches.Add(new Match(_matchID, _player));
            Debug.Log("Match Generated");
            return true;
        }
        else
        {
            Debug.Log("Match Id Already Exists");
            return false;
        }
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
        Debug.Log("Random Match ID: (_id)");
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

