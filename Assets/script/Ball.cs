using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{

	coin coinsManager;

	[HideInInspector] public Rigidbody2D rb;
	[HideInInspector] public CircleCollider2D col;

	[HideInInspector] public Vector3 pos { get { return transform.position; } }



	//------coin managing-------------------------
	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<CircleCollider2D>();
	}

	void Start()
    {
		coinsManager = FindObjectOfType<coin>();
    }


	// OnTriggerEnter2D는 충돌이 일어날때 한번만 호출되는 함수

	private float Coin = 0;

	public TextMeshProUGUI textCoins;


	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform.tag == "coin")
		{
			Coin++;
			textCoins.text = Coin.ToString();
			Destroy(other.gameObject);

		}
	}

	//end coin managing-------------------------------



	public void Push(Vector2 force)
	{
		rb.AddForce(force, ForceMode2D.Impulse);
	}

	public void ActivateRb()
	{
		rb.isKinematic = false;
	}

	public void DesactivateRb()
	{
		rb.velocity = Vector3.zero;
		rb.angularVelocity = 0f;
		rb.isKinematic = true;
	}







	
}