using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : Singleton<TimeManager> {

	private bool timeCountDown;
	private bool timeIsRunning;
	private float time;
	private float minutes;
	private float seconds;	
	private string timeString;	
	

	// [SerializeField]
	// private AudioClip timerClip;

	//[SerializeField]
	//private Color redColorTime;

	// Use this for initialization
	void Start () {
		
		minutes = Mathf.Floor (time / 60);
		seconds = time % 60;
		timeString = Mathf.RoundToInt (minutes).ToString ("00") + ":" + Mathf.RoundToInt (seconds).ToString ("00");

	}
		
	// Update is called once per frame
	void Update () {

		if(timeIsRunning){
			
			if(timeCountDown){
				
				CountDown();
			} else {
				CountUp();
			}
			
			timeString = Mathf.RoundToInt (minutes).ToString ("00") + ":" + Mathf.RoundToInt (seconds).ToString ("00");
		}
		

		// if (!GetComponent<AudioSource> ().isPlaying) {
		// 	GetComponent<AudioSource> ().clip = timerClip;
		// 	GetComponent<AudioSource> ().Play ();
		// }				

				
	}		

	void CountDown(){
			time -= Time.deltaTime;
			minutes = Mathf.Floor (time / 60);
			seconds = time % 60;	
	
	}

	void CountUp(){
		time += Time.deltaTime;
		minutes = Mathf.Floor (time / 60);
		seconds = time % 60;	
	}

	public string FloatTimeToString(float time){		
		float minutes = Mathf.Floor (time / 60);
		float seconds = time % 60;
		return Mathf.RoundToInt (minutes).ToString ("00") + ":" + Mathf.RoundToInt (seconds).ToString ("00");
	}

	public string GetTimeString (){
		return timeString;
	}

	public float GetTime (){
		return time;
	}

	public void ResetTime(){
		time = 0;
	}

	public void SetTimeIsRunning(bool value){
		timeIsRunning = value;
	}

	public bool GetTimeIsRunning(){
		return timeIsRunning;
	}

	public void SetTimeCountDown(bool value, float time){
		timeCountDown = value;
		this.time = time;
	}

}
