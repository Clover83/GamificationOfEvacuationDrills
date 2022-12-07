using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;


public class AskForPermissions : MonoBehaviour
{
    //Increase size if there is a need for more perms
    string[] _permissions = new string[2];
    static int _nrPerms;
    static bool _asked = false;
    private void Start()
    {
        if(_asked == false)
        {
            _asked = true;
            //AddPermission(Permission.Microphone.ToString());
            //AddPermission(Permission.ExternalStorageRead.ToString());
            AddPermission(Permission.FineLocation.ToString());
            AddPermission(Permission.Camera.ToString());
            Permission.RequestUserPermissions(_permissions);
        }

    }

    void AddPermission(string bob)
    {
        _permissions[_nrPerms] = bob;
        _nrPerms++;
    }
}