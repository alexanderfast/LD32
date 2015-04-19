using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class PlayerBehaviour : MonoBehaviour
{
	public GameObject WizardAvatar;
	public AudioClip CharacterEnteredSound;
	public AudioClip WordCompleteSound;

	private WizardBehaviour m_wizardBehaviour;
	private WizardDeathBehaviour m_wizardDeathBehaviour;
	private const string m_alphabet = "abcdefghijklmnopqrstuvxyz";
	private IList<KeyCode> m_keyCodes = new List<KeyCode>();

	void Awake()
	{
		if (WizardAvatar != null)
		{
			m_wizardBehaviour = WizardAvatar.GetComponent<WizardBehaviour>();
			m_wizardDeathBehaviour = WizardAvatar.GetComponent<WizardDeathBehaviour>();
			m_wizardBehaviour.SetWizardType(true, true);
		}

		foreach (var a in m_alphabet)
		{
			try
			{
				m_keyCodes.Add((KeyCode)Enum.Parse(typeof(KeyCode), a.ToString(), true));
			}
			catch (Exception)
			{
				Debug.LogError("Unable to parse: " + a);
			}
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update()
	{
		if (Application.isEditor && Input.GetKeyDown(KeyCode.Space)) // for debugging
		{
			m_wizardBehaviour.Scream();
			//m_wizardDeathBehaviour.enabled = true;
		}

		var pressed = m_keyCodes.Where(x => Input.GetKeyDown(x));
		if (!pressed.Any())
			return;
		//Debug.Log("Pressed: " + string.Join(" ", pressed.Select(x => x.ToString()).ToArray()));
		var words = GameObject.FindGameObjectsWithTag("Word").Select(x => x.GetComponent<TextHelper>());
		foreach (var word in words)
		{
			foreach (var a in pressed)
			{
				var ch = a.ToString().ToLower().ToCharArray().FirstOrDefault();
				if (word.GetNextCharacterToType() ==  ch)
				{
					AudioSource.PlayClipAtPoint(CharacterEnteredSound, Vector3.zero);
				}
				else
				{
					// TODO fail sound
					continue;
				}
				if (word.AcceptCharacter(ch))
				{
					m_wizardBehaviour.Scream();
					AudioSource.PlayClipAtPoint(WordCompleteSound, Vector3.zero);
				}
			}
		}
	}
}
