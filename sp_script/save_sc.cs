using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class save_sc : MonoBehaviour
{
    public save_user_data save_data = new save_user_data();// �̰͸� �ҷ��� ����ϸ� ����.

    string data_path;//���� ��� awake���� �ʱ�ȭ

    save_user_data save_temp = new save_user_data();

    // Start is called before the first frame update
    void Awake()
    {
        data_path = Application.persistentDataPath + "/" + "save_user_data" + ".json";
        Load();
        Save();
    }
    public static save_sc find_save_sc()// �ڱ� �ڽ� ã�� 
    {
        save_sc gm_temp = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<save_sc>();
        return gm_temp;
    }
    // Update is called once per frame
    // �ҷ�����
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
    //����
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

    public int user_now_swoad = 0;//���׷��̵� ��
    public Int64 user_now_gold = 0;
    public int user_now_have_protect = 0;//��ȣ�� ��� 
    public int[] user_sowd_broken_have = new int[3] {0,0,0};//���� 3�� ���� ������Ʈ ����

}

