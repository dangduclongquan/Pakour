using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;
using Photon.Realtime;

public class PhotonPlayerManager : MonoBehaviourPunCallbacks
{
	[SerializeField] string SurvivorPrefabAddress;
	[SerializeField] GameObject CameraSystemPrefab;

	void Start()
	{
		Survivor survivor = PhotonNetwork.Instantiate(SurvivorPrefabAddress, new Vector3(0, 0, 0), Quaternion.identity, 0).GetComponent<Survivor>();
		CameraSystem camera = Instantiate(CameraSystemPrefab).GetComponent<CameraSystem>();
		camera.Attach(survivor);
	}
}