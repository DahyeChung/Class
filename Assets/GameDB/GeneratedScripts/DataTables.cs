﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EasySpreadsheet.
//     Runtime Version: 5.0.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Collections;
using EasySpreadsheet;

namespace DB
{
	public partial class DataTables
	{
		public static DataTables Instance => s_instance;

		private Dictionary<Type, EsRowDataTable> _cfgs;
		public Dictionary<Type, EsRowDataTable> Cfgs => _cfgs;
		public DialogueTable DialogueTable {get; private set;}
		public SkillTable SkillTable {get; private set;}

		private static DataTables s_instance;

		public DataTables()
		{
			s_instance = this;
		}

		public void Load(IEsDataLoader loader)
		{
			_cfgs = new Dictionary<Type, EsRowDataTable>();
			DialogueTable = loader.Load("Dialogue") as DialogueTable;
			_cfgs.Add(typeof(DialogueTable), DialogueTable);
			SkillTable = loader.Load("Skill") as SkillTable;
			_cfgs.Add(typeof(SkillTable), SkillTable);
			PostInit();
		}

		public IEnumerator LoadAsync(IEsDataLoader loader)
		{
			_cfgs = new Dictionary<Type, EsRowDataTable>();
			DialogueTable = loader.Load("Dialogue") as DialogueTable;
			_cfgs.Add(typeof(DialogueTable), DialogueTable);
			yield return null;
			SkillTable = loader.Load("Skill") as SkillTable;
			_cfgs.Add(typeof(SkillTable), SkillTable);
			yield return null;
			PostInit();
		}

		partial void PostInit();


		public static Dialogue GetDialogue(int id)
		{
			return s_instance.DialogueTable.Get(id);
		}

		public static List<Dialogue> GetDialogueList()
		{
			return s_instance.DialogueTable.DataList;
		}

		public static Skill GetSkill(int id)
		{
			return s_instance.SkillTable.Get(id);
		}

		public static List<Skill> GetSkillList()
		{
			return s_instance.SkillTable.DataList;
		}

	}
}
