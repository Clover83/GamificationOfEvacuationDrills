using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Just a class to deserialize the creds.json file.
// To add new keys (or potentially any sensitive information)
// add a new public field and mirror that name in the creds.json file.
// Then add the corresponding lines in HasEmptyKeys().

[System.Serializable]
public class APIKeys
{
    public string googleMaps;
    public string jsonBinMaster;
    public string jsonBinAccess;

    // No file extension since Resources.Load does not need it.
    // Relation is Assets/Resources/CREDS_PATH
    private const string CREDS_PATH = "creds";

    private bool HasEmptyKeys()
    {
        return googleMaps.Length == 0 ||
            jsonBinMaster.Length == 0 ||
            jsonBinAccess.Length == 0;
    }

    public static APIKeys LoadKeys()
    {
        TextAsset ta = Resources.Load<TextAsset>(CREDS_PATH);
        if (ta == null || ta.text.Length == 0)
        {
            Debug.Log("Empty or non-existant creds.hson file. Did you forget to make one?");
        }
        APIKeys keys = JsonUtility.FromJson<APIKeys>(ta.text);
        if (keys.HasEmptyKeys())
        {
            Debug.Log("creds.json contains empty key(s). Did you forget to initialize it?");
        }

        return keys;
    }
}
