using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class save_sc : MonoBehaviour
{
    public save_user_data save_data = new save_user_data();// 이것만 불러서 사용하면 끝남.

    string data_path;//저장 장소 awake에서 초기화

    save_user_data save_temp = new save_user_data();

    // Start is called before the first frame update
    void Awake()
    {
        data_path = Application.persistentDataPath + "/" + "save_user_data" + ".json";
        Load();
        Save();
    }
    public static save_sc find_save_sc()// 자기 자신 찾기 
    {
        save_sc gm_temp = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<save_sc>();
        return gm_temp;
    }
    // Update is called once per frame
    // 불러오기
    public void Load()
    {
        save_user_data save_temp = new save_user_data();
        if (!File.Exists(data_path))
        {
            Save();
        }
        if (File.Exists(data_path))
        {
            string json = File.ReadAllText(data_path);

            byte[] data =  System.Convert.FromBase64String(json);
            string j_data = System.Text.Encoding.UTF8.GetString(data);

            save_temp = JsonUtility.FromJson<save_user_data>(j_data);
            
        }
        save_data = save_temp;

        return;
    }
    //저장
    public void Save()
    {
        save_temp = save_data;

        string json = JsonUtility.ToJson(save_temp);
        Byte[] data = System.Text.Encoding.UTF8.GetBytes(json);
        string j_data = System.Convert.ToBase64String(data);

        File.WriteAllText(data_path, j_data);
    }
}

[System.Serializable]
public class save_user_data
{

    public int user_now_swoad = 0;//업그레이드 용
    public Int64 user_now_gold = 0;
    public int user_now_have_protect = 0;//보호권 사용 
    public int[] user_sowd_broken_have = new int[3] {0,0,0};//현재 3개 추후 업데이트 예정

}

