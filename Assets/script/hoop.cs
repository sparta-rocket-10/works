using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float x = Random.Range(-2.7f, 2.7f);
        float y = Random.Range(-6.0f, -6.1f);

        transform.position = new Vector3(x, y, 0);
    }

    // Update is called once per frame
    void Update()
    {//���ο� ��ġ = ������ġ + ���� * �ӵ� 
        transform.position = transform.position + new Vector3(0, 1, 0) * Time.deltaTime;
    }
}
