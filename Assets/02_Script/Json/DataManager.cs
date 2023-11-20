using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#region ���嵥����Ŭ����
public class SoundData
{
    public float BGMSoundVal = .5f;
    public float EffSoundVal = .5f;
}
#endregion

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    #region ����ü �� ���� ����
    public SoundData soundData = new SoundData();
    #endregion

    #region ��� ���� ����
    private string _path;
    private string _soundFileName = "/SoundData.json";
    #endregion

    private void Awake()
    {
        #region �̱���
        if (Instance == null)
        {
            Instance = FindObjectOfType<DataManager>();
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);

        #endregion

        _path = Path.Combine(Application.persistentDataPath, "save");

        JsonLoad();
    }

    #region �����ͷε�
    public void JsonLoad()
    {
        #region ������ ������ �ʱ�ȭ
        if (!GetDir())
        {
            Directory.CreateDirectory(_path);
            soundData.BGMSoundVal = 0.5f;
            soundData.EffSoundVal = 0.5f;
            SaveOption();

        }
        #endregion
        #region ������ ������ ������ �ε�
        else
        {
            LoadOption();
        }
        #endregion
    }
    #endregion

    #region ����ɼ�
    public void SaveOption()
    {
        string data = JsonUtility.ToJson(soundData);
        File.WriteAllText(_path + _soundFileName, data);
    }

    public void LoadOption()
    {
        string data = File.ReadAllText(_path + _soundFileName);
        soundData = JsonUtility.FromJson<SoundData>(data);
    }
    #endregion

    public void DataClear()
    {
        soundData = new SoundData();
        SaveOption();
    }

    public bool GetDir()
    {
        return Directory.Exists(_path);
    }
}
