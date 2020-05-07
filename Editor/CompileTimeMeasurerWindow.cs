using UnityEditor;
using UnityEngine;

namespace UniCompileTimeMeasurer
{
	/// <summary>
	/// 計測したコンパイル時間を閲覧できるウィンドウ
	/// </summary>
	internal sealed class CompileTimeMeasurerWindow : EditorWindow
	{
		//================================================================================
		// 変数
		//================================================================================
		private Vector2 m_scrollPosition;

		//================================================================================
		// 関数
		//================================================================================
		/// <summary>
		/// GUI を描画する時に呼び出されます
		/// </summary>
		private void OnGUI()
		{
			if ( Event.current.type == EventType.Repaint )
			{
				var backgroundRect = new Rect
				(
					x: 0,
					y: 0,
					width: EditorGUIUtility.currentViewWidth,
					height: EditorGUIUtility.singleLineHeight
				);

				EditorStyles.toolbar.Draw
				(
					position: backgroundRect,
					isHover: false,
					isActive: true,
					on: true,
					hasKeyboardFocus: false
				);
			}

			var entities = TimeEntities.LoadFromConfig();
			var settings = CompileTimeMeasurerSettings.LoadFromEditorPrefs();

			using ( new EditorGUILayout.HorizontalScope() )
			{
				GUILayout.FlexibleSpace();

				if ( GUILayout.Button( "Settings", EditorStyles.toolbarButton ) )
				{
					SettingsService.OpenUserPreferences( "Preferences/UniCompileTimeMeasurer" );
				}
				if ( GUILayout.Button( "Clear", EditorStyles.toolbarButton ) )
				{
					entities.Clear();
					TimeEntities.SaveToConfig( entities );
					Repaint();
				}
			}

			using ( var scope = new EditorGUILayout.ScrollViewScope( m_scrollPosition ) )
			{
				for ( var i = entities.Entities.Count - 1; i >= 0; i-- )
				{
					var entry             = entities.Entities[ i ];
					var dateTimeFormat    = settings.DateTimeFormat;
					var elapsedTimeFormat = settings.ElapsedTimeFormat;
					var elapsedTimeSuffix = settings.ElapsedTimeSuffix;

					var dateTime = string.IsNullOrWhiteSpace( dateTimeFormat )
							? entry.DateTime.ToString()
							: entry.DateTime.ToString( dateTimeFormat )
						;

					var elapsedTime = string.IsNullOrWhiteSpace( elapsedTimeFormat )
							? entry.ElapsedTime.TotalSeconds.ToString()
							: entry.ElapsedTime.ToString( elapsedTimeFormat )
						;

					var label1 = dateTime;
					var label2 = elapsedTime + elapsedTimeSuffix;

					EditorGUILayout.LabelField( label1, label2 );
				}

				m_scrollPosition = scope.scrollPosition;
			}
		}

		//================================================================================
		// 関数(static)
		//================================================================================
		/// <summary>
		/// 開きます
		/// </summary>
		[MenuItem( "Window/UniCompileTimeMeasurer" )]
		private static void Open()
		{
			GetWindow<CompileTimeMeasurerWindow>();
		}
	}
}