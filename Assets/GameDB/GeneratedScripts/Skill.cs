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
using UnityEngine;
using EasySpreadsheet;

namespace DB
{
	[Serializable]
	public sealed partial class Skill : EsRowData
	{
		[EsKeyField]
		[SerializeField]
		private int _Id;
		public int Id => _Id;

		[SerializeField]
		private string _Name;
		public string Name => _Name;

		[SerializeField]
		private int _Range;
		public int Range => _Range;

		[SerializeField]
		private int _CoolDown;
		public int CoolDown => _CoolDown;

		[SerializeField]
		private int _Duration;
		public int Duration => _Duration;

		[SerializeField]
		private int _Luciferin;
		public int Luciferin => _Luciferin;


		public Skill()
		{
		}

#if UNITY_EDITOR
		private void Parse(List<string> cells, int column)
		{
			EsParser.TryParse(cells[column++], out _Id);
			EsParser.TryParse(cells[column++], out _Name);
			EsParser.TryParse(cells[column++], out _Range);
			EsParser.TryParse(cells[column++], out _CoolDown);
			EsParser.TryParse(cells[column++], out _Duration);
			EsParser.TryParse(cells[column++], out _Luciferin);
		}
#endif
		protected override void OnDeserialized()
		{

			PostInit();
		}

		partial void PostInit();
	}

}