using System.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace PerelesoqTest.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        public Task<T> Load<T>(string key) where T : class;
        public Task<SceneInstance> LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single);
        public void Release(string key);
        public void Cleanup();
    }
}