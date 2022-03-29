using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using Photon.Pun;
using Photon.Realtime;

public class SceneController : MonoBehaviourPunCallbacks
{
    [SerializeField] LocalStateController StateController;

    public static SceneController instance;
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    void Start()
    {
        SceneManager.LoadScene("Launcher");
        StateController.Launcher();
    }

    public void Exit()
    {
        Application.Quit(0);
    }

    public void OpenLauncher()
    {
        if (SceneManager.GetActiveScene().name != "Launcher")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            PhotonNetwork.LoadLevel("Launcher");
            StateController.Launcher();
        }
        else
        {
            Debug.Log("Load Scene error: The launcher is already loaded.");
        }
    }
    
    public string GetActiveSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
        StateController.GameStart();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        OpenLauncher();
    }

    public override void OnLeftRoom()
    {
        if (GetActiveSceneName() != "Launcher")
        {
            WindowController.instance.ShowErrorMessage("You have been kicked from the room.");
            OpenLauncher();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (cause.ToString() != "None" && cause.ToString() != "DisconnectByClientLogic")
        {
            WindowController.instance.ShowErrorMessage("Disconnected: " + cause.ToString());
        }
        if (GetActiveSceneName() != "Launcher")
        { 
            OpenLauncher();
        }
    }

    public override void OnErrorInfo(ErrorInfo errorInfo)
    {
        WindowController.instance.ShowErrorMessage(errorInfo.Info);
    }
}
