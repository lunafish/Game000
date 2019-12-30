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

    AICtrl[] AICtrls;

    // Start is called before the first frame update
    void Start()
    {
        CurrentWave = 0;

        // 
        AICtrls = GameObject.FindObjectsOfType<AICtrl>();

        for (int i = 0; i < AICtrls.Length; i++)
        {
            if (AICtrls[i].Stage == Stage && AICtrls[i].Wave != CurrentWave)
            {
                AICtrls[i].gameObject.SetActive(false);
            }
            else if(AICtrls[i].Stage == Stage && AICtrls[i].Wave == CurrentWave)
            {
                AICtrls[i].gameObject.SetActive(true);
                Vector3 Pos = AICtrls[i].transform.position;
                Pos.y = 0;
                AICtrls[i].transform.position = Pos;
            }
        }
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
        int Count = 0;
        for(int i = 0; i < AICtrls.Length; i++)
        {
            if(AICtrls[i] != null && AICtrls[i].Stage == Stage && AICtrls[i].Wave == CurrentWave)
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
            else
            {
                for (int i = 0; i < AICtrls.Length; i++)
                {
                    if (AICtrls[i] != null && AICtrls[i].Stage == Stage && AICtrls[i].Wave == CurrentWave)
                    {
                        AICtrls[i].gameObject.SetActive(true);
                        Vector3 Pos = AICtrls[i].transform.position;
                        Pos.y = 0;
                        AICtrls[i].transform.position = Pos;
                    }
                }
            }
        }
    }
}