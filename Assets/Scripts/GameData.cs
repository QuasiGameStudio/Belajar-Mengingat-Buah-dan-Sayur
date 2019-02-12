using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : Singleton<GameData> {

	[SerializeField] private bool resetData;

	void Awake(){
		if(resetData)
			PlayerPrefs.DeleteAll();

		//Set First Level Opened
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 1; j++)
			{
				SetTypeLevelOpened(i,j,1);
			}
		}
	}

	void Start(){
		
	}

	public void SetLevelNum(int num){
		PlayerPrefs.SetInt("LevelNum", num);
	}

	public int GetLevelNum(){
		return PlayerPrefs.GetInt("LevelNum", 0);
	}

	public void SetGameType(int type){
		PlayerPrefs.SetInt("GameType", type);
	}

	public int GetGameType(){
		return PlayerPrefs.GetInt("GameType", 0);
	}

	public void SetTypeLevelOpened(int type, int level, int opened){
		PlayerPrefs.SetInt("TypeLevel_" + type + "_" + level, opened);
	}

	public int GetTypeLevelOpened(int type, int level){
		return PlayerPrefs.GetInt("TypeLevel_" + type + "_" + level, 0);
	}
}
