using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class AdmobReward : MonoBehaviour
{
    string adUnitId;
    RewardedAd rewardedAd;
    bool rewardeFlag = false;

    public void ShowReward()
    {
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
        else
        {
            GameManager.i.Restart();
        }
    }

    public void Test()
    {

    }

    void Start()
    {
#if UNITY_IPHONE
        adUnitId = "ca-app-pub-4751206041539571~7975444353";
#else
        adUnitId = "";
#endif
        CreateAndLoadRewardedAd();
    }

    // Update is called once per frame
    void Update()
    {
        if (rewardeFlag)
        {
            rewardeFlag = false;
            GameManager.i.Restart();
        }
        //else
        //{
        //    Debug.Log("dame");
        //}
    }

    public void CreateAndLoadRewardedAd()
    {
        rewardedAd = new RewardedAd(adUnitId);
        rewardedAd.OnAdLoaded += HandleReardedAdLoaded;
        rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
    }
    public void HandleReardedAdLoaded(object sender, EventArgs args)
    {

    }
    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {

    }

    public void HandleRewardedAdClosed(object sender,EventArgs args)
    {
        CreateAndLoadRewardedAd();
    }

    public void HandleUserEarnedReward(object sender , Reward args)
    {
        rewardeFlag = true;
    }
}
