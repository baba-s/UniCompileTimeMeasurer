using System;
using UnityEditor;
using UnityEditor.Compilation;

namespace UniCompileTimeMeasurer
{
	/// <summary>
	/// コンパイル時間を計測するエディタ拡張
	/// </summary>
	[InitializeOnLoad]
	internal sealed class CompileTimeMeasurer
	{
		//================================================================================
		// 関数(static)
		//================================================================================
		/// <summary>
		/// コンストラクタ
		/// </summary>
		static CompileTimeMeasurer()
		{
			CompilationPipeline.compilationStarted  += OnStarted;
			CompilationPipeline.compilationFinished += OnFinished;
		}

		/// <summary>
		/// コンパイルを開始した時に呼び出されます
		/// </summary>
		private static void OnStarted( object _ )
		{
			var settings = CompileTimeMeasurerSettings.LoadFromEditorPrefs();

			if ( !settings.IsEnable ) return;

			var state = ScriptableSingleton<CompileState>.instance;
			state.StartRecord( DateTime.Now.Ticks );
		}

		/// <summary>
		/// コンパイルを終了した時に呼び出されます
		/// </summary>
		private static void OnFinished( object _ )
		{
			var settings = CompileTimeMeasurerSettings.LoadFromEditorPrefs();

			if ( !settings.IsEnable ) return;

			var state = ScriptableSingleton<CompileState>.instance;

			if ( !state.IsCompiling ) return;

			state.StopRecord();

			var startTimeTicks   = state.StartTimeTicks;
			var elapsedTimeTicks = DateTime.Now.Ticks - startTimeTicks;
			var entities         = TimeEntities.LoadFromConfig();
			var entry            = new TimeEntry( startTimeTicks, elapsedTimeTicks );

			entities.Add( entry, settings.MaxCount );

			TimeEntities.SaveToConfig( entities );
		}
	}
}