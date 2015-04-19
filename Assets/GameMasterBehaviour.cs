using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameMasterBehaviour : MonoBehaviour {

	GameObject m_tutorialText;


	// Use this for initialization
	void Start () {
		m_tutorialText = GameObject.Find("TutorialText");
		StartTutorial();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartTutorial()
	{
		ShowText("TutorialText");
	}

	public void EndTutorial()
	{
		Debug.Log("Ending tutorial");
		GameObject.Find("Canvas").GetComponent<WordsCanvasBehaviour>().IsInTutorial = false;
		GameObject.Find("WizardPurple").GetComponent<EnemyWizardBehaviour>().enabled = true;
		ShowText("");
	}

	public void Win()
	{
		Debug.Log("Won");
		StopGame();
		ShowText("VictoryText");
	}

	public void Lose()
	{
		Debug.Log("Lost");
		StopGame();
		ShowText("LoseText");
	}

	private void StopGame()
	{
		GameObject.Find("Player").GetComponent<PlayerBehaviour>().enabled = false;
		GameObject.Find("WizardPurple").GetComponent<EnemyWizardBehaviour>().enabled = false;

		var words = GameObject.Find("Canvas").GetComponent<WordsCanvasBehaviour>();
		words.enabled = false;
		words.RemoveAllWords();
	}

	private void ShowText(string name)
	{
		var textNames = new[] { "TutorialText", "VictoryText", "LoseText" };
		foreach (var i in textNames)
		{
			var text = GameObject.Find(i).GetComponent<Text>();
			text.enabled = i == name;
		}
	}
}
