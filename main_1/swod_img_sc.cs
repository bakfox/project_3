using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class swod_img_sc : MonoBehaviour
{
    public bool upgrade_swoad = false;

    Vector3 pos_temp;
    float delta_temp = 0.5f;
    float spead_temp = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        pos_temp = transform.position;
        StartCoroutine("idle");
    }

    // Update is called once per frame
    IEnumerator idle() //±âº» 
    {
        while (upgrade_swoad == false)
        {
            Vector3 v_pos = pos_temp;
            v_pos.y = pos_temp.y - delta_temp * Mathf.Sin(Time.time * spead_temp);
            transform.position = v_pos;
            yield return new WaitForFixedUpdate();
        }
    }
    
}
