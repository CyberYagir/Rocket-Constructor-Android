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
        money = PlayerPrefs.GetInt("Money", 2000000);
    }
    
    public void ShowInterstitialAd() {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady()) {
            Advertisement.Show("video", new ShowOptions() { resultCallback = HandleShowResult});
        } 
        else {
            Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
        }
    }
    private void Update()
    {
        if (money != money_)
        {
            PlayerPrefs.SetFloat("Money", money);
            money_ = money;
        }
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
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }

}
