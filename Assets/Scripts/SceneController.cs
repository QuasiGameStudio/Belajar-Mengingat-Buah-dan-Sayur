using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class SceneController : Singleton<SceneController> {

	[SerializeField] private string backToScene = "";

	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			if(backToScene == "QuitGame"){
				QuitGame();
			}else if(backToScene == ""){
								
			}else{
				GoToScene(backToScene);
			}
		} 
	}

	void ActiveBackPanel(){

	}

	public void RestartScene (){
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	public void GoToScene(string sceneName){
		SceneManager.LoadScene (sceneName);
	}

	public void GoToSceneWithAds(string dataString){
		//data index is
		// 0 = adsType , 1 = adsIdIndex , 2 = sceneName
		string[] data = dataString.Split('_');
		
		AdsManager.Instance.CallAdsWithScene(Int32.Parse(data[0]), Int32.Parse(data[1]), data[2]);
	}

	
	public void QuitGame(){
		Application.Quit ();
	}

	
}
