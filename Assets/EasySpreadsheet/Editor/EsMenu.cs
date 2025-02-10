using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace EasySpreadsheet
{
	public static class EsMenu
	{
		private const string MenuPath = "EasySpreadsheet/";
		
		[MenuItem(MenuPath + "Generate Scripts")]
		public static void GenScripts()
		{
			if (CheckSpreadsheetFilesDir(out string dir))
			{
				var cnv = new EsConverter();
				cnv.GenerateScripts(dir, EsSettings.Instance.generatedScriptPath);
			}
			else
			{
				EsLog.Error("CheckSpreadsheetFilesDir");
			}
		}
		
		[MenuItem(MenuPath + "Generate Data")]
		public static void GenData()
		{
			if (EditorApplication.isCompiling)
				return;
			
			if (CheckSpreadsheetFilesDir(out string dir))
			{
				var cnv = new EsConverter();
				cnv.GenerateData(dir, EsSettings.Instance.generatedAssetPath);
			}
			else
			{
				EsLog.Error("CheckSpreadsheetFilesDir");
			}
		}
		
		[MenuItem(MenuPath + "Clear Cache")]
		public static void Clear()
		{
			EsCaches.Clear();
			DeleteGeneratedAssets();
			AssetDatabase.Refresh();
		}

		private static bool CheckSpreadsheetFilesDir(out string dir)
		{
			string excelPath = Path.GetFullPath(EsSettings.Instance.excelFilesPath);
			if (Directory.Exists(excelPath))
			{
				dir = excelPath;
				return true;
			}
			
			dir = string.Empty;

			return false;
		}
		
		/*[MenuItem(@"Tools/EasySpreadsheet/Import Xlsx Files")]
		public static void ImportFolder()
		{
			var historySpreadsheetPath = EditorPrefs.GetString(EsConverter.excelPathKey);
			if (string.IsNullOrEmpty(historySpreadsheetPath) || !Directory.Exists(historySpreadsheetPath))
			{
				var fallbackDir = Environment.CurrentDirectory + "/Assets/EasySpreadsheet/Example/SpreadsheetFiles";
				historySpreadsheetPath = Directory.Exists(fallbackDir) ? fallbackDir : Environment.CurrentDirectory;
			}

			var excelPath = EditorUtility.OpenFolderPanel("Select the folder of excel files", historySpreadsheetPath, "");
			if (string.IsNullOrEmpty(excelPath))
				return;

			EditorPrefs.SetString(EsConverter.excelPathKey, excelPath);
			EsConverter.GenerateScripts(excelPath, Environment.CurrentDirectory + "/" + EsSettings.Instance.generatedScriptPath);
		}

		/*[DidReloadScripts]
		private static void OnScriptsReloaded()
		{
			if (!EditorPrefs.GetBool(EsConverter.csChangedKey, false)) return;
			EditorPrefs.SetBool(EsConverter.csChangedKey, false);
			var historySpreadsheetPath = EditorPrefs.GetString(EsConverter.excelPathKey);
			if (string.IsNullOrEmpty(historySpreadsheetPath)) return;
			EsLog.Log("Scripts are reloaded, start generating assets...");
			EsConverter.GenerateScriptableObjects(historySpreadsheetPath, Environment.CurrentDirectory + "/" + EsSettings.Instance.generatedAssetPath);
		}*/

		private static void DeleteGeneratedAssets()
		{
			string assetPath = EsSettings.Instance.generatedAssetPath;
			if (Directory.Exists(assetPath))
				Directory.Delete(assetPath, true);

			string asMeta = Path.GetFullPath(assetPath) + ".meta";
			if (File.Exists(asMeta))
				File.Delete(asMeta);
		}

		[MenuItem(MenuPath + "Settings")]
		public static void OpenSettingsWindow() => SettingsService.OpenProjectSettings("Project/Easy Spreadsheet");

		[MenuItem(MenuPath + "About")]
		private static void OpenAboutWindow()
		{
			var window = EditorWindow.GetWindowWithRect<EsAboutWindow>(new Rect(0, 0, 480, 320), true, "About EasySpreadsheet", true);
			window.Show();
		}
	}
}