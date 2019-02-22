using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : Singleton<AdsManager>
{

    string goToSceneWithAdName = "null";

    void OnGUI(){
        GUI.Label(new Rect(10, 10, 150, 100), "ThresHold: " + GameData.Instance.GetAdsThresHold(0));
    }

    
    
    public void CallAdsWithScene(int adsType, int adsIdIndex,string sceneName){
        switch (adsType){
            case 0:
                CallInterstitialAds(adsIdIndex,sceneName);
                break;
            default:
                CallInterstitialAds(0,sceneName);
                break;
        }
    }

    public void CallInterstitialAds(int adsIdIndex,string sceneName){

        int adsType = 0;
        this.goToSceneWithAdName = sceneName;

        if(GameData.Instance.GetAdsThresHold(adsType) < 3){
            GameData.Instance.AddAdsThresHold(adsType); 
            SceneController.Instance.GoToScene(goToSceneWithAdName);           
        } else {
            
            AdMobManager.Instance.RequestInterstitial(adsIdIndex);
            AdMobManager.Instance.ShowInterstitial();

            if(AdMobManager.Instance.GetLastAdsIsSuccessToLoaded())
                GameData.Instance.ResetAdsThresHold(adsType);
        }

        

    }

    public void SetAdsEventResult(int eventId){
        //event id 
        //0 = Ads Loaded
        //1 = Ads Failed to loaded
        //2 = Ads Opened
        //3 = Ads Closed
        //4 = User Leaving App

        switch (eventId){
            case 0:
                
                break;
            case 1:
                SceneController.Instance.GoToScene(goToSceneWithAdName);
                break;
            case 2:
                
                break;
            case 3:
                SceneController.Instance.GoToScene(goToSceneWithAdName);
                break;
            case 4:
                
                break;
            default:
                
                break;
        }
    }
   
}
