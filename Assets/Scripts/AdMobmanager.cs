using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// #if GOOGLE_MOBILE_ADS
using GoogleMobileAds;
using GoogleMobileAds.Api;
// #endif


public class AdMobManager : Singleton<AdMobManager> {

	#region
// #if GOOGLE_MOBILE_ADS
	
	bool testingMode = true;

	BannerView bannerView;
	InterstitialAd interstitial;
	AdRequest requestBanner;
	AdRequest requestInterstitial;

	string goToSceneWithAdName = "null";
	int idIndex = 0;
	bool lastAdsIsSuccessToLoaded = false;
// #endif

// #if GOOGLE_MOBILE_ADS
	private string appId = "ca-app-pub-3204981671781860~3979982505";	

	//original ads id
	// private string[] bannerIds = new string[] {
	// 	"ca-app-pub-3204981671781860/2343467205"
	// };	
	// private string[] interstitialIds = new string {
	// 	"ca-app-pub-3204981671781860/8130856791"
	// };

	//test ads id
	private string[] bannerIds = new string[] {"ca-app-pub-3940256099942544/6300978111"};	
	private string[] interstitialIds = new string[] {"ca-app-pub-3940256099942544/1033173712"};

	//Edit with your device id
	private string testDeviceId = "81A5D70CE479330C99C85E799E15DA1A";
	
	
// #endif

	
	int tryingToShowInterstitial;

	void Awake(){

		DontDestroyOnLoad (this);
					
// #if GOOGLE_MOBILE_ADS
		MobileAds.Initialize(appId);
// #endif

		Set();	

	}

	public void Set(){

		if(bannerView != null){
			bannerView.Hide();
		}
		
// #if GOOGLE_MOBILE_ADS	
		RequestBanner(idIndex);					
// #endif
		bannerView.Show();

	}

	public void DestroyBanner(){
		bannerView.Destroy();
	}

	public void DestoryInterstitial(){
		interstitial.Destroy();
	}

	public void SetIdIndex(int idIndex){
		this.idIndex = idIndex;
	}

	public void RequestBanner(int idIndex){

		bannerView =  new BannerView(bannerIds[idIndex], AdSize.SmartBanner, AdPosition.Bottom);		
				
		if(testingMode)
			requestBanner = new AdRequest.Builder().AddTestDevice(testDeviceId).Build();
		else
			requestBanner = new AdRequest.Builder().Build();

		bannerView.LoadAd(requestBanner);					
	}

	public void RequestInterstitial(int idIndex){
// #if GOOGLE_MOBILE_ADS		
		interstitial = new InterstitialAd(interstitialIds[idIndex]);

		if(testingMode)
			requestInterstitial = new AdRequest.Builder().AddTestDevice(testDeviceId).Build();
		else
			requestInterstitial = new AdRequest.Builder().Build();
		
		interstitial.OnAdClosed += HandleOnAdClosed;
		interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;

		interstitial.LoadAd(requestInterstitial);
		
// #endif
	}
	
	public void ShowBanner(){
// #if GOOGLE_MOBILE_ADS		
        bannerView.Show();        
// #endif
	}

	public void ShowInterstitial(){
// #if GOOGLE_MOBILE_ADS
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
        else
        {
            RequestInterstitial(idIndex);
            if (interstitial.IsLoaded())
            {
                interstitial.Show();
            }
        }
// #endif
	}

	public void ShowInterstitial(string goToSceneWithAdName){
// #if GOOGLE_MOBILE_ADS

		this.goToSceneWithAdName = goToSceneWithAdName;

		if (interstitial.IsLoaded())
		{
			interstitial.Show();
		}
		else
		{
			RequestInterstitial(idIndex);
			if (interstitial.IsLoaded())
			{
				interstitial.Show();
			}
		}
// #endif
	}

	public bool GetLastAdsIsSuccessToLoaded(){
		return lastAdsIsSuccessToLoaded;
	}

// #if GOOGLE_MOBILE_ADS
	// Called when an ad request has successfully loaded.
	public void HandleOnAdLoaded(object sender, EventArgs args)
	{
			
	}

	public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		SceneController.Instance.GoToScene(goToSceneWithAdName);
	}

	public void HandleOnAdOpened(object sender, EventArgs args)
	{
			
	}

	public void HandleOnAdClosed(object sender, EventArgs args)
	{
		SceneController.Instance.GoToScene(goToSceneWithAdName);			
	}

	public void HandleOnAdLeavingApplication(object sender, EventArgs args)
	{
			
	}
	
// #endif

	#endregion
}
