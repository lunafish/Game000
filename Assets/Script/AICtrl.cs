using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICtrl : MonoBehaviour
{
    public int Defense = 3;
    public int Stage = 0;
    public int Wave = 0;

    int Damage;

    // Start is called before the first frame update
    void Start()
    {
        Damage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Crash(GameObject Attacker)
    {
        Damage++;

        if(Damage >= Defense)
        {
            for(int i = 0; i < 4; i++)
            {
                GameObject Bullet = (GameObject)Instantiate(Resources.Load("Prefabs/Bullet"));
                if (Bullet != null)
                {
                    Vector3 Dir = Quaternion.Euler(0, 45 + (90 * i), 0) * new Vector3(0, 0, 1);

                    Bullet.transform.position = transform.position + Dir;
                    Bullet.GetComponent<BulletCtrl>().Direction = Dir;
                }
            }

            Destroy(gameObject);
        }
    }
}
