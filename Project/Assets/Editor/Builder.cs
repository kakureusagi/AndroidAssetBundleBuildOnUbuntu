using System.IO;
using UnityEditor;
using UnityEditor.Build.Pipeline;
using UnityEngine;

namespace App
{
	public class Builder
	{
		static readonly string OutputDirectory = "Assets/AssetBundles";
		
		[MenuItem("App/Build AssetBundle")]
		public static void BuildAssetBundle()
		{
			Directory.CreateDirectory(OutputDirectory);
			var parameters = new BundleBuildParameters(BuildTarget.Android, BuildTargetGroup.Android, OutputDirectory);

			var content = new BundleBuildContent(new AssetBundleBuild[]
			{
				new AssetBundleBuild
				{
					assetBundleName = "CubePrefab.bytes",
					assetNames = new[] { "Assets/Data/CubePrefab.prefab" },
				},
				new AssetBundleBuild
				{
					assetBundleName = "Animation.bytes",
					assetNames = new[] { "Assets/Data/Animation.anim" },
				},
				new AssetBundleBuild
				{
					assetBundleName = "SampleScene.bytes",
					assetNames = new[] { "Assets/Data/SampleScene.unity" },
				},
				new AssetBundleBuild
				{
					assetBundleName = "Dog.bytes",
					assetNames = new[] { "Assets/Data/Dog.png" },
				},
			});

			ContentPipeline.BuildAssetBundles(parameters, content, out var results);
			foreach (var info in results.BundleInfos)
			{
				Debug.Log(info.Key);
				Debug.Log(info.Value.FileName);
			}
		}

		[MenuItem("App/Build App")]
		public static void BuildApp()
		{
			var options = new BuildPlayerOptions
			{
				scenes = new[] { "Assets/Scenes/FirstScene.unity" },
				locationPathName = "App.apk",
				target = BuildTarget.Android,
				targetGroup = BuildTargetGroup.Android,
				options = BuildOptions.None,
			};

			var report = BuildPipeline.BuildPlayer(options);
			Debug.Log(report.summary.result);
		}
	}
}
