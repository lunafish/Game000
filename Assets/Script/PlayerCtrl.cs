using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public GameObject CursorPress;
    public GameObject CursorMove;
    public GameObject Camera;
    public float FireTime = 1.0f;
    public float MaxSpeed = 100.0f;

    Vector3 MoveDir;
    Vector3 MousePos;
    float Speed;
    float FireDelta;
    bool MoveNext;
    Vector3 PosNext;

    // Start is called before the first frame update
    void Start()
    {
        Speed = 7.5f;
        CursorPress.SetActive(false);
        CursorMove.SetActive(false);
        FireDelta = 0.0f;
        MoveNext = false;
    }

    bool CheckBound(Vector3 Pos)
    {
        RaycastHit Hit;
        // left
        if(Physics.Raycast(Pos, new Vector3(-1, 0, 0), out Hit, 1))
        {
            if(Hit.transform.gameObject.tag == "Bound")
            {
                return true;
            }
        }
        // right
        if (Physics.Raycast(Pos, new Vector3(1, 0, 0), out Hit, 1))
        {
            if (Hit.transform.gameObject.tag == "Bound")
            {
                return true;
            }
        }
        // up
        if (Physics.Raycast(Pos, new Vector3(0, 0, 1), out Hit, 1))
        {
            if (Hit.transform.gameObject.tag == "Bound")
            {
                return true;
            }
        }
        // down
        if (Physics.Raycast(Pos, new Vector3(0, 0, -1), out Hit, 1))
        {
            if (Hit.transform.gameObject.tag == "Bound")
            {
                return true;
            }
        }



        return false;
    }

    void Move()
    {
        Vector3 Pos = Input.mousePosition - MousePos;
        MoveDir = Pos.normalized;

        if (Pos != Vector3.zero)
        {
            // mouse
            Pos = Input.mousePosition - MousePos;
            float Len = Pos.magnitude;
            if(Len > MaxSpeed)
            {
                Len = MaxSpeed;
            }
            Pos = MousePos + (Pos.normalized * Len);
            Pos.x -= ((float)Screen.width * 0.5f);
            Pos.y -= ((float)Screen.height * 0.5f);

            CursorMove.transform.localPosition = Pos;

            CursorMove.SetActive(true);

            // Move
            Vector3 Mov = MoveDir * Time.deltaTime * (Speed * (Len / MaxSpeed));
            Mov.z = Mov.y;
            Mov.y = 0.0f;

            if(!CheckBound(transform.position + Mov))
            {
                transform.position += Mov;
            }
        }
    }

    void ProcessInput()
    {
        if (MoveNext == true)
        {
            return;
        }

        MoveDir = Vector3.zero;

        if(Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Mouse Down");
            MousePos = Input.mousePosition;

            Vector3 Pos = MousePos;
            Pos.x -= ((float)Screen.width * 0.5f);
            Pos.y -= ((float)Screen.height * 0.5f);

            CursorPress.transform.localPosition = Pos;

            CursorPress.SetActive(true);
        }
        if (Input.GetMouseButton(0))
        {
            //Debug.Log("Mouse Press");

            Move();
        }
        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log("Mouse Up");
            CursorPress.SetActive(false);
            CursorMove.SetActive(false);
        }
    }

    void ProcessBullet()
    {
        FireDelta += Time.deltaTime;

        if(FireDelta > FireTime)
        {

            GameObject Bullet = (GameObject)Instantiate(Resources.Load("Prefabs/Bullet"));
            if (Bullet != null && MoveNext == false)
            {
                Bullet.transform.position = transform.position + new Vector3(0, 0, 1);
            }

            FireDelta = 0.0f;
        }
    }

    void ProcessMoveNext()
    {
        if(MoveNext == false)
        {
            return;
        }

        Vector3 Pos = Camera.transform.position;
        Pos.x = Pos.y = 0;
        Vector3 V = PosNext - Pos;
        if(V.magnitude < 1.0f)
        {
            MoveNext = false;
            return;
        }

        Vector3 Mov =  new Vector3(0, 0, 1) * Time.deltaTime * 30.0f;
        transform.position += Mov;
        Camera.transform.position += Mov;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        ProcessBullet();
        ProcessMoveNext();
    }

    public void MoveNextStage(Vector3 Pos)
    {
        MoveNext = true;
        PosNext = Pos;

        // remove all bullet
        BulletCtrl[] Bullets = GameObject.FindObjectsOfType<BulletCtrl>();
        for(int i = 0; i < Bullets.Length; i++)
        {
            Destroy(Bullets[i].gameObject);
        }
    }
}
