using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PerelesoqTest.Infrastructure.AssetManagement;
using PerelesoqTest.Services.Logging;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace PerelesoqTest.Infrastructure.SceneManagement
{
    public class SceneLoader
    {
        private readonly IAssetProvider _assetProvider;
        private readonly ILoggingService _logger;
        private SceneInstance _uiScene;

        public SceneLoader(ILoggingService logger, IAssetProvider assetProvider)
        {
            _logger = logger;
            _assetProvider = assetProvider;
        }
        
        public async Task<SceneInstance> Load(string sceneName, Action<string> onLoaded = null)
        {
            var scene = await _assetProvider.LoadScene(sceneName);
            scene.ActivateAsync();
            
            onLoaded?.Invoke(sceneName);
            
            _logger.LogMessage($"{sceneName} loaded in single mode", nameof(SceneLoader));
            return scene;
        }
        
        public async Task<Dictionary<SceneLayerType, SceneInstance>> LoadSet(string sceneName, Action<string> onLoaded = null)
        {
            var result = new Dictionary<SceneLayerType, SceneInstance>();
            
            foreach (var sceneLayerType in (SceneLayerType[]) Enum.GetValues(typeof(SceneLayerType)))
            {
                var sceneKey = sceneName + (sceneLayerType == SceneLayerType.MAIN ? "" :  $"_{sceneLayerType}");
                
                var scene = await _assetProvider.LoadScene(
                    sceneName: sceneKey,
                    mode: sceneLayerType == SceneLayerType.MAIN 
                        ? LoadSceneMode.Single : LoadSceneMode.Additive);
                
                result.Add(sceneLayerType, scene);
                scene.ActivateAsync();
            }

            _logger.LogMessage($"all scene layers loaded for {sceneName}", nameof(SceneLoader));
            
            onLoaded?.Invoke(sceneName);
            return result;
        }
    }
}