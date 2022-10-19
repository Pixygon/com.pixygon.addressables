using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Pixygon.Addressable {
    public class AddressableSceneLoader : MonoBehaviour {
        public static AsyncOperationHandle<SceneInstance> LoadSceneOperation(string sceneName, bool additive = false) {
            var handle1 =
                Addressables.LoadSceneAsync("Scenes/" + sceneName, additive ? LoadSceneMode.Additive : LoadSceneMode.Single);
            handle1.Completed += obj => {
                if(obj.Status != AsyncOperationStatus.Succeeded)
                    Debug.Log("Something went wrong while loading the scene... " + obj.Status);
            };
            return handle1;
        }
        public static AsyncOperationHandle UnloadSceneOperation(AsyncOperationHandle<SceneInstance> scene) {
            var handle1 = Addressables.UnloadSceneAsync(scene);
            handle1.Completed += obj => {
                if(obj.Status == AsyncOperationStatus.Succeeded)
                    Resources.UnloadUnusedAssets();
            };
            return handle1;
        }
    }
}