using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour {

	[SerializeField] private Text gameTypeText = null;
	[SerializeField] private GameObject[] levelButtons = null;

	// Use this for initialization
	void Start () {
		SetLevelButtons(GameData.Instance.GetGameType());
		switch (GameData.Instance.GetGameType())
		{
			case 1:
				gameTypeText.text = "Time Challange";
				break;
			case 2:
				
				break;
			default:
				gameTypeText.text = "Simple";
				break;
		}	
		StartCoroutine("PopUpLevelButtons");

		AdMobManager.Instance.Set();		
		AdMobManager.Instance.ShowBanner();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator PopUpLevelButtons(){
		foreach (GameObject lvlButton in levelButtons)
		{
			yield return new WaitForSeconds(0.1f);
			lvlButton.GetComponent<Animator>().SetTrigger("PopUp");
			AudioShouter.Instance.ShoutClip(0);
		}

	}

	public void SetLevelButtons(int gameType){
		for (int i = 0; i < levelButtons.Length; i++)
		{
			
			levelButtons[i].transform.GetChild(0).GetComponent<Button>().interactable = GameData.Instance.GetTypeLevelOpened(gameType,i) > 0;
			levelButtons[i].SetActive(false);
			levelButtons[i].SetActive(true);
		}
		StartCoroutine("PopUpLevelButtons");

		GameData.Instance.SetGameType(gameType);
	}
}
