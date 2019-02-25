using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

	private int gameType;
	private int gameLevel;

	private int matched;
	private int goals;
	private int[] openedTileNumbers = {-1,-1};
	private GameObject[] openedTile = new GameObject[2];

	private bool gameIsPlaying = false;

	[SerializeField] private GameObject gameMusic = null;

	//GameType things
	//15 //30 //55 //70 //1.55 //2.35 //3.10 //4 //4.50 //6.40
	private float[] timeLimits = {70f,90f,115f,130f,175f,215f,250f,300f,350f,460f};

	void Awake(){
		AdMobManager.Instance.RequestBanner(0);
	}

	// Use this for initialization
	void Start () {
		AdMobManager.Instance.Set();		
		AdMobManager.Instance.ShowBanner();

		gameType = GameData.Instance.GetGameType();
		gameLevel = GameData.Instance.GetLevelNum();

		switch (gameType)
		{
			case 1:
				SetTimeChallange();
				break;
			case 2:
				
				break;
			default:
				
				break;
		}	

		AdMobManager.Instance.ShowBanner();
	}
	
	// Update is called once per frame
	void Update () {
		
		TimeManager.Instance.SetTimeIsRunning(gameIsPlaying);		
		GUIManager.Instance.UpdateClockText();		
		
		switch (gameType)
		{
			case 1:
				TimeChallange();
				break;
			case 2:
				
				break;
			default:
				
				break;
		}	
		
	}

	void SetTimeChallange(){		
		TimeManager.Instance.SetTimeCountDown(true,timeLimits[gameLevel]);		
	}

	void TimeChallange(){
				
		GUIManager.Instance.UpdateClockImageFilled(timeLimits[gameLevel],TimeManager.Instance.GetTime());

		if(TimeManager.Instance.GetTime() <= 0){
			GameOver();
		}
	}

	void GameOver(){

		TimeManager.Instance.ResetTime();
		GUIManager.Instance.SetClockTextToZero();

		GUIManager.Instance.ActiveGameOverPanel();

		//Disable Game Music
		gameMusic.GetComponent<AudioSource>().volume = 0.05f;
		
		gameIsPlaying = false;

	}

	public void PauseGame(){
		gameIsPlaying = false;
		gameMusic.GetComponent<AudioSource>().volume = 0.05f;
	}

	public void UnPauseGame(){
		gameIsPlaying = true;
		gameMusic.GetComponent<AudioSource>().volume = 0.4f;
	}
	

	public void SetOpenedTileNumber(GameObject tile){
		
		gameIsPlaying = true;
		
		if(openedTileNumbers[0] == -1){
			
			openedTile[0] = tile;
			openedTileNumbers[0] = tile.GetComponent<Tile>().GetNumber();

			AudioShouter.Instance.ShoutClip(1);
			
		} else if(openedTileNumbers[1] == -1){
			openedTile[1] = tile;
			openedTileNumbers[1] = tile.GetComponent<Tile>().GetNumber();
			CheckTileNumbersMatched();
		} else{

			AudioShouter.Instance.ShoutClip(1);

			openedTile[0].GetComponent<Tile>().CloseTile();
			openedTile[1].GetComponent<Tile>().CloseTile();

			openedTile[0] = tile;
			openedTileNumbers[0] = tile.GetComponent<Tile>().GetNumber();

			openedTileNumbers[1] = -1;
		}

		
	}

	void CheckTileNumbersMatched(){
		
		if(openedTile[0].GetComponent<Tile>().GetNumber() == openedTile[1].GetComponent<Tile>().GetNumber()){
			Matched();
		}else{
			UnMatched();
		}
	}

	bool Matched(){
		Debug.Log("Matched");
		openedTileNumbers[0] = -1;
		openedTileNumbers[1] = -1;

		openedTile[0].GetComponent<Tile>().SetMatched(true);
		openedTile[1].GetComponent<Tile>().SetMatched(true);

		AudioShouter.Instance.ShoutClip(2);

		matched++;
		if(matched >= goals){
			Finish();
		}

		return true;
	}

	void Finish(){

		//Set Next Level Opened;
		int level = GameData.Instance.GetLevelNum();
		int type = GameData.Instance.GetGameType();
		GameData.Instance.SetTypeLevelOpened(type,level+1,1);

		// AdMobmanager.Instance.ShowInterstitial();

		GUIManager.Instance.ActiveFinishPanel();

		//Disable Game Music
		gameMusic.GetComponent<AudioSource>().volume = 0.05f;

		

		gameIsPlaying = false;
	}

	bool UnMatched(){
		Debug.Log("UnMatched");
		openedTileNumbers[0] = -2;
		openedTileNumbers[1] = -2;

		AudioShouter.Instance.ShoutClip(3);

		return false;
		
	}

	public void SetGoals(int goals){
		this.goals = goals;
	}
}
