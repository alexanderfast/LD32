using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextHelper : MonoBehaviour {

	public string Text;

	public bool IsDying { get; private set; }

	private Text m_foreground;
	private Text m_background;
	private int m_progress;

	// Use this for initialization
	void Start ()
	{
		foreach (var text in GetComponentsInChildren<Text>())
		{
			if (text.name == "Foreground")
				m_foreground = text;
			else if (text.name == "Background")
				m_background = text;
		}

		// right now text is always set before start
		m_foreground.text = string.Empty;
		m_background.text = Text;

		StartCoroutine(FadeIn());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public char GetNextCharacterToType()
	{
		if (m_progress >= Text.Length)
			return default(char); // shouldnt happen
		return char.ToLower(Text[m_progress]);
	}
	
	public bool AcceptCharacter(char a)
	{
		if (GetNextCharacterToType() != char.ToLower(a))
			return false;

		m_progress++;
		m_foreground.text = Text.Substring(0, m_progress);

		if (m_progress == Text.Length)
		{
			//Object.Destroy(gameObject);

			if (Text == "start")
			{
				GameObject.Find("GameMaster").GetComponent<GameMasterBehaviour>().EndTutorial();
			}

			StartCoroutine(Die());
			return true;
		}
		return false;
	}

	private IEnumerator FadeIn()
	{
		const float duration = .3f; // seconds
		var start = Time.time;
		for (float progress = .1f; progress < duration; progress = Time.time - start)
		{
			var i = progress / duration;
			transform.localScale = new Vector3(1f, Mathf.Lerp(0f, 1f, i), 1f);
			yield return new WaitForSeconds(.01f);
		}
	}

	private IEnumerator Die()
	{
		IsDying = true;
		const float duration = .9f; // seconds
		var start = Time.time;
		for (float progress = .1f; progress < duration; progress = Time.time - start)
		{
			var i = progress / duration;
			var angle = Mathf.Lerp(0f, 6.28f, i);
			this.transform.Rotate(new Vector3(0,0,1), angle);
			FadeText(m_foreground, i);
			FadeText(m_background, i);
			yield return new WaitForSeconds(.01f);
		}
		Object.Destroy(gameObject);
	}

	private void FadeText(Text text, float amount)
	{
		var c = text.color;
		c = new Color(c.r, c.g, c.b, Mathf.Lerp(1f, 0f, amount));
		text.color = c;
	}
}
