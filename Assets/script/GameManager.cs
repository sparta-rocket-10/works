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

	//싱글톤이라는 디자인패턴. 하나밖에 존재할수밖에 없다. 어디서든 불러 쓸수 있다. 
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
		//totalScore를 문자열로 바꿔서 입력
		ScoreText.text = totalScore.ToString();
	}



	//-Drag--------------------------------------
	void OnDragStart()
	{
		//공이 멈추게 만들어버림.
		ball.DesactivateRb();
		startPoint = cam.ScreenToWorldPoint(Input.mousePosition);

		//공 점점커지는게 보인다. 
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