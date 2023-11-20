using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

public enum Sound_Type
{
    None, Master, BGM, SFX, UI
}

public class SoundPanel : Panel
{
    [SerializeField]
    private Slider _masterSoundSlider;
    [SerializeField]
    private Slider _bgmSoundSlider;
    [SerializeField]
    private Slider _sfxSoundSlider;
    [SerializeField]
    private Slider _uiSoundSlider;

    [SerializeField]
    private AudioMixer _masterMixer;

    private Dictionary<Sound_Type, string> soundName = new Dictionary<Sound_Type, string>();
    private readonly float _minValue = -60;
    private readonly float _maxValue = 20;

    protected override void Awake()
    {
        base.Awake();

        InitValue();
    }

    private void InitValue()
    {
        // Init Dictinary Value
        soundName.Add(Sound_Type.Master, "_Master");
        soundName.Add(Sound_Type.BGM, "_BGM");
        soundName.Add(Sound_Type.SFX, "_SFX");
        soundName.Add(Sound_Type.UI, "_UI");

        // Init Slider OnChanged
        _masterSoundSlider.onValueChanged.AddListener((value) => { SetSound(Sound_Type.Master, value); });
        _bgmSoundSlider.onValueChanged.AddListener((value) => { SetSound(Sound_Type.BGM, value); });
        _sfxSoundSlider.onValueChanged.AddListener((value) => { SetSound(Sound_Type.SFX, value); });
        _uiSoundSlider.onValueChanged.AddListener((value) => { SetSound(Sound_Type.UI, value); });

        // Get Saved Json Value
        // Set Value
        // Slider Set Value
        
        //DataManager.Instance.soundData.BGMSoundVal;
        //DataManager.Instance.soundData.EffSoundVal;

    }

    private void SetSound(Sound_Type type, float value)
    {
        float soundValue = Mathf.Lerp(_minValue, _maxValue, value);
        _masterMixer.SetFloat(soundName[type], soundValue);
    }

    public override void ShowOff()
    {
        base.ShowOff();
        DataManager.Instance.SaveOption();
    }
}
