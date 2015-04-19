using UnityEngine;
using System.Collections;

public class FireballBehaviour : MonoBehaviour {

	public bool GoingRight = true;
	public float Speed = 20.0f;
	public GameObject Owner;
	public GameObject FireballDeathPrefab;
	public float HorizontalBounds = 6.0f;

	private Rigidbody2D m_rigidBody;
	private SpriteRenderer m_spriteRenderer;

	// Use this for initialization
	void Start ()
	{
		if (Owner == null)
			Debug.LogError("Has no owner");

		m_rigidBody = GetComponent<Rigidbody2D>();
		m_spriteRenderer = GetComponent<SpriteRenderer>();
		if (GoingRight)
		{
			m_rigidBody.AddForce(new Vector2(Speed,0));
		}
		else
		{
			transform.localScale += new Vector3(-(transform.localScale.x * 2), 0, 0); // mirror on x
			m_rigidBody.AddForce(new Vector2(-Speed,0));
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (transform.position.x < -HorizontalBounds || transform.position.x > HorizontalBounds)
			Destroy(gameObject);
	}

	void  OnTriggerEnter2D(Collider2D collision)
	{
		var otherFireball = collision.gameObject.GetComponent<FireballBehaviour>();
		if (otherFireball != null && otherFireball.Owner != Owner)
		{
			Destroy(gameObject);

			// only one of the wizards should spawn the death animation so we only get one
			if (Owner.name == "WizardGreen")
				SpawnFireballDeathEffect();
		}
		var wizard = collision.gameObject.GetComponent<WizardBehaviour>();
		if (wizard != null && wizard.gameObject != Owner)
		{
			wizard.GetComponent<WizardDeathBehaviour>().enabled = true;
			Destroy(gameObject);
			SpawnFireballDeathEffect();
		}
	}

	void SpawnFireballDeathEffect()
	{
		var bounds = m_spriteRenderer.bounds;
		var y = bounds.center.y;
		var x = bounds.center.x + (bounds.size.x * .5f);
		Instantiate(FireballDeathPrefab, new Vector3(x, y, this.transform.position.z), Quaternion.identity);
	}
}
