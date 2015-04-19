using UnityEngine;
using System.Collections;

public class EnemyWizardBehaviour : MonoBehaviour {

	public float DifficultyLevel = .5f;
	public float TimeBetweenIncreasedDifficulty = 5.0f; // TODO not used yet

	private WizardBehaviour m_wizardBehaviour;

	// Use this for initialization
	void Start ()
	{
		m_wizardBehaviour = GetComponent<WizardBehaviour>();
		m_wizardBehaviour.SetWizardType(false, false);
		StartCoroutine(AttackRoutine());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private IEnumerator AttackRoutine()
	{
		while (enabled)
		{
			m_wizardBehaviour.Scream();
			yield return new WaitForSeconds(DifficultyLevel * 2f);
		}
	}

}
