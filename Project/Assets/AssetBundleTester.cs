using UnityEngine;
using UnityEngine.SceneManagement;

namespace App
{
	public class AssetBundleTester : MonoBehaviour
	{
		public void Start()
		{
			AssetBundle.LoadFromFile("../AssetBundles/SampleScene");

			SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
		}
	}
}