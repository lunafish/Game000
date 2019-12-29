using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCtrl : MonoBehaviour
{
    public Material MatTexture;
    public float Speed = 1.0f;

    Vector2 Offset = new Vector2(0, 1);
    Vector2 UVOffset = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UVOffset += Offset * (Time.deltaTime * Speed);
        MatTexture.SetTextureOffset("_MainTex", UVOffset);

        if(UVOffset.y >= 1.0f)
        {
            UVOffset = Vector2.zero;
        }
    }
}
