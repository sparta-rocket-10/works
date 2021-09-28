using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	#region Singleton class: GameManager

	public GameObject hoop;
	public static GameManager Instance;
	public Text ScoreText;

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

		InvokeRepeating("floating", 0.0f, 3.5f);
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
	}
	//score---------------
	public void addScore(int score)
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