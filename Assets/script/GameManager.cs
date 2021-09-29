using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	#region Singleton class: GameManager

	public GameObject hoop;

	// Sprite Renderer component ����ϱ� ���� ����,�ʱ�ȭ
	//public SpriteRenderer renderer;
	
	public static GameManager Instance;
	public Text ScoreText;

	//gamdobject bg1,2,3�� �޾ƿ´�. 
	public GameObject bg1;
	public GameObject bg2;
	public GameObject bg3;

	int totalScore = 0;

	//�̱����̶�� ����������. �ϳ��ۿ� �����Ҽ��ۿ� ����. ��𼭵� �ҷ� ���� �ִ�. 
	public static GameManager I;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}


    #endregion

    Camera cam;

	public Ball ball;
	public Ball Coin;

	public Trajectory trajectory;
	[SerializeField] float pushForce = 4f;

	bool isDragging = false;

	Vector2 startPoint;
	Vector2 endPoint;
	Vector2 direction;
	Vector2 force;
	float distance;

	//---------------------------------------
	void Start()
	{
		cam = Camera.main;
		ball.DesactivateRb();

		InvokeRepeating("floating", 0.0f, 2.5f);

		//Sprite Renderer component ����ϱ� ����  �ʱ�ȭ
		//renderer = GetComponent<SpriteRenderer>();
	}


	void floating()
    {
		Instantiate(hoop);
    }


	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			isDragging = true;
			OnDragStart();
		}
		if (Input.GetMouseButtonUp(0))
		{
			isDragging = false;
			OnDragEnd();
		}

		if (isDragging)
		{
			OnDrag();
		}

		//����Ƽ �ٸ� ��ũ��Ʈ�� �ִ� ������ �����ϱ�
		//GameObJect.Find(��ũ��Ʈ�� �����ϴ� ������Ʈ�̸�).GetComponent<��ũ��Ʈ �̸�>().����

		if (GameObject.Find("Ball").GetComponent<Ball>().Coin< 1)
		{
			Debug.Log("1");
			//�˻��ؼ� �ϴ� ��� �ڵ带 �����ͼ� ����ϱ�
			//rain��ũ��Ʈ�� ������ ȭ�� component���� spriterenderer�� color�� �����ϴ°�. 
			//GetComponent<SpriteRenderer>().color = new Color(100 / 255.0f, 100 / 255.0f, 255 / 255.0f, 255 / 255.0f);
			
			//GameObject.Find("bg1").GetComponent<SpriteRenderer>().set
			bg1.SetActive(true);
			bg2.SetActive(false);
			bg3.SetActive(false);
		}
		if (GameObject.Find("Ball").GetComponent<Ball>().Coin > 1 && GameObject.Find("Ball").GetComponent<Ball>().Coin < 3)
		{
			Debug.Log("2");
			bg2.SetActive(true);
			bg3.SetActive(false);
			bg1.SetActive(false);

		}
		if (GameObject.Find("Ball").GetComponent<Ball>().Coin > 3)
		{
			Debug.Log("3");
			bg3.SetActive(true);
			bg2.SetActive(false);
			bg1.SetActive(false);
		}


	}
	//score---------------
	void addScore(int score)
	{
		totalScore += score;
		//Debug.Log(totalScore);
		//totalScore�� ���ڿ��� �ٲ㼭 �Է�
		ScoreText.text = totalScore.ToString();
	}



	//-Drag--------------------------------------
	void OnDragStart()
	{
		//���� ���߰� ��������.
		ball.DesactivateRb();
		startPoint = cam.ScreenToWorldPoint(Input.mousePosition);

		//�� ����Ŀ���°� ���δ�. 
		trajectory.Show();
	}

	void OnDrag()
	{
		endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
		distance = Vector2.Distance(startPoint, endPoint);
		direction = (startPoint - endPoint).normalized;
		force = direction * distance * pushForce;

		//just for debug
		//Debug.DrawLine(startPoint, endPoint);


		trajectory.UpdateDots(ball.pos, force);
	}

	void OnDragEnd()
	{
		//push the ball
		ball.ActivateRb();

		ball.Push(force);

		trajectory.Hide();
	}

}