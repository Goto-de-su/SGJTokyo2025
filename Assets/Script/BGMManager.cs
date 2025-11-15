using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance { get; private set; }

    private AudioSource bgmSource;

    private const string BGM_MUTE_KEY = "BGM_MUTE";   // ?????
    private const string BGM_VOLUME_KEY = "BGM_VOLUME"; // ???

    private void Awake()
    {
        // ???????????? BGMManager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // ??????
        DontDestroyOnLoad(gameObject);

        // ?? AudioSource
        bgmSource = GetComponent<AudioSource>();
        if (bgmSource == null)
        {
            bgmSource = gameObject.AddComponent<AudioSource>();
        }

        // ??????
        bool muted = PlayerPrefs.GetInt(BGM_MUTE_KEY, 0) == 1;
        bgmSource.mute = muted;

        // ?????0~1???? 1
        float volume = PlayerPrefs.HasKey(BGM_VOLUME_KEY)
            ? PlayerPrefs.GetFloat(BGM_VOLUME_KEY)
            : 1f;
        bgmSource.volume = Mathf.Clamp01(volume);

        // ?????
        if (!bgmSource.isPlaying && bgmSource.clip != null)
        {
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void SetBGMEnabled(bool enabled)
    {
        bgmSource.mute = !enabled;
        PlayerPrefs.SetInt(BGM_MUTE_KEY, enabled ? 0 : 1);
        PlayerPrefs.Save();
    }

    public bool IsBGMEnabled()
    {
        return !bgmSource.mute;
    }

    public void ToggleBGM()
    {
        SetBGMEnabled(!IsBGMEnabled());
    }

    // ===== ??????? =====

    public void SetBGMVolume(float volume)
    {
        volume = Mathf.Clamp01(volume); // ??? 0~1
        bgmSource.volume = volume;
        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }

    public float GetBGMVolume()
    {
        return bgmSource.volume;
    }
}
