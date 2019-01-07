using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour {

	[SerializeField] private int number;

	private bool matched;
	private Sprite mainSprite;

	private Image mainImage;

	private bool opened;

	// Use this for initialization
	void Start () {
		mainImage = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
						
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Click(){

		//Clicked Color
		mainImage.transform.parent.GetComponent<Image>().enabled = false;
		

		if(!matched){
			mainImage.sprite = mainSprite;

			if(!opened){
				OpenTile();
				GameManager.Instance.SetOpenedTileNumber(this.gameObject);
			}else{
				CantToClick();
			}

		} else {
			Debug.Log("Tile is Matched");
			CantToClick();
		}
		
	}

	void CantToClick(){
		GetComponent<Animator>().SetTrigger("Shake");
		AudioShouter.Instance.ShoutClip(4);
	}

	public void OpenTile(){
		opened = true;
		mainImage.transform.parent.GetComponent<Animator>().SetBool("Opened",opened);
		
	}
	public void CloseTile(){
		opened = false;
		mainImage.transform.parent.GetComponent<Animator>().SetBool("Opened",opened);
	}

	public void SetMainSprite(Sprite sprite){		
		mainSprite = sprite;
	}

	public void SetNumber(int number){
		this.number = number;
	}
	public int GetNumber(){
		return number;
	}

	public void SetMatched(bool matched){
		this.matched = matched;
	}
	public bool GetMatched(){
		return matched;
	}
}
