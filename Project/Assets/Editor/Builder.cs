using System.IO;
using UnityEditor;
using UnityEditor.Build.Pipeline;
using UnityEngine;

namespace App
{
	public class Builder
	{
		static readonly string OutputDirectory = "../AssetBundles";
		
		[MenuItem("App/Build AssetBundle")]
		public static void Build()
		{
			Directory.CreateDirectory(OutputDirectory);
			var parameters = new BundleBuildParameters(BuildTarget.Android, BuildTargetGroup.Android, OutputDirectory);

			var content = new BundleBuildContent(new AssetBundleBuild[]
			{
				new AssetBundleBuild
				{
					assetBundleName = "CubePrefab",
					assetNames = new[] { "Assets/CubePrefab.prefab" },
				},
				new AssetBundleBuild
				{
					assetBundleName = "Animation.anim",
					assetNames = new[] { "Assets/Animation.anim" },
				},
				new AssetBundleBuild
				{
					assetBundleName = "SampleScene",
					assetNames = new[] { "Assets/SampleScene.unity" },
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
