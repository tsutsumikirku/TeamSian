using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource seSource;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField, Header("オーディオクリップを指定してください")] private List<AudioClipData> seClips;
    [SerializeField, Header("BGMのオーディオクリップを指定してください")] private List<AudioClipData> bgmClips;
    public static AudioManager Instance { get; private set; }
    void Start()
    {
        if (!Instance) return;
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    /// <summary>
    /// SEを再生するメソッドもし対応する音が存在していなければfalseを返す存在していればtrueを返して再生する
    /// </summary>
    /// <param name="seName"></param>
    /// <returns></returns>
    public bool TryPlaySE(string seName)
    {
        if (seClips.Count == 0)
        {
            Debug.LogWarning("SEクリップが設定されていません。");
            return false;
        }
        foreach (var seClip in seClips)
        {
            if (seClip.name == seName)
            {
                seSource.PlayOneShot(seClip.clip);
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// BGMを再生するメソッドもし対応する音が存在していなければfalseを返す存在していればtrueを返して再生する
    /// </summary>
    /// <param name="bgmName"></param>
    /// <returns></returns>
    public bool TryPlayBGM(string bgmName)
    {
        if (bgmClips.Count == 0)
        {
            Debug.LogWarning("BGMクリップが設定されていません。");
            return false;
        }
        foreach (var bgmClip in bgmClips)
        {
            if (bgmClip.name == bgmName)
            {
                bgmSource.clip = bgmClip.clip;
                bgmSource.Play();
                return true;
            }
        }
        return false;
    }
}
[System.Serializable]
public class AudioClipData
{
    public string name;
    public AudioClip clip;
}
