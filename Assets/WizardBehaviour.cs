using UnityEngine;
using System.Collections;

public class WizardBehaviour : MonoBehaviour {

	public Object FireballPrefab;

	private Animator m_animator;
	private int m_screamTrigger = Animator.StringToHash("Scream");
	private SpriteRenderer m_spriteRenderer;
	private bool m_isPlayer;

	void Awake()
	{
		m_animator = GetComponent<Animator>();
		m_spriteRenderer = GetComponent<SpriteRenderer>();
		SetWizardType(false, false);
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public GameObject GetOpponentWizard()
	{
		if (name.Contains("Green"))
			return GameObject.Find("WizardPurple");
		else
			return GameObject.Find("WizardGreen");
	}

	public void SetWizardType(bool isGreen, bool isPlayer)
	{
		m_isPlayer = isPlayer;

	}

	public void SpawnFireball()
	{
		var p = transform.position;
		var position = new Vector3(p.x, m_spriteRenderer.bounds.center.y, p.z);
		var spawned = (GameObject)Instantiate(FireballPrefab, position, Quaternion.identity);
		var fireball = spawned.GetComponent<FireballBehaviour>();
		fireball.GoingRight = m_isPlayer;
		fireball.Owner = gameObject;
	}

	public void Scream()
	{
		//Debug.Log("Screaming");
		StartCoroutine(ScreamRoutine());
		SpawnFireball();
	}

	public IEnumerator ScreamRoutine()
	{
		m_animator.SetInteger(m_screamTrigger, 1);
		yield return new WaitForSeconds(.8f);
		m_animator.SetInteger(m_screamTrigger, 0);
	}
}
