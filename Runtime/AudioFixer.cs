using UnityEngine;

namespace Pixygon.Addressable {
    public class AudioFixer : MonoBehaviour {
        [SerializeField] private AudioSource _source;
        [SerializeField] private MixerGroups _group;

        private void Awake() {
            _source.outputAudioMixerGroup = _group switch {
                MixerGroups.SFX => AudioMaster.Instance.MixerSFX,
                MixerGroups.BGM => AudioMaster.Instance.MixerBGM,
                _ => _source.outputAudioMixerGroup
            };
        }

        public enum MixerGroups {
            SFX,
            BGM
        }
    }
}