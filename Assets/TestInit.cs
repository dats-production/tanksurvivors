using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using GameAnalyticsSDK;
using UnityEngine;

public class TestInit : MonoBehaviour
{   
    void Start()
    {
        FB.Init();
        Amplitude.Instance.init("8318cfa57c2f17bc75bc3bb41ba537c3");
        GameAnalytics.Initialize();
    }
}
