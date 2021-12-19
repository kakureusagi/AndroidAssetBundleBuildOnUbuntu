using UnityEngine;
using UnityEngine.SceneManagement;

namespace App
{
	public class AssetBundleTester : MonoBehaviour
	{
		[SerializeField]
		TextAsset scene = default;

		[SerializeField]
		TextAsset cube = default;

		[SerializeField]
		TextAsset dog = default;
		
		
		public void Start()
		{
			AssetBundle.LoadFromMemory(dog.bytes);
			AssetBundle.LoadFromMemory(cube.bytes);
			AssetBundle.LoadFromMemory(scene.bytes);

			SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
		}
	}
}