using System;
using UnityEditor;
using UnityEngine;

namespace UniCompileTimeMeasurer
{
	/// <summary>
	/// コンパイル時間の計測に関する設定を管理するクラス
	/// </summary>
	[Serializable]
	internal sealed class CompileTimeMeasurerSettings
	{
		//================================================================================
		// 定数
		//================================================================================
		public const string KEY = "UNI_COMPILE_TIME_MEASURE_SETTINGS";

		//================================================================================
		// 変数(SerializeField)
		//================================================================================
		[SerializeField] private bool   m_isEnable          = false;
		[SerializeField] private int    m_maxCount          = 500;
		[SerializeField] private string m_dateTimeFormat    = "yyyy/MM/dd HH:mm:ss";
		[SerializeField] private string m_elapsedTimeFormat = @"s\.ff";
		[SerializeField] private string m_elapsedTimeSuffix = " 秒";

		//================================================================================
		// プロパティ
		//================================================================================
		public bool IsEnable
		{
			get => m_isEnable;
			set => m_isEnable = value;
		}

		public int MaxCount
		{
			get => m_maxCount;
			set => m_maxCount = value;
		}

		public string DateTimeFormat
		{
			get => m_dateTimeFormat;
			set => m_dateTimeFormat = value;
		}

		public string ElapsedTimeFormat
		{
			get => m_elapsedTimeFormat;
			set => m_elapsedTimeFormat = value;
		}

		public string ElapsedTimeSuffix
		{
			get => m_elapsedTimeSuffix;
			set => m_elapsedTimeSuffix = value;
		}

		//================================================================================
		// 関数(static)
		//================================================================================
		/// <summary>
		/// 設定から読み込みます
		/// </summary>
		public static CompileTimeMeasurerSettings LoadFromEditorPrefs()
		{
			var json = EditorPrefs.GetString( KEY );
			var settings = JsonUtility.FromJson<CompileTimeMeasurerSettings>( json )
			            ?? new CompileTimeMeasurerSettings();

			return settings;
		}

		/// <summary>
		/// 設定に保存します
		/// </summary>
		public static void SaveToEditorPrefs( CompileTimeMeasurerSettings settings )
		{
			var json = JsonUtility.ToJson( settings );

			EditorPrefs.SetString( KEY, json );
		}
	}
}