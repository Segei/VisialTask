using UnityEngine;
using UnityEngine.SceneManagement;


namespace Script
{
    [CreateAssetMenu(menuName = "LoaderScene")]
    public class LoaderScene : ScriptableObject
    {
        [SerializeField] private string _nameSceneView = null;
        [SerializeField] private string _nameSceneEdit = null;
        [SerializeField] private string _nameSceneCircleDiagram = null;
        [SerializeField] private string _nameSceneBarGraphDiagram = null;
        private Scene _sceneActive;


        public void LoadSceneView()
        {
            _sceneActive = SceneManager.LoadScene(_nameSceneView, new LoadSceneParameters(LoadSceneMode.Single));
        }

        public void LoadSceneEdit()
        {
            _sceneActive = SceneManager.LoadScene(_nameSceneEdit, new LoadSceneParameters(LoadSceneMode.Single));
        }

        public void LoadSceneCircleDiagram()
        {
            _sceneActive = SceneManager.LoadScene(_nameSceneCircleDiagram, new LoadSceneParameters(LoadSceneMode.Single));
        }

        public void LoadSceneBarGraphDiagram()
        {
            _sceneActive = SceneManager.LoadScene(_nameSceneBarGraphDiagram, new LoadSceneParameters(LoadSceneMode.Single));
        }
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
