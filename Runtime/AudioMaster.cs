using UnityEngine;
using UnityEngine.Audio;

namespace Pixygon.Addressable {
    public class AudioMaster : MonoBehaviour {
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private AudioListener _listener;
        
        private GameObject _currentListener;

        public static AudioMaster Instance;
        
        public AudioMixerGroup MixerSFX { get; private set; }

        public AudioMixerGroup MixerBGM { get; private set; }

        private void Awake() {
            if (Instance != null) Destroy(this);
            else Instance = this;
            DontDestroyOnLoad(gameObject);
            SetMixer();
        }
        public void SetAudioListener(GameObject g) {
            _currentListener = g;
            _currentListener.AddComponent<AudioListener>();
            _listener.enabled = false;
        }
        public void ReleaseAudioListener() {
            _currentListener = null;
            _listener.enabled = true;
        }

        public void SetMixer() {
            _mixer.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("MasterVolume", 1f)) * 20f);
            _mixer.SetFloat("BGMVolume", Mathf.Log10(PlayerPrefs.GetFloat("BGMVolume", 1f)) * 20f);
            _mixer.SetFloat("SFXVolume", Mathf.Log10(PlayerPrefs.GetFloat("SFXVolume", 1f)) * 20f);
            MixerSFX = _mixer.FindMatchingGroups("SFX")[0];
            MixerBGM = _mixer.FindMatchingGroups("BGM")[0];
        }
    }
}