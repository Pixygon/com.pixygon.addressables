using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;
using Pixygon.DebugTool;

namespace Pixygon.Addressable {
    public class AddressableLoader : MonoBehaviour {
        public static async Task<GameObject> LoadGameObject(AssetReference reference, Transform parent = null, bool addSelfCleanup = true, Action<float> percentage = null) {
            GameObject go = null;
            if(reference == null) {
                Log.DebugMessage(DebugGroup.Addressable, "AssetReference is null!");
                return null;
            }
            try {
                var obj = Addressables.InstantiateAsync(reference);
                Log.DebugMessage(DebugGroup.Addressable, "Loading asset: " + reference + " valid: "+ obj.IsValid());
                percentage?.Invoke(obj.PercentComplete);
                obj.Completed += obj => {
                    switch (obj.Status) {
                        case AsyncOperationStatus.Succeeded: {
                            go = obj.Result;
                            if (go == null) return;
                            if (addSelfCleanup) go.AddComponent<SelfCleanup>();
                            if (parent == null) return;
                            go.transform.SetParent(parent, false);
                            go.transform.localPosition = Vector3.zero;
                            break;
                        }
                        case AsyncOperationStatus.None:
                            Log.DebugMessage(DebugGroup.Addressable, "Failed to load the asset??" + obj.OperationException);
                            break;
                        case AsyncOperationStatus.Failed:
                            Log.DebugMessage(DebugGroup.Addressable, "Failed to load the asset!!" + obj.OperationException);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                };
                Log.DebugMessage(DebugGroup.Addressable, "Loading 3");
                await obj.Task;
                Log.DebugMessage(DebugGroup.Addressable, "Loading 4");
                return go;
            } catch(Exception e) {
                Log.DebugMessage(DebugGroup.Addressable, "Failed to find asset!\n" + e);
                return null;
            }
        }
        public static async Task<T> LoadAsset<T>(AssetReference reference, Action<float> percentage = null) where T : Object {
            T o = null;
            var obj = Addressables.LoadAssetAsync<T>(reference);
            percentage?.Invoke(obj.PercentComplete);
            obj.Completed += obj => { o = obj.Result; };
            while (o == null) await Task.Yield();
            return o;
        }
        
        public static async void SetIcon(GameObject loadIcon, Image image, AssetReference r) {
            loadIcon.SetActive(true);
            image.color = Color.clear;
            image.sprite = await LoadAsset<Sprite>(r);
            image.color = Color.white;
            loadIcon.SetActive(false);
        }
    }
}