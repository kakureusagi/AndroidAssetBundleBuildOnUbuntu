using System.IO;
using UnityEditor;
using UnityEditor.Build.Pipeline;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace App
{
	public class Builder
	{
		static readonly string OutputDirectory = "Assets/AssetBundles";
		
		[MenuItem("App/Build AssetBundle")]
		public static void Build()
		{
			Directory.CreateDirectory(OutputDirectory);
			var parameters = new BundleBuildParameters(BuildTarget.Android, BuildTargetGroup.Android, OutputDirectory);

			var content = new BundleBuildContent(new AssetBundleBuild[]
			{
				new AssetBundleBuild
				{
					assetBundleName = "CubePrefab.bytes",
					assetNames = new[] { "Assets/CubePrefab.prefab" },
				},
				new AssetBundleBuild
				{
					assetBundleName = "Animation.bytes",
					assetNames = new[] { "Assets/Animation.anim" },
				},
				new AssetBundleBuild
				{
					assetBundleName = "SampleScene.bytes",
					assetNames = new[] { "Assets/SampleScene.unity" },
				},
				new AssetBundleBuild
				{
					assetBundleName = "Dog.bytes",
					assetNames = new[] { "Assets/Dog.png" },
				},
			});


			ContentPipeline.BuildAssetBundles(parameters, content, out var results);
			foreach (var info in results.BundleInfos)
			{
				Debug.Log(info.Key);
				Debug.Log(info.Value.FileName);
			}
		}
	}
}
