using UnityEditor;
using UnityEngine;

namespace UniCompileTimeMeasurer
{
	/// <summary>
	/// コンパイルの状態を管理するシングルトンクラス
	/// </summary>
	internal sealed class CompileState : ScriptableSingleton<CompileState>
	{
		//================================================================================
		// 変数(SerializeField)
		//================================================================================
		[SerializeField] private long m_startTimeTicksTicks = default;
		[SerializeField] private bool m_isCompiling         = default;

		//================================================================================
		// プロパティ
		//================================================================================
		public long StartTimeTicks => m_startTimeTicksTicks;
		public bool IsCompiling    => m_isCompiling;

		//================================================================================
		// 関数
		//================================================================================
		/// <summary>
		/// プライベートコンストラクタ
		/// </summary>
		private CompileState()
		{
		}

		/// <summary>
		/// 計測を開始します
		/// Start という名前にすると Unity のイベント名と競合するため
		/// StartRecord という名前に変更
		/// </summary>
		public void StartRecord( long startTimeTicks )
		{
			m_startTimeTicksTicks = startTimeTicks;
			m_isCompiling         = true;
		}

		/// <summary>
		/// 計測を終了します
		/// </summary>
		public void StopRecord()
		{
			m_isCompiling = false;
		}
	}
}