using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInternetConnection : MonoBehaviour
{
    [SerializeField] private GameObject noInternetImage;
    private static bool internetPresent = false;

    // Update is called once per frame
    void Update()
    {
        noInternetImage.SetActive(!checkInternetConnection());
        internetPresent = checkInternetConnection();
    }

    private bool checkInternetConnection()
    {
        //if (Application.internetReachability == NetworkReachability.NotReachable)
        //    return false;
        return true;
    }

    public static bool getInternetConnection()
    {
        return internetPresent;
    }
}
