using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSceneWithInterAds(string sceneName){
        int adsType = 0;

        if(GameData.Instance.GetAdsThresHold(adsType) < 3){
            GameData.Instance.AddAdsThresHold(adsType);            
        } else {
            
            AdMobManager.Instance.RequestBanner(0);
            AdMobManager.Instance.ShowInterstitial(sceneName);

            if(AdMobManager.Instance.GetLastAdsIsSuccessToLoaded())
                GameData.Instance.ResetAdsThresHold(adsType);
        }
    }
}
