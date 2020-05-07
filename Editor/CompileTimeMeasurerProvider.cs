using UnityEditor;
using UnityEngine;

namespace UniCompileTimeMeasurer
{
	/// <summary>
	/// コンパイル時間計測 Preferences における設定画面を管理するクラス
	/// </summary>
	internal sealed class CompileTimeMeasurerProvider : SettingsProvider
	{
		//================================================================================
		// 関数
		//================================================================================
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CompileTimeMeasurerProvider( string path, SettingsScope scope )
			: base( path, scope )
		{
		}

		/// <summary>
		/// GUI を描画する時に呼び出されます
		/// </summary>
		public override void OnGUI( string searchContext )
		{
			var settings = CompileTimeMeasurerSettings.LoadFromEditorPrefs();

			using ( var checkScope = new EditorGUI.ChangeCheckScope() )
			{
				settings.IsEnable          = EditorGUILayout.Toggle( "Enabled", settings.IsEnable );
				settings.MaxCount          = EditorGUILayout.IntField( "Max Count", settings.MaxCount );
				settings.DateTimeFormat    = EditorGUILayout.TextField( "Date Time Format", settings.DateTimeFormat );
				settings.ElapsedTimeFormat = EditorGUILayout.TextField( "Elapsed Time Format", settings.ElapsedTimeFormat );
				settings.ElapsedTimeSuffix = EditorGUILayout.TextField( "Elapsed Time Suffix", settings.ElapsedTimeSuffix );

				if ( GUILayout.Button( "Use Defaults" ) )
				{
					settings = new CompileTimeMeasurerSettings();
					CompileTimeMeasurerSettings.SaveToEditorPrefs( settings );
				}

				if ( !checkScope.changed ) return;

				CompileTimeMeasurerSettings.SaveToEditorPrefs( settings );
			}
		}

		//================================================================================
		// 関数(static)
		//================================================================================
		/// <summary>
		/// Preferences にメニューを追加します
		/// </summary>
		[SettingsProvider]
		private static SettingsProvider Create()
		{
			var path     = "Preferences/UniCompileTimeMeasurer";
			var provider = new CompileTimeMeasurerProvider( path, SettingsScope.User );

			return provider;
		}
	}
}