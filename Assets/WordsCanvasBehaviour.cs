using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;

public class WordsCanvasBehaviour : MonoBehaviour {

	public Object WordPrefab;
	public int TargetWords = 1;
	public float SpawnWidth = 100f;
	public float SpawnY = .0f;
	public float SpawnHeight = 100f;
	public bool IsInTutorial = true;


	// Use this for initialization
	void Start ()
	{
		transform.position = Camera.main.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (IsInTutorial)
		{
			if (GameObject.FindGameObjectsWithTag("Word").Length == 0)
				SpawnWord("start", new Vector3(0, SpawnHeight, 0));
		}
		else
		{
			var currentWords = GameObject.FindGameObjectsWithTag("Word");
			var wordBehaviours = currentWords.Select(x => x.GetComponent<TextHelper>()).Where(x => !x.IsDying).ToArray();
			//Debug.Log("Current word count: " + currentWords.Length);
			for (int i = wordBehaviours.Length; i < TargetWords; i++)
			{
				//SpawnWord("Cat", new Vector3(SpawnWidth, .0f, .0f), 0);
				SpawnWord(GetRandomWord(), GetRandomSpawnPosition());
			}
		}
	}

	public void RemoveAllWords()
	{
		foreach (var go in GameObject.FindGameObjectsWithTag("Word"))
		{
			Destroy(go);
		}
	}

	public void SpawnWord(string text, Vector3 position)
	{
		Debug.Log("Spawning word: " + text + " at " + position);
		var spawned = Instantiate(WordPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		spawned.transform.position = position;
		spawned.transform.SetParent(transform, false);
		spawned.GetComponent<TextHelper>().Text = text.ToLower();
	}

	private Vector3 GetRandomSpawnPosition()
	{
		var x = Random.Range(-SpawnWidth, SpawnWidth);
		var y = Random.Range(SpawnY - SpawnHeight, SpawnY + SpawnHeight);
		return new Vector3(x, y, .0f);
	}

	private string GetRandomWord()
	{
		var words = Dictionary.Words;
		var i = Random.Range(0, words.Length);
		return words[i].ToLower();
	}
}
