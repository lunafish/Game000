using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCtrl : MonoBehaviour
{
    public int Stage = 0;
    public int Wave = 0;
    public Transform NextStage;

    int CurrentWave = 0;
    bool MoveNextStage = false;
    float NextDelta;

    // Start is called before the first frame update
    void Start()
    {
        CurrentWave = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(MoveNextStage)
        {
            ProcessNextStage();
        }
        else
        {
            ProcessWave();
        }
    }

    void ProcessNextStage()
    {
        NextDelta -= Time.deltaTime;
        if(NextDelta > 0)
        {
            return;
        }

        Debug.Log("Move Stage");

        PlayerCtrl Player = GameObject.FindObjectOfType<PlayerCtrl>();
        if (Player != null)
        {
            Player.MoveNextStage(NextStage.position);
        }

        Destroy(gameObject);
    }

    void ProcessWave()
    {
        AICtrl[] Ctrls = GameObject.FindObjectsOfType<AICtrl>();

        int Count = 0;
        for(int i = 0; i < Ctrls.Length; i++)
        {
            if(Ctrls[i].Stage == Stage && Ctrls[i].Wave == CurrentWave)
            {
                Count++;
            }
        }

        if(Count == 0)
        {
            // wave clear
            CurrentWave++;
            if(CurrentWave > Wave)
            {
                Debug.Log("Clear Stage");
                MoveNextStage = true;
                NextDelta = 1.0f;
            }
        }
    }
}
