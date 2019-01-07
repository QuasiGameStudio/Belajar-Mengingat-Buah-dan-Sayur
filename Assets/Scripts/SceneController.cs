using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : Singleton<SceneController> {

	[SerializeField] private string backToScene;

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

	public void QuitGame(){
		Application.Quit ();
	}

	
}
