using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swod_data_base : MonoBehaviour
{
    public GameObject[] swod_datas;//아이템 모음집 인덱스가 아이디임 ex: 1번 무기 데이터
                                     // Start is called before the first frame update

    public swod_data return_find_item_data(int id_temp)
    {
        if (swod_datas[id_temp] != null)
        {
            swod_data item_Data_temp = swod_datas[id_temp].GetComponent<swod_data>();
            return item_Data_temp;
        }
        return null;
    }
}
