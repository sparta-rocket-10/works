using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoop : MonoBehaviour


{

	

	// Start is called before the first frame update
	void Start()
    {
        float x = Random.Range(-2.0f, 2.0f);
        float y = Random.Range(-6.0f, -6.1f);

        transform.position = new Vector3(x, y, 0);
    }

    // Update is called once per frame
    void Update()
    {//새로운 위치 = 현재위치 + 방향 * 속도 
        transform.position = transform.position + new Vector3(0, 2, 0) * Time.deltaTime;

		

	}
}
