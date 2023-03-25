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
        private readonly string[] _levelLayersSuffixes = {"", "_Env", "_Nav", "_Lights", "_UI"};
        
        private readonly IAssetProvider _assetProvider;
        private readonly ILoggingService _logger;

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
        
        public async Task<List<SceneInstance>> LoadSet(string sceneName, Action<string> onLoaded = null)
        {
            var result = new List<SceneInstance>();
            for (var index = 0; index < _levelLayersSuffixes.Length; index++)
            {
                var scene = await _assetProvider.LoadScene(
                    sceneName + _levelLayersSuffixes[index],
                    mode: index == 0 ? LoadSceneMode.Single : LoadSceneMode.Additive);
                result.Add(scene);
                scene.ActivateAsync();
                
                _logger.LogMessage($"{sceneName + _levelLayersSuffixes[index]} loaded for level set", nameof(SceneLoader));
            }

            onLoaded?.Invoke(sceneName);
            return result;
        }
    }
}