using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILobby : MonoBehaviour
{
    public static UILobby instance;
    [Header("Host Join")]
    [SerializeField] InputField joinMatchInput;
    [SerializeField] Button joinButton;
    [SerializeField] Button hostButton;
    [SerializeField] Canvas lobbyCanvas;

    [Header("Lobby")]
    [SerializeField] Transform UIPlayerParent;
    [SerializeField] GameObject UIPlayerPrefab;
    [SerializeField] Text matchIDText;
    [SerializeField] GameObject beginGameButton;


    void Start()
    {
        instance = this;
    }

    /* HOST GAME 
     */

    public void Host()
    {
        joinButton.interactable = false;
        hostButton.interactable = false;
        joinMatchInput.interactable = false;
        Player.localPlayer.HostGame();
    }
    public void HostSuccess(bool success, string matchID)
    {
        if (success)
        {
            lobbyCanvas.enabled = true;
            SpawnPlayerUIPrefab(Player.localPlayer);
            matchIDText.text = matchID;
            beginGameButton.SetActive(true);
        }
        else
        {
            joinButton.interactable = true;
            hostButton.interactable = true;
            joinMatchInput.interactable = true;
        }

    }

    /* 
     JOIN GAME 
     */

    public void Join()
    {
        joinButton.interactable = false;
        hostButton.interactable = false;
        joinMatchInput.interactable = false;
        Player.localPlayer.JoinGame(joinMatchInput.text.ToUpper());
    }
    public void JoinSuccess(bool success, string matchID){
        if (success)
        {
            lobbyCanvas.enabled = true;
            SpawnPlayerUIPrefab(Player.localPlayer);
            matchIDText.text = Player.localPlayer.matchID;
        }
        else
        {
            joinButton.interactable = true;
            hostButton.interactable = true;
            joinMatchInput.interactable = true;
        }
}
    /* 
     JOIN GAME 
     */
    public void SpawnPlayerUIPrefab(Player player)
    {
        GameObject newUIPlayer = Instantiate(UIPlayerPrefab, UIPlayerParent);
        newUIPlayer.GetComponent<UIPlayer>().SetPlayer(player);
        newUIPlayer.transform.SetSiblingIndex(player.playerIndex - 1);
    }

    public void BeginGame()
    {
        Player.localPlayer.BeginGame();
    }
}

