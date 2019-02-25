using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : Singleton<GUIManager> {

	[SerializeField] private GameObject finishPanel = null;
	[SerializeField] private GameObject finishBackground = null;
	[SerializeField] private GameObject gameOverPanel = null;
	[SerializeField] private Text clockText = null;
	[SerializeField] private Text finishClockText = null;
	[SerializeField] private GameObject[] stars = null;

	[SerializeField] private Image clockImageFilled = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ActiveGameOverPanel(){
		gameOverPanel.SetActive(true);
		gameOverPanel.GetComponent<Animator>().SetTrigger("PopUp");
		
	}

	public void ActiveFinishPanel(){
		finishPanel.SetActive(true);
		finishBackground.SetActive(true);
		finishPanel.GetComponent<Animator>().SetTrigger("PopUp");
		finishClockText.text = clockText.text;

		//Pop Up Stars
		StartCoroutine("PopUpStars");
	}

	IEnumerator PopUpStars(){
		yield return new WaitForSeconds(0.5f);
		int i = 5;
		foreach (GameObject star in stars)
		{
			yield return new WaitForSeconds(0.5f);
			star.GetComponent<Animator>().SetTrigger("PopUp");
			AudioShouter.Instance.ShoutClip(i);
			i++;
		}

		//Fanfare Win
		AudioShouter.Instance.ShoutClip(8);
		//Active ConfettiShower
		SFXManager.Instance.ActiveConfettiShower();

	}

	public void UpdateClockText(){
		clockText.text = TimeManager.Instance.GetTimeString();
	}

	public void SetClockTextToZero(){
		clockText.text = "00:00";
	}

	public void UpdateClockImageFilled(float timeLimit, float time){
		clockImageFilled.gameObject.SetActive(true);
		clockImageFilled.fillAmount = time / timeLimit;
		// Debug.Log("R: " + time / timeLimit);
	}
}
