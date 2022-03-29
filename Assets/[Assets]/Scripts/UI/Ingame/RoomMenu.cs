using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;

public class RoomMenu : MonoBehaviourPunCallbacks
{
	[SerializeField] TMP_Text roomLabel;
	[SerializeField] Button StartButton;
	[SerializeField] Toggle PrivateToggle;

	private void UpdateClientStatus()
	{
		if (PhotonNetwork.IsMasterClient)
		{
			PrivateToggle.interactable = true;
			StartButton.gameObject.SetActive(true);
		}
		else
		{
			PrivateToggle.interactable = false;
			StartButton.gameObject.SetActive(false);
		}
		
		if (PhotonNetwork.CurrentRoom == null)
			roomLabel.text = "Singleplayer";
		else
			roomLabel.text = PhotonNetwork.CurrentRoom.Name;
		PrivateToggle.isOn = PhotonNetwork.CurrentRoom.IsOpen;
	}

	public override void OnEnable()
	{
		base.OnEnable();
		UpdateClientStatus();
	}

	public void ToggleInteract(bool state)
	{
		if (PhotonNetwork.IsMasterClient)
		{
			PhotonNetwork.CurrentRoom.IsOpen = state;
		}
		else
		{
			Debug.LogError("Client tried to toggle room privacy without permission");
		}
	}

	public void Leave()
	{
		SceneController.instance.LeaveRoom();
	}

    public void StartGame()
    {
        // To-do
    }
    
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        UpdateClientStatus();
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        UpdateClientStatus();
    }
}