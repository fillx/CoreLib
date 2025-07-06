using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Profiling;
using UnityEngine.UI;

namespace Assets.Tests
{
    public class AssetLoadedTest : MonoBehaviour
    {
        public AssetReference Reference;
        public Image Image;
        
        IAssetService assetService;
        IAssetProvider assetProvider;
        private void Start()
        {
            assetProvider = new AddressablesAssetProvider();
            assetService = new AssetService(assetProvider);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                LoadIcon();
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                StartCoroutine(LogBeforeAndAfterUnload());
            }
        }

        public async Task LoadIcon()
        {
            AssetReference assetRef = Reference;
            using var handle = await assetService.LoadAsync<Sprite>(Reference.AssetGUID);
            var icon = handle.Asset;
            Image.sprite = icon;
        }
        
        public IEnumerator LogBeforeAndAfterUnload()
        {
            // Лог до очистки
            LogMemoryUsage("Before");

            Debug.Log($"Textures: {Resources.FindObjectsOfTypeAll<Texture2D>().Length}");
            Debug.Log($"Sprites: {Resources.FindObjectsOfTypeAll<Sprite>().Length}");
            Debug.Log($"AudioClips: {Resources.FindObjectsOfTypeAll<AudioClip>().Length}");
            Debug.Log($"GameObjects: {Resources.FindObjectsOfTypeAll<GameObject>().Length}");

            // Очистка неиспользуемых ресурсов
           // Image.sprite = null;
            yield return Resources.UnloadUnusedAssets();
            yield return null;

            // Лог после очистки
            LogMemoryUsage("After");

            Debug.Log($"Textures: {Resources.FindObjectsOfTypeAll<Texture2D>().Length}");
            Debug.Log($"Sprites: {Resources.FindObjectsOfTypeAll<Sprite>().Length}");
            Debug.Log($"AudioClips: {Resources.FindObjectsOfTypeAll<AudioClip>().Length}");
            Debug.Log($"GameObjects: {Resources.FindObjectsOfTypeAll<GameObject>().Length}");
        }

        private void LogMemoryUsage(string label)
        {
            long totalMemory = Profiler.GetTotalAllocatedMemoryLong();
            long reservedMemory = Profiler.GetTotalReservedMemoryLong();
            long monoMemory = Profiler.GetMonoUsedSizeLong();

            Debug.Log($"{label} Memory Usage:");
            Debug.Log($"Total Allocated: {totalMemory / (1024f * 1024f):F2} MB");
            Debug.Log($"Reserved: {reservedMemory / (1024f * 1024f):F2} MB");
            Debug.Log($"Mono Used: {monoMemory / (1024f * 1024f):F2} MB");
        }
    }
}