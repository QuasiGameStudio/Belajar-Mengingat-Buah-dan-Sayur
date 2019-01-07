using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileManager : Singleton<TileManager> {

	[SerializeField] private Sprite[] fruitSprites;

	// [SerializeField] private GameObject[] boards;
	[SerializeField] private Transform boardParent;

	private GameObject[] tiles;

	private int[] arrNumbers = new int[98]; // your fruit sprites 
	private int[] arrTileNumbers;

	// Use this for initialization
	void Start () {

		SpawnBoard();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnBoard(){
		// GameObject board = Instantiate(boards[GameData.Instance.GetLevelNum()]);
		// board.transform.parent = boardParent;
		GameObject board = null;
		for (int i = 0; i < boardParent.childCount; i++)
		{
			if(GameData.Instance.GetLevelNum() != i){
				Destroy(boardParent.GetChild(i).gameObject);
			} else {
				board = boardParent.GetChild(i).gameObject;
				// Debug.Log(board.name);
			}
		}
		
		//board.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
		// board.GetComponent<RectTransform>().anchoredPosition = boardParent.GetComponent<RectTransform>().anchoredPosition;

		//Set arrNumber
		for (int i = 0; i < arrNumbers.Length; i++)
		{
			arrNumbers[i] = i;			
		}

		//random arrNumber order
		RandomIntArrayOrder(arrNumbers);
		
		//Get all tiles
		// tiles = GameObject.FindGameObjectsWithTag("Tile");
		int nRow = board.transform.GetChild(0).childCount;
		int nCol = board.transform.GetChild(0).GetChild(0).childCount;
		tiles = new GameObject[nRow*nCol];
		// Debug.Log(nRow);
		// Debug.Log(nCol);
		// Debug.Log(nRow*nCol);
		
		int x = 0;
		for (int i = 0; i < nRow; i++)
		{
			for (int j= 0; j < nCol; j++)
			{
				tiles[x] = board.transform.GetChild(0).GetChild(i).GetChild(j).gameObject;
				x++;
			}
		}


		//Set arrTileNumbers Size
		arrTileNumbers = new int [tiles.Length];	
		// Debug.Log(tiles.Length);

		for (int i = 0; i < arrTileNumbers.Length; i+=2)
		{
			for (int j = 0; j < 2; j++)
			{								
				arrTileNumbers[i + j] = arrNumbers[i];			
			}
		}
		RandomIntArrayOrder(arrTileNumbers);

		//Set Game Goals
		GameManager.Instance.SetGoals(tiles.Length/2);


		
        //Set arrTileNumbers
		for (int i = 0; i < tiles.Length; i++)
		{
							
			tiles[i].GetComponent<Tile>().SetNumber(arrTileNumbers[i]);	
										
			//tile
			tiles[i].GetComponent<Tile>().SetMainSprite(fruitSprites[arrTileNumbers[i]]);
			tiles[i].GetComponent<Tile>().SetNumber(arrTileNumbers[i]);
		}

		StartCoroutine("PopUpTiles");
		
	}

	IEnumerator PopUpTiles(){
		foreach (GameObject tile in tiles)
		{
			tile.GetComponent<Animator>().SetTrigger("PopUp");
			yield return new WaitForSeconds(0.1f);
			AudioShouter.Instance.ShoutClip(0);
		}

	}

	public void CloseAllTiles(){
		foreach (GameObject tile in tiles)
		{
			tile.GetComponent<Tile>().CloseTile();
		}
	}

	void RandomIntArrayOrder(int[] arr){
		for (int i = 0; i < 200; i++){
			int r = Random.Range(0,arr.Length - 1);
			int r_ = Random.Range(0,arr.Length - 1);
			
			int tempIndex = arr[r];
			arr[r] = arr[r_];
			arr[r_] = tempIndex;
		}

		// // Show array value
		// for (int i = 0; i < arr.Length; i++){
		// 	Debug.Log(arr[i]);
		// }
	}
}
