using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILobby : MonoBehaviour
{
    public static UILobby instance;

    [SerializeField] InputField joinMatchInput;
    [SerializeField] Button joinButton;
    [SerializeField] Button hostButton;
    [SerializeField] Canvas lobbyCanvas;

    void Start()
    {
        instance = this;
    }
    public void Host()
    {
        joinButton.interactable = false;
        hostButton.interactable = false;
        joinMatchInput.interactable = false;
        Player.localPlayer.HostGame();
    }
    public void HostSuccess(bool success)
    {
        if (success)
        {
            lobbyCanvas.enabled = true;
        }
        else
        {
            joinButton.interactable = true;
            hostButton.interactable = true;
            joinMatchInput.interactable = true;
        }
    }
    public void Join()
    {
        joinButton.interactable = false;
        hostButton.interactable = false;
        joinMatchInput.interactable = false;
    }
    public void JoinSuccess(bool success){
        if (success)
        {

        }
        else
        {
            joinButton.interactable = true;
            hostButton.interactable = true;
            joinMatchInput.interactable = true;
        }
}

}
