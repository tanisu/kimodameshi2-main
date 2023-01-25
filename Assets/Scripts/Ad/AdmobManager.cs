using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;
public class AdmobManager : MonoBehaviour
{
    BannerView bannerView;
    void Start()
    {
        RequestBanner();
    }

    
    void Update()
    {
        
    }
    // riwa-do   ca-app-pub-4751206041539571~7975444353


    private void RequestBanner()
    {
#if UNITY_IPHONE
        string adUnitId = "ca-app-pub-4751206041539571~7975444353";
#else
        string adUnitId = "";
#endif

        if(bannerView != null)
        {
            bannerView.Destroy();
        }

        AdSize adaptiveSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Bottom);
        bannerView.OnAdLoaded += HandleAdLoaded;
        bannerView.OnAdFailedToLoad += HandleAdFailedToLoad;
        AdRequest adRequest = new AdRequest.Builder().Build();
        bannerView.LoadAd(adRequest);
    }
    #region BannerCallbackHanders


    public void HandleAdLoaded(object sender , EventArgs args)
    {

    }
    
    public void HandleAdFailedToLoad(object sender,AdFailedToLoadEventArgs args)
    {

    }
    #endregion
}
