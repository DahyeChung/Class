using System;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace EasySpreadsheet
{
	/// <summary>
	/// EasySpreadsheet setting
	/// </summary>
	//[CreateAssetMenu(fileName = "New EasySpreadsheet Settings", menuName = "EasySpreadsheet/Settings", order = 999)]
	public class EsSettings : ScriptableObject
	{
		private const string SettingsFileName = "EasySpreadsheetSettings";
		private const string SettingsFilePath = "Assets/Resources/" + SettingsFileName + ".asset";
		
		[Header("Directory of excel files")]
		public string excelFilesPath;

		[Header("Output path of generated ScriptableObject files")]
		public string generatedAssetPath;

		[Header("Output path of generated script files")]
		public string generatedScriptPath;

		[Header(@"Postfix of generated sheet data classes")]
		public string sheetClassPostfix;

		[Header(@"Postfix of generated row data classes")]
		public string rowClassPostfix;

		[Header("The namespace of generated classes")]
		public string nameSpace;

		[Header("Let variables writable")]
		public bool writable;
		
		[Header("Support Unity Visual Scripting")]
		public bool visualScripting = false;


		public string GetNameSpace(string fileName)
		{
			return /*useFileNameAsNameSpace ? nameSpacePrefix + Path.GetFileNameWithoutExtension(fileName) : */nameSpace;
		}

		public void ResetAll()
		{
			excelFilesPath = "Assets/EasySpreadsheet/Example/SpreadsheetFiles";
			generatedAssetPath = "Assets/EasySpreadsheet/Example/GeneratedData/Resources";
			generatedScriptPath = "Assets/EasySpreadsheet/Example/GeneratedScripts";
			sheetClassPostfix = "Table";
			rowClassPostfix = "";
			nameSpace = "Es";
			writable = false;
			visualScripting = false;
		}

		public void Save(bool saveAsText = true)
		{
			if (!_current)
			{
				Debug.LogError("Cannot save EsSettings: no instance!");
				return;
			}

#if UNITY_EDITOR
			if (string.IsNullOrEmpty(sheetClassPostfix)) sheetClassPostfix = "Table";
			
			EditorUtility.SetDirty(_current);
			AssetDatabase.Refresh();
#endif
		}

		#region Sigleton

		private static EsSettings _current;

		public static EsSettings Instance
		{
			get
			{
				if (_current != null) return _current;
				_current = Resources.Load<EsSettings>(SettingsFileName);
				if (_current != null) return _current;
				_current = CreateInstance<EsSettings>();
				_current.ResetAll();
#if UNITY_EDITOR
				string directoryName = Path.GetDirectoryName(SettingsFilePath);
				if (!Directory.Exists(directoryName))
					Directory.CreateDirectory(SettingsFilePath);

				AssetDatabase.CreateAsset(_current, SettingsFilePath);
				AssetDatabase.Refresh();
#endif
				return _current;
			}
		}

		#endregion

		public string GetRowDataClassName(string fileName, string sheetName, bool includeNameSpace = false)
		{
			return (includeNameSpace ? GetNameSpace(fileName) + "." : null) + sheetName + rowClassPostfix;
		}

		public string GetSheetClassName(string fileName, string sheetName)
		{
			return /*useFileNameAsNameSpace ? (Path.GetFileNameWithoutExtension(fileName) + "_" + sheetName + sheetClassPostfix) :
				*/sheetName + sheetClassPostfix;
		}

		public string GetSheetInspectorClassName(string fileName, string sheetName)
		{
			return /*Path.GetFileNameWithoutExtension(fileName) + "_" + */sheetName + "Inspector";
		}

		public string GetAssetFileName(string fileName, string sheetName)
		{
			return /*Path.GetFileNameWithoutExtension(fileName) + "_" + */sheetName + ".asset";
		}

		public string ToAssetFileName(string sheetClassName)
		{
			if (string.IsNullOrEmpty(sheetClassPostfix)) return sheetClassName;
			return sheetClassName.Substring(0, sheetClassName.Length - sheetClassPostfix.Length);
		}

		public string GetTableClassFileName(string fileName, string sheetName)
		{
			return GetSheetClassName(fileName, sheetName) + ".cs";
		}

		public string GetRowClassFileName(string fileName, string sheetName)
		{
			return GetRowDataClassName(fileName, sheetName) + ".cs";
		}

		public string GetSheetName(Type sheetClassType)
		{
			string fullName = sheetClassType.Name;
			string[] parts = fullName.Split('.');
			string lastPart = parts[parts.Length - 1];
			lastPart = lastPart.Substring(lastPart.IndexOf('_') + 1);
			return string.IsNullOrEmpty(sheetClassPostfix) ? lastPart : lastPart.Substring(0, lastPart.IndexOf(sheetClassPostfix, StringComparison.Ordinal));
		}
	}
}