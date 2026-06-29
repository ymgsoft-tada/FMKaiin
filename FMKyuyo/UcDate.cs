using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using GrapeCity.Win.Editors;

using ComponentRegistry;
using GControlGcDateEx;
using GrapeCity.Win.Editors.Fields;

namespace App
{
	/// <summary>
	/// [作成者 fj]
	/// アプリ用にフォーマットを強制特化できるDateコントロールです。
	/// </summary>
	public partial class UcDate : UserControl
	{
		#region *** Public Enum ***
		/// <summary>
		/// Dateフォーマット
		/// </summary>
		public enum UcDateFormat
		{
			/// <summary>
			/// 1000/01/01, H20/01/01
			/// </summary>
			YYYY_MM_DD,
			/// <summary>
			/// 1000/01/01, H20年01月01日
			/// </summary>
			YYYYMMDD,
			/// <summary>
			/// 1000/01, H20年01月
			/// </summary>
			YYYYMM,
			/// <summary>
			/// 1000, H20年度
			/// </summary>
			YYYY,
		}
		#endregion
		
		/// <summary>
		/// 値の変更処理
		/// </summary>
		public event EventHandler UcValueChanged;

		#region *** Private Value ***

		/// <summary>
		/// 日付を西暦で表示するかどうかの値。trueで西暦表示します。
		/// </summary>
		static bool		uc_koyomiSeirekiOn = false;
		/// <summary>
		/// コントロールの表示フォーマット
		/// </summary>
		UcDateFormat	uc_format;
		#endregion

		#region *** Property ***
		/// <summary>
		/// フォントサイズを設定します。
		/// </summary>
		[Description("[UC機能]入力コントロールのフォントサイズを変更します。")]
		public new Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
				date.Font = value;
			}
		}
		
		/// <summary>
		/// (UC機能)Dateコントロールを取得します。取得したコントロールのプロパティを直接書き換えると誤動作の原因になるため、行わないようにしてください。
		/// </summary>
		[Description("[UC機能]Dateコントロールを取得します。取得したコントロールのプロパティを直接書き換えると誤動作の原因になるため、行わないようにしてください。")]
		public GcDateEx	UcDateCtl
		{
			get
			{
				return date;
			}
		}

		/// <summary>
		/// (UC機能)表示フォーマットを取得・設定します。
		/// </summary>
		[Description("[UC機能]表示フォーマットを取得・設定します。")]
		public UcDateFormat	UcFormat
		{
			get
			{
				return uc_format;
			}
			
			set
			{
				if (uc_format != value)
				{
					uc_format = value;
					changeFormat();
				}
			}
		}
		/// <summary>
		/// 日付の値をDateTime型で取得、または設定します。
		/// </summary>
		[Description("日付の値をDateTime?型で取得、または設定します。")]
		public DateTime? UcValue
		{
			get
			{
				return date.Value;
			}
			set
			{
				if (value != null)
				{
					if (value < date.ExMinDate)
					{
						value = date.ExMinDate;
					}
					else if (value > date.ExMaxDate)
					{
						value = date.ExMaxDate;
					}
				}
				date.Value = value;
//				Console.WriteLine(date.DisplayText);
			}
		}
		/// <summary>
		/// コントロールの枠色を取得、または設定します。
		/// </summary>
		[Description("コントロールの境界線の色を取得、または設定します。")]
		public Color SingleBorderColor
		{
			get
			{
				return date.SingleBorderColor;
			}
			set
			{
				date.SingleBorderColor = value;
			}
		}
		/// <summary>
		/// コントロールがEnabled=false時の文字色を取得、または設定します。
		/// </summary>
		[Description("コントロールがEnabled=false時の文字色を取得、または設定します。")]
		public Color DisabledForeColor
		{
			get
			{
				return date.DisabledForeColor;
			}
			set
			{
				date.DisabledForeColor = value;
			}
		}
		/// <summary>
		/// コントロールがEnabled=false時の背景色を取得、または設定します。
		/// </summary>
		[Description("コントロールがEnabled=false時の背景色を取得、または設定します。")]
		public Color DisabledBackColor
		{
			get
			{
				return date.DisabledBackColor;
			}
			set
			{
				date.DisabledBackColor = value;
			}
		}
		/// <summary>
		/// サイドボタン（カレンダー）を実行時に表示するか取得・設定します。
		/// </summary>
		[Description("サイドボタン（カレンダー）を実行時に表示するか取得・設定します。")]
		public bool UcSideButton
		{
			get
			{
				return date.SideButtons[0].Visible != ButtonVisibility.NotShown;
			}
			set
			{
				if (value == true)
				{
					date.SideButtons[0].Visible = ButtonVisibility.ShowAlways;
				}
				else
				{
					date.SideButtons[0].Visible = ButtonVisibility.NotShown;
				}
			}
		}
		/// <summary>
		/// コントロールの背景色を取得・設定します。
		/// </summary>
		[Description("コントロールの背景色を取得・設定します。")]
		public Color UcBackColor
		{
			get
			{
				return date.BackColor;
			}
			set
			{
				date.BackColor = value;
			}
		}
		/// <summary>
		/// コントロールの境界線の種類を取得、または設定します。
		/// </summary>
		[Description("コントロールの境界線の種類を取得、または設定します。")]
		public BorderStyle UcBorderStyle
		{
		    get
		    {
		        return date.BorderStyle;
		    }
		    set
		    {				
		        date.BorderStyle	= value;				
		    }
		}
		
		/// <summary>
		/// コントロールを読み込み専用モードにするかどうかを取得、または設定します。
		/// </summary>
		[Description("コントロールを読み込み専用モードにするかどうかを取得、または設定します。")]
		public bool UcReadOnly
		{
			get
			{
				return date.ReadOnly;
			}
			set
			{
				date.ReadOnly = value;
			}
		}
		/// <summary>
		/// コントロールがユーザーとの対話に応答できるかどうかを示す値を取得、または設定します。
		/// </summary>
		[Description("コントロールがユーザーとの対話に応答できるかどうかを示す値を取得、または設定します。")]
		public bool UcEnabled
		{
			get
			{
				return this.Enabled;
			}
			set
			{
				this.Enabled = value;
				date.Enabled = value;
			}
		}
		/// <summary>
		/// カレンダーの選択可能最小日付を取得、または設定します。
		/// </summary>
		[Description("カレンダーの選択可能最小日付を取得、または設定します。")]
		public DateTime? UcCalenderMinDate
		{
			get
			{
				return date.DropDownCalendar.MinDate;
			}
			set
			{
				if (value != null)
				{
					// 最大日付＜最小日付だとエラーになるので、辻褄を合わせる。
					if (date.DropDownCalendar.MaxDate < value)
					{
						date.DropDownCalendar.MaxDate = value.Value.AddDays(1);
					}
					date.DropDownCalendar.MinDate = value;
				}
			}
		}
		/// <summary>
		/// カレンダーの選択可能最大日付を取得、または設定します。
		/// </summary>
		[Description("カレンダーの選択可能最大日付を取得、または設定します。")]
		public DateTime? UcCalenderMaxDate
		{
			get
			{
				return date.DropDownCalendar.MaxDate;
			}
			set
			{
				if (value != null)
				{
					// 最大日付＜最小日付だとエラーになるので、辻褄を合わせる。
					if (date.DropDownCalendar.MinDate > value)
					{
						date.DropDownCalendar.MinDate = value.Value.AddDays(-1);
					}
					date.DropDownCalendar.MaxDate = value;
				}
			}
		}
		#endregion
		
		#region *** Constructor ***
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public UcDate()
		{			
			InitializeComponent();

			this.date.Fields.Clear();
			this.date.DisplayFields.Clear();
			this.date.Fields.AddRange("ggg ee年MM月");
			this.date.DisplayFields.AddRange("ggg ee年MM月");
			
			this.date.DisabledForeColor = System.Drawing.Color.Black;
			this.date.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.date.ForeColor = System.Drawing.Color.Black;
			this.date.ExMaxDate = new System.DateTime(2100, 12, 31, 23, 59, 59, 0);
			this.date.ExMinDate = new System.DateTime(1868, 1, 1, 0, 0, 0, 0);
			this.date.Name = "date";
			this.date.ContentAlignment = ContentAlignment.BottomCenter;
			this.date.Value = DateTime.Now;

			//¶ 2017/1/19 kj 入力モードはUnfixedにする事で 01/01でも1/1のいずれでも登録が可能となる。
			this.date.FieldsEditMode = FieldsEditMode.Unfixed;

			//¶2010/07/07 K.Tada
			// 2010/09 ... 2010/09/[05] [14:03:27] （[]の部分は現在日時が勝手に入る）などと入る値を 2010/09/01 00:00:00 にするため。
			//¶2010/07/23 fj
			// 年が0001だと年不定の際にあまりに非現実な値となるため、今年の値を挿入。
			this.date.RecommendedValue = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0);
			// マウスホイールを無効に。
			this.date.Spin.AllowSpin = false;
			// カレンダーのヘッダ書式を指定値を使用するように。
			this.date.DropDownCalendar.UseHeaderFormat = true;

			this.SizeChanged += new EventHandler(UCDate_SizeChanged);

			// 初期フォーマット
			uc_format = UcDateFormat.YYYYMMDD;
			changeFormat();
			
			this.Load += new EventHandler(UcDate_Load);

			//¶2010/07/07 K.Tada コメントアウト
			//this.date.ValueChanged += new EventHandler(date_ValueChanged);
		}
		#endregion
		
		#region *** Event ***
		/// <summary>
		/// コントロールロード
		/// </summary>
		private void UcDate_Load(object sender, EventArgs e)
		{
			this.Enter += new EventHandler(UCDate_Enter);
			date.Enter += new EventHandler(date_Enter);
			date.ValueChanged += date_ValueChanged;
		}

		void date_ValueChanged(object sender, EventArgs e)
		{
			if (UcValueChanged != null)
			{
				UcValueChanged(sender, e);
			}
		}

		/// <summary>
		/// サイズが変更された時の処理
		/// </summary>
		void UCDate_SizeChanged(object sender, EventArgs e)
		{
			// 日付コントロールのサイズをUcに合わせる
			date.Size = this.Size;
		}

		/// <summary>
		/// ユーザーコントロールが選択された時の処理
		/// </summary>
		void UCDate_Enter(object sender, EventArgs e)
		{
			date.Select();
		}
		
		/// <summary>
		/// 日付入力コントロールが選択された時の処理
		/// </summary>
		void date_Enter(object sender, EventArgs e)
		{
			date.SelectAll();
		}
		
		//¶2010/07/07 K.Tada コメントアウト
		///// <summary>
		///// Value変更の際に呼ばれる処理
		///// </summary>
		//void date_ValueChanged(object sender, EventArgs e)
		//{
		//    // 指定されたフォーマットの、未設定値部分を期待通りの値でクリアします。
		//    refreshFormat();
		//}	
		#endregion
		
		#region *** Public Value ***
		/// <summary>
		/// 日付を西暦表示するか設定します。falseで和暦、trueで西暦表示します。
		/// ここで設定した表示モードは全てのUCDateに反映されます。
		/// </summary>
		/// <param name="SeirekiOn">falseで和暦、trueで西暦表示</param>
		public static void SetKoyomiSeirekiOn(bool SeirekiOn)
		{
			uc_koyomiSeirekiOn = SeirekiOn;
		}

		public void ChangedKoyomiSeirekiOn(bool seirekiOn)
		{
			changeFormat(seirekiOn);
			date.Refresh();
		}
		#endregion
		
		#region *** Private Value ***
		//¶2010/07/07 K.Tada コメントアウト（理由・ValueChangedで値をセットするとカレットが一番左に戻ってしまうため）
		///// <summary>
		///// 指定されたフォーマットの、未設定値部分を期待通りの値でクリアします。
		///// 2010/09 ... 2010/09/[05] [14:03:27] （[]の部分は現在日時が勝手に入る）などと入る値を 2010/09/01 00:00:00 にします。
		///// </summary>
		//void refreshFormat()
		//{
		//    if (date.Value == null)
		//    {
		//        return;
		//    }
		//    DateTime	dt = date.Value.Value;
			
		//    switch (uc_format)
		//    {
		//        case UcDateFormat.YYYY :
		//            date.Value = new DateTime(dt.Year, 1, 1, 0,0,0);
		//            break;
		//        case UcDateFormat.YYYYMM :
		//            date.Value = new DateTime(dt.Year, dt.Month, 1, 0,0,0);
		//            break;
		//        case UcDateFormat.YYYYMMDD :
		//            date.Value = new DateTime(dt.Year, dt.Month, dt.Day, 0,0,0);
		//            break;
		//        case UcDateFormat.YYYY_MM_DD :
		//            date.Value = new DateTime(dt.Year, dt.Month, dt.Day, 0,0,0);
		//            break;
		//    }
		//}
		
		/// <summary>
		/// フォーマット変更
		/// </summary>
		void changeFormat()
		{
			changeFormat(uc_koyomiSeirekiOn);
		}
		/// <summary>
		/// フォーマット変更
		/// </summary>
		void changeFormat(bool seirekiOn)
		{
			if (seirekiOn == false)
			{
				date.DropDownCalendar.HeaderFormat = "ggg ee年MM月";

				switch (uc_format)
				{
					case UcDateFormat.YYYY :
						date.Fields.Clear();
						date.DisplayFields.Clear();
						date.Fields.AddRange("g．ee年");
						date.DisplayFields.AddRange("ggg ee年");
						date.DropDownCalendar.CalendarType = CalendarType.YearMonth;
						date.DropDownCalendar.YearMonthFormat = new YearMonthFormat("ggg ee年", "M月");
						break;
					case UcDateFormat.YYYYMM :
						date.Fields.Clear();
						date.DisplayFields.Clear();
						date.Fields.AddRange("g. ee/MM");
						date.DisplayFields.AddRange("ggg ee年MM月");
						date.DropDownCalendar.CalendarType = CalendarType.YearMonth;
						date.DropDownCalendar.YearMonthFormat = new YearMonthFormat("ggg ee年", "M月");
						break;
					case UcDateFormat.YYYYMMDD :
						date.Fields.Clear();
						date.DisplayFields.Clear();
						date.Fields.AddRange("g. ee/MM/dd");
						date.DisplayFields.AddRange("ggg ee年MM月dd日");
						break;
					case UcDateFormat.YYYY_MM_DD :
						date.Fields.Clear();
						date.DisplayFields.Clear();
						date.Fields.AddRange("g. ee/MM/dd");
						date.DisplayFields.AddRange("ggg ee/MM/dd");
						break;
				}
			}
			else
			{
				date.DropDownCalendar.HeaderFormat = "yyyy年MM月";

				switch (uc_format)
				{
					case UcDateFormat.YYYY :
						date.Fields.Clear();
						date.DisplayFields.Clear();
						date.Fields.AddRange("yyyy");
						date.DisplayFields.AddRange("yyyy年");
						date.DropDownCalendar.CalendarType = CalendarType.YearMonth;
						date.DropDownCalendar.YearMonthFormat = new YearMonthFormat("yyyy年", "M月");
						break;
					case UcDateFormat.YYYYMM :
						date.Fields.Clear();
						date.DisplayFields.Clear();
						date.Fields.AddRange("yyyy/MM");
						date.DisplayFields.AddRange("yyyy年MM月");
						date.DropDownCalendar.CalendarType = CalendarType.YearMonth;
						date.DropDownCalendar.YearMonthFormat = new YearMonthFormat("yyyy年", "M月");
						break;
					case UcDateFormat.YYYYMMDD :
						date.Fields.Clear();
						date.DisplayFields.Clear();
						date.Fields.AddRange("yyyy/MM/dd");
						date.DisplayFields.AddRange("yyyy年MM月dd日");
						break;
					case UcDateFormat.YYYY_MM_DD :
						date.Fields.Clear();
						date.DisplayFields.Clear();
						date.Fields.AddRange("yyyy/MM/dd");
						date.DisplayFields.AddRange("yyyy/MM/dd");
						break;
				}

				
			}
			
			//¶2010/07/07 K.Tada コメントアウト
			//// 指定されたフォーマットの、未設定値部分を期待通りの値でクリアします。
			//refreshFormat();
		}
		#endregion
	}
}
