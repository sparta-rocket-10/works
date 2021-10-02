using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
	public GameObject hoop;
	public GameObject newHoop;
	
	#region Singleton class: GameManager



	// Sprite Renderer component 사용하기 위한 선언,초기화
	//public SpriteRenderer renderer;

	public static GameManager Instance;
	public Text ScoreText;

	//gamdobject bg1,2,3를 받아온다. 
	public GameObject bg1;
	public GameObject bg2;
	public GameObject bg3;


	

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

		//Sprite Renderer component 사용하기 위한  초기화
		//renderer = GetComponent<SpriteRenderer>();
	}


	public void floating()
    {
		GameObject newHoop = Instantiate(hoop);
		//Instantiate(hoop);
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

		//유니티 다른 스크립트에 있는 변수에 접근하기
		//GameObJect.Find(스크립트를 포함하는 오브젝트이름).GetComponent<스크립트 이름>().변수

		if (GameObject.Find("Ball").GetComponent<Ball>().Coin< 1)
		{
			Debug.Log("1");
			//검색해서 하는 방법 코드를 가져와서 사용하기
			//rain스크립트의 오른쪽 화면 component에서 spriterenderer에 color를 수정하는것. 
			//GetComponent<SpriteRenderer>().color = new Color(100 / 255.0f, 100 / 255.0f, 255 / 255.0f, 255 / 255.0f);
			
			//GameObject.Find("bg1").GetComponent<SpriteRenderer>().set
			bg1.SetActive(true);
			bg2.SetActive(false);
			bg3.SetActive(false);

			//.SetActive(true);

			
			newHoop.transform.Find("hoop1").gameObject.SetActive(true);
			newHoop.transform.Find("hoop1_1").gameObject.SetActive(false);
			//GameObject.FindGameObjectWithTag("hoop1").SetActive(true);
			//GameObject.FindGameObjectWithTag("hoop1_1").SetActive(false);
			//hoop.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
			//hoop1.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
			//Prefabs.Getchild(0).gameObject;

			//GameObject의 부로 hoop -> 자식 hoop1으로 들어가는건 됨. 
			//GameObject.Find("hoop").transform.Find("hoop1").gameObject.SetActive(false);
			//GameObject.Find("hoop").transform.Find("hoop1_1").gameObject.SetActive(false);
			//GameObject.Find("hoop").transform.Find("hoop1_1").gameObject.SetActive(false);

			//Null 이 발생하는건  hoop1이 정의안되서 그런거같다. 

			//GameObject.Find("hoop1").gameObject.SetActive(false);
			//GameObject.Find("hoop1_1").gameObject.SetActive(true);


			//GameObject.Find("hoop").transform.GetChild(3).gameObject.SetActive(true);
			//GameObject.Find("hoop").transform.GetChild(5).gameObject.SetActive(false);


			//.getchild(0).setactive(true);

		}
		if (GameObject.Find("Ball").GetComponent<Ball>().Coin > 1 && GameObject.Find("Ball").GetComponent<Ball>().Coin < 3)
		{
			Debug.Log("2");
			bg2.SetActive(true);
			bg3.SetActive(false);
			bg1.SetActive(false);

			//GameObject.Find("hoop").transform.Find("hoop1").gameObject.SetActive(true);
			//GameObject.Find("hoop").transform.Find("hoop1_1").gameObject.SetActive(false);
			newHoop.transform.Find("hoop1").gameObject.SetActive(false);
			newHoop.transform.Find("hoop1_1").gameObject.SetActive(false);
		}
		if (GameObject.Find("Ball").GetComponent<Ball>().Coin > 3)
		{
			Debug.Log("3");
			bg3.SetActive(true);
			bg2.SetActive(false);
			bg1.SetActive(false);

			//GameObject.Find("hoop").transform.Find("hoop1").gameObject.SetActive(false);
			//GameObject.Find("hoop").transform.Find("hoop1_1").gameObject.SetActive(true);
			newHoop.transform.Find("hoop1").gameObject.SetActive(false);
			newHoop.transform.Find("hoop1_1").gameObject.SetActive(true);
		}


	}
	//score---------------
	void addScore(int score)
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