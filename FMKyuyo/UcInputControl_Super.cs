using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace App
{
	/// <summary>
	/// [作成者 fj]
	/// ユーザーコントロールの親オブジェクトです。
	/// </summary>
	public partial class UcInputControl_Super : UserControl
	{
		#region *** Private Value ***
		/// <summary>
		/// ユーザーコントロールから、グリッドに移動許可を出していいかを示す値。
		/// </summary>
		protected bool	grid_move_permission = true;
		#endregion
		
		#region *** Property ***
		/// <summary>
		/// ユーザーコントロールから、グリッドに移動許可を出していいかを示す値を取得します。
		/// </summary>
		public bool GridMovePermission
		{
			get
			{
				return grid_move_permission;
			}
		}
		#endregion
		
		#region *** Constructor ***
		/// <summary>
		/// コンストラクタ。
		/// </summary>
		public UcInputControl_Super()
		{
			InitializeComponent();
		}
		#endregion
	}
}
