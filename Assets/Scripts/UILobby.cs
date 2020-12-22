using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILobby : MonoBehaviour
{
    [SerializeField] InputField joinMatchInput;
    [SerializeField] Button joinButton;
    [SerializeField] Button hostButton;

    public void Host()
    {
        joinButton.interactable = false;
        hostButton.interactable = false;
        joinMatchInput.interactable = false;
    }

    public void Join()
    {
        joinButton.interactable = false;
        hostButton.interactable = false;
        joinMatchInput.interactable = false;
    }
}
