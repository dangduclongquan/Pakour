using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

// Assumes the player name is (ID:{ID}){Name}
public class DumbPhotonSecurity : IDumbNetworkSecurity
{
    private int GetIdFromName(string name)
    {
        if (name == "server")
            return 0;

        Regex rx = new Regex(@"^\(ID:([0-9]+)\).*");
        MatchCollection matches = rx.Matches(name);
        int id = Int32.Parse(matches[0].Groups[1].Value);

        return id;
    }

    private string GetPermissionLevel(string name)
    {
        if (name == "Server")
            return "server";

        int id = GetIdFromName(name);
        return GetPermissionLevel(id);
    }

    private string GetPermissionLevel(int id)
    {
        if (id == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            return "self";
        }
        else if (id == PhotonNetwork.MasterClient.ActorNumber)
        {
            return "host";
        }
        else if (id == 0)
        {
            return "server";
        }
        else
        {
            return "player";
        }
    }

    private bool CheckPermission(string permission, string requirement)
    {
        // for now, this is all we need
        if (requirement == "player")
        {
            Debug.LogWarning("\"player\" access level required: Player does not have any elevated access level.");
            Debug.LogWarning("Please check your access logic. Defaulting to true...");
            return true;
        }
        else if (permission == "server")
        {
            Debug.LogWarning("\"player\" access level required: Player does not have any elevated access level.");
            Debug.LogWarning("Please check your access logic. Defaulting to true...");
            return true;
        }
        else
        {
            return requirement == permission;
        }
    }

    public bool CheckPermissionByName(string name, string requirement)
    {
        string permission = GetPermissionLevel(name);
        return CheckPermission(permission, requirement);
    }

    public bool CheckPermissionByID(int id, string requirement)
    {
        string permission = GetPermissionLevel(id);
        return CheckPermission(permission, requirement);
    }
}