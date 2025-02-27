﻿using System.Collections;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using static GrimoraMod.GrimoraPlugin;

namespace GrimoraMod;

public static class AssetUtils
{
	private static readonly Dictionary<string, string> FileChecksums = new()
	{
		{ "grimoramod_abilities", "5F4480016DFEC0508ED38B9AF20058722130C1E24870B357C1DE4BCC609A9292" },
		{ "grimoramod_controller", "74BC4A80C0FA64CF5EF3F578DCB49625DBA54079785C210E7B2DC79B87C86FC5" },
		{ "grimoramod_mats", "939AD534C55F4150F586CE706345E19E80ABA39EC1D4298A61192220B3B1790F" },
		{ "grimoramod_prefabs", "D92040437EE9077A6015E888794C689DF8C40B783A53DBF8F9A60C335542023E" },
		{ "grimoramod_sounds", "A2FC231C491780A59F925C4F3FB83B2E789DE4323516C74FE148ADCC4549E1D2" },
		{ "grimoramod_sprites", "A5193D32E761A35C2DE5D6CEE8544D4E16F7288EFB5D558E24EF8CACB7B3E685" },
	};

	private static string ValidateFile(string assetBundleFile)
	{
		string fileToRead = FileUtils.FindFileInPluginDir(assetBundleFile);
		using var sha256 = SHA256.Create();
		using var stream = File.OpenRead(fileToRead);
		byte[] checksum = sha256.ComputeHash(stream);
		var sha265Checksum = BitConverter.ToString(checksum).Replace("-", string.Empty);
		if (FileChecksums.TryGetValue(Path.GetFileName(fileToRead), out string correctChecksum) && correctChecksum != sha265Checksum)
		{
			Log.LogError($"[AssetUtils] File [{Path.GetFileName(fileToRead)}] calculated checksum [{sha265Checksum}] does not match the correct one [{correctChecksum}] for this file!" +
			             $"\nPlease redownload the mod!");
		}

		return fileToRead;
	}

	public static List<T> LoadAssetBundle<T>(string assetBundleFile) where T : UnityEngine.Object
	{
		Stopwatch stopwatch = Stopwatch.StartNew();
		string fileToRead = ValidateFile(assetBundleFile);
		AssetBundle assetBundle = AssetBundle.LoadFromFile(fileToRead);
		var loadedBundle = assetBundle.LoadAllAssets<T>();
		// GrimoraPlugin.Log.LogDebug($"Bundle [{assetBundle}] - {string.Join(",", loadedBundle.Select(_ => _.name))}");
		assetBundle.Unload(false);
		stopwatch.Stop();
		Log.LogDebug($"Time taken to load bundle [{assetBundleFile}]: [{stopwatch.ElapsedMilliseconds}]ms");
		return loadedBundle.ToList();
	}

	public static IEnumerator LoadAssetBundleAsync<T>(string assetBundleFile) where T : UnityEngine.Object
	{
		Stopwatch stopwatch = Stopwatch.StartNew();

		Type type = typeof(T);
		string fileToRead = ValidateFile(assetBundleFile);
		var bundleLoadRequest = AssetBundle.LoadFromFileAsync(fileToRead);
		yield return bundleLoadRequest;
		var bundle = bundleLoadRequest.assetBundle;
		var allAssetsRequest = bundle.LoadAllAssetsAsync<T>();
		yield return allAssetsRequest;
		// Log.LogDebug($"Bundle [{bundle}] - {bundle.GetAllAssetNames().Join()}");
		if (type == typeof(AudioClip))
		{
			AllSounds = allAssetsRequest.allAssets.Cast<AudioClip>().ToList();
		}
		else if (type == typeof(Material))
		{
			AllMats = allAssetsRequest.allAssets.Cast<Material>().ToList();
		}
		else if (type == typeof(GameObject))
		{
			AllPrefabs = allAssetsRequest.allAssets.Cast<GameObject>().ToList();
		}
		else if (type == typeof(RuntimeAnimatorController))
		{
			AllControllers = allAssetsRequest.allAssets.Cast<RuntimeAnimatorController>().ToList();
		}
		else if (type == typeof(Sprite))
		{
			AllSprites = allAssetsRequest.allAssets.Cast<Sprite>().ToList();
		}
		else if (type == typeof(Texture))
		{
			AllAbilitiesTextures = allAssetsRequest.allAssets.Cast<Texture>().ToList();
		}
		else if (type == typeof(Mesh))
		{
			AllMesh = allAssetsRequest.allAssets.Cast<Mesh>().ToList();
		}

		bundle.Unload(false);
		stopwatch.Stop();
		Log.LogDebug($"Time taken to load bundle [{assetBundleFile}]: [{stopwatch.ElapsedMilliseconds}]ms");
	}

	private static bool NameMatchesAsset(UnityEngine.Object obj, string nameToCheckFor)
	{
		return obj.name.Equals(nameToCheckFor, StringComparison.OrdinalIgnoreCase);
	}

	public static T GetPrefab<T>(string prefabName) where T : UnityEngine.Object
	{
		Type type = typeof(T);

		T objToReturn = null;
		try
		{
			if (type == typeof(AudioClip))
			{
				objToReturn = AllSounds.Single(go => NameMatchesAsset(go, prefabName)) as T;
			}
			else if (type == typeof(Material))
			{
				objToReturn = AllMats.Single(go => NameMatchesAsset(go, prefabName)) as T;
			}
			else if (type == typeof(GameObject))
			{
				objToReturn =AllPrefabs.Single(go => go.name==prefabName) as T;
			}
			else if (type == typeof(RuntimeAnimatorController))
			{
				objToReturn = AllControllers.Single(go => NameMatchesAsset(go, prefabName)) as T;
			}
			else if (type == typeof(Sprite))
			{
				objToReturn = AllSprites.Single(go => NameMatchesAsset(go, prefabName)) as T;
			}
			else if (type == typeof(Texture))
			{
				objToReturn = AllAbilitiesTextures.Single(go => NameMatchesAsset(go, prefabName)) as T;
			}
			else if (type == typeof(Mesh))
			{
				objToReturn = AllMesh.Single(go => NameMatchesAsset(go, prefabName)) as T;
			}

		}
		catch (Exception e)
		{
			Log.LogError(
				$"Unable to find prefab [{prefabName}]! This could mean the asset bundle doesn't contain it, or, most likely, your mod manager didn't correctly update the asset bundle files."
			+ "\n If it worked last update, delete your files and download the mod files again. "
			+ "\n There's a weird issue with how mod managers handle asset bundle between mod updates."
			);
			throw;
		}

		return objToReturn;
	}
}
