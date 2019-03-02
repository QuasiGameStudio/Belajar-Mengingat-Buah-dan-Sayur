using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : Singleton<AdsManager>
{

    string goToSceneWithAdName = "Game";

    void OnGUI(){
        //GUI.Label(new Rect(10, 10, 150, 100), "ThresHold: " + GameData.Instance.GetAdsThresHold(0));
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
        // this.goToSceneWithAdName = sceneName;

        if(GameData.Instance.GetAdsThresHold(adsType) < 2){
            GameData.Instance.AddAdsThresHold(adsType);             
            SceneController.Instance.GoToScene(goToSceneWithAdName);           
                                    
        } else {
            // Debug.Log("GGGGGGGGGGGGGGGGGGGGGGGGGGGGGG");
            if(AdMobManager.Instance.GetLastAdsIsSuccessToLoaded()){
                AdMobManager.Instance.ShowInterstitial();
                // Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            } else {
                // Debug.Log("BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB");
                SceneController.Instance.GoToScene(goToSceneWithAdName);           
            }

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
                
                break;
            case 2:
                
                break;
            case 3:
                SceneController.Instance.GoToScene(goToSceneWithAdName);
                GameData.Instance.ResetAdsThresHold(0);
                break;
            case 4:
                
                break;
            default:
                
                break;
        }
    }
   
}
