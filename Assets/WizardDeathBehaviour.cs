using UnityEngine;
using System.Collections;

public class WizardDeathBehaviour : MonoBehaviour {

	// this script should start disabled

	public float Duration = 5.0f;
	public AudioClip DeathSound;
	public AudioClip FinishSound;

	private SpriteRenderer m_spriteRenderer;
	private float m_startTime;
	private bool m_started;
	private Vector3 m_startPosition;
	private Vector3 m_targetPosition;

	void Start()
	{
		m_spriteRenderer = GetComponent<SpriteRenderer>();
		m_startPosition = transform.position;
		
		var height = m_spriteRenderer.bounds.size.y * 4f ;
		m_targetPosition = m_startPosition + new Vector3(0, height, 0);
		Debug.Log(m_startPosition);
		Debug.Log (m_targetPosition);
	}

	void Update()
	{
		if (!m_started)
		{
			m_started = true;
			m_startTime = Time.time;
			AudioSource.PlayClipAtPoint(DeathSound, Vector3.zero);
		}
		float t = (Time.time - m_startTime) / Duration;
		m_spriteRenderer.color = Color.Lerp(m_spriteRenderer.color, new Color(1f, 0f, 0f, 0f), t);
		transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, t);

		Debug.Log(t);
		if (t >= .1f)
		{
			Debug.Log("blap");
			AudioSource.PlayClipAtPoint(FinishSound, Vector3.zero);
			enabled = false;

			Debug.Log(gameObject.name);

			if (gameObject.name.Contains("Purple"))
			{
				GameObject.Find("GameMaster").GetComponent<GameMasterBehaviour>().Win();
			}
			else
			{
				GameObject.Find("GameMaster").GetComponent<GameMasterBehaviour>().Lose();
			}
		}
	}
}

