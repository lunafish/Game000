using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public float Speed = 5.0f;
    public float LifeTime = 1.0f;
    public Vector3 Direction = new Vector3(0, 0, 1);

    float Delta;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        UpdateCrash();
    }

    void UpdatePosition()
    {
        Delta += Time.deltaTime;
        if (Delta > LifeTime)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 Pos = Direction * (Time.deltaTime * Speed);

        transform.localPosition += Pos;
    }

    void UpdateCrash()
    {
        AICtrl[] Ctrls = GameObject.FindObjectsOfType<AICtrl>();

        for(int i = 0; i < Ctrls.Length; i++)
        {
            Vector3 Pos = Ctrls[i].transform.position - transform.position;

            if(Pos.magnitude < 1.0f)
            {
                Ctrls[i].Crash(gameObject);
                Destroy(gameObject);
            }
        }
    }

    public void Init()
    {
        Delta = 0.0f;
    }
}
