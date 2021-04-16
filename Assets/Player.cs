using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Player : MonoBehaviour
{
    public static float money = 2000000;
    
    public float money_ = 2000000;

    public static string companyName = "NoName";

    void Start(){
        Advertisement.Initialize("4074573", false);
    }
    
    public void ShowInterstitialAd() {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            if (money <= 50000)
            {
                money += 1000000;
            }
        }
        else
        {
            // Check if UnityAds ready before calling Show method:
            if (Advertisement.IsReady())
            {
                Advertisement.Show("video", new ShowOptions() { resultCallback = HandleShowResult });
            }
            else
            {
                Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
            }
        }
    }
    private void Update()
    {
    }
    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                Player.money += 1000000;
                break;
            case ShowResult.Skipped:
                Player.money += 1000000;
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }

}
