using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#region 사운드데이터클래스
public class SoundData
{
    public float BGMSoundVal = .5f;
    public float EffSoundVal = .5f;
}
#endregion

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    #region 구조체 및 변수 선언
    public SoundData soundData = new SoundData();
    #endregion

    #region 경로 변수 선언
    private string _path;
    private string _soundFileName = "/SoundData.json";
    #endregion

    private void Awake()
    {
        #region 싱글톤
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

    #region 데이터로드
    public void JsonLoad()
    {
        #region 파일이 없으면 초기화
        if (!GetDir())
        {
            Directory.CreateDirectory(_path);
            soundData.BGMSoundVal = 0.5f;
            soundData.EffSoundVal = 0.5f;
            SaveOption();

        }
        #endregion
        #region 파일이 있으면 데이터 로드
        else
        {
            LoadOption();
        }
        #endregion
    }
    #endregion

    #region 사운드옵션
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
