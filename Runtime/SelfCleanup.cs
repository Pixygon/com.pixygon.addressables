using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Pixygon.Addressable {
    public class SelfCleanup : MonoBehaviour {
        private void OnDestroy() {
            Addressables.ReleaseInstance(gameObject);
        }
    }
}