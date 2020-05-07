using System;
using UnityEngine;

namespace UniCompileTimeMeasurer
{
	/// <summary>
	/// コンパイル時間のエントリを管理するクラス
	/// </summary>
	[Serializable]
	internal sealed class TimeEntry
	{
		//================================================================================
		// 変数(SerializeField)
		//================================================================================
		[SerializeField] private long m_dateTimeTicks    = default; // コンパイルした日時
		[SerializeField] private long m_elapsedTimeTicks = default; // コンパイルにかかった時間

		//================================================================================
		// プロパティ
		//================================================================================
		public DateTime DateTime    => new DateTime( m_dateTimeTicks );
		public TimeSpan ElapsedTime => new TimeSpan( m_elapsedTimeTicks );

		//================================================================================
		// 関数
		//================================================================================
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public TimeEntry( long dateTimeTicks, long elapsedTimeTicks )
		{
			m_dateTimeTicks    = dateTimeTicks;
			m_elapsedTimeTicks = elapsedTimeTicks;
		}
	}
}