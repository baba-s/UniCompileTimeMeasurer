using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UniCompileTimeMeasurer
{
	/// <summary>
	/// コンパイル時間のエントリをすべて管理するクラス
	/// </summary>
	[Serializable]
	internal sealed class TimeEntities
	{
		//================================================================================
		// 定数
		//================================================================================
		public const string KEY = "UNI_COMPILE_TIME_MEASURER";

		//================================================================================
		// 変数(SerializeField)
		//================================================================================
		[SerializeField] private List<TimeEntry> m_entries = new List<TimeEntry>();

		//================================================================================
		// プロパティ
		//================================================================================
		public IReadOnlyList<TimeEntry> Entities => m_entries;

		//================================================================================
		// 関数
		//================================================================================
		/// <summary>
		/// エントリを追加します
		/// </summary>
		public void Add( TimeEntry entry, int maxCount )
		{
			m_entries.Add( entry );

			while ( maxCount < m_entries.Count )
			{
				m_entries.RemoveAt( 0 );
			}
		}

		/// <summary>
		/// すべてのエントリをクリアします
		/// </summary>
		public void Clear()
		{
			m_entries.Clear();
		}

		//================================================================================
		// 関数(static)
		//================================================================================
		/// <summary>
		/// 設定から読み込みます
		/// </summary>
		public static TimeEntities LoadFromConfig()
		{
			var value    = EditorUserSettings.GetConfigValue( KEY );
			var entities = JsonUtility.FromJson<TimeEntities>( value ) ?? new TimeEntities();

			return entities;
		}

		/// <summary>
		/// 設定に保存します
		/// </summary>
		public static void SaveToConfig( TimeEntities entities )
		{
			var json = JsonUtility.ToJson( entities );

			EditorUserSettings.SetConfigValue( KEY, json );
		}
	}
}