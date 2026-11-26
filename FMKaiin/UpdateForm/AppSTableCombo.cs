using System.Windows.Forms;
using ComponentSUI;
using ComponentDB;

namespace App
{
    /// <summary>
    /// テーブルの行を選ぶコンボ ボックス（<see cref="STableComboBox"/>）の、一覧の内容の設定。
    /// S 系のコントロールを使う画面用。Gc 系のコントロールを使う画面用の <see cref="AppTableCombo"/> に当たる。
    /// </summary>
    /// <remarks>
    /// AppTableCombo と同じ内容（列・幅・並び順）を、S 系のコントロール用に書いたもの。画面を S 系へ移している間は、両方を持つ。
    /// 一覧の内容を変えるときは、両方を同じように直すこと。このクラスは Gc 系を参照しない。
    /// いまは、S 系へ移した画面が使うものだけを用意してある。
    /// </remarks>
    public static class AppSTableCombo
    {
        /// <summary>
        /// 銀行の一覧に表示する「銀行名」の列の名前。銀行情報（<see cref="AppBank"/>）が、テーブルに追加している列。
        /// AppTableCombo.Fld_BankName と同じ値であること。
        /// </summary>
        public const string Fld_BankName = "Fld_BankName";

        /// <summary>
        /// 銀行の一覧を設定する。選択後は銀行名を表示し、値は銀行の ID。
        /// 銀行情報が変わった後に呼び直すと、一覧を作り直す。
        /// </summary>
        public static void SetComboBox_Bank(STableComboBox cmb)
        {
            cmb.DataSource = AppGlobal.Banks.DbView.DataTable;
            cmb.Sort = DBQuery.GetSql(t_bank.FBNK_Code);

            cmb.ClearColumns();
            cmb.AddColumn(t_bank.FBNK_Code, "銀行ｺｰﾄﾞ", 80, HorizontalAlignment.Right);
            cmb.AddColumn(t_bank.FBNK_CodeShiten, "支店ｺｰﾄﾞ", 80, HorizontalAlignment.Right);
            cmb.AddColumn(Fld_BankName, "銀行名", 310);

            cmb.DisplayMember = Fld_BankName;
            cmb.ValueMember = t_bank.FID_Bank;

            cmb.DropDownWidth = 470;
            cmb.MaxDropDownItems = 12;
            cmb.ImeMode = ImeMode.Hiragana;
        }

        /// <summary>
        /// 診療科の一覧を設定する。選択後は名称を表示し、値は診療科の ID。
        /// AppTableCombo.SetComboBox_Shinryoka と同じ内容。
        /// </summary>
        /// <param name="cmb">設定するコントロール</param>
        /// <param name="dv">診療科の表（t_shinryoka）のビュー。一覧は、このビューのテーブルから作る（ビューの絞り込みと並び順は使わない）</param>
        public static void SetComboBox_Shinryoka(STableComboBox cmb, DBView dv)
        {
            cmb.DataSource = dv.DataTable;
            cmb.Sort = DBQuery.GetSql(t_shinryoka.FSRK_Code);

            cmb.ClearColumns();
            cmb.AddColumn(t_shinryoka.FSRK_Code, "コード", 80, HorizontalAlignment.Right);
            cmb.AddColumn(t_shinryoka.FSRK_Name, "名称", 220);

            cmb.DisplayMember = t_shinryoka.FSRK_Name;
            cmb.ValueMember = t_shinryoka.FID_Shinryoka;

            cmb.DropDownWidth = 300;
            cmb.MaxDropDownItems = 12;
            cmb.ImeMode = ImeMode.Hiragana;
        }

        /// <summary>
        /// 開設主体の一覧を設定する。選択後は名称を表示し、値は開設主体の ID。
        /// AppTableCombo.SetComboBox_Kaisetsushutai と同じ内容。
        /// </summary>
        /// <param name="cmb">設定するコントロール</param>
        /// <param name="dv">開設主体の表（t_kaisetsushutai）のビュー。一覧は、このビューのテーブルから作る（ビューの絞り込みと並び順は使わない）</param>
        public static void SetComboBox_Kaisetsushutai(STableComboBox cmb, DBView dv)
        {
            cmb.DataSource = dv.DataTable;
            cmb.Sort = DBQuery.GetSql(t_kaisetsushutai.FKST_Code);

            cmb.ClearColumns();
            cmb.AddColumn(t_kaisetsushutai.FKST_Code, "コード", 80, HorizontalAlignment.Right);
            cmb.AddColumn(t_kaisetsushutai.FKST_Name, "名称", 220);

            cmb.DisplayMember = t_kaisetsushutai.FKST_Name;
            cmb.ValueMember = t_kaisetsushutai.FID_Kaisetsushutai;

            cmb.DropDownWidth = 300;
            cmb.MaxDropDownItems = 12;
            cmb.ImeMode = ImeMode.Hiragana;
        }

        /// <summary>
        /// iFAX グループの一覧を設定する。選択後は名称を表示し、値は iFAX グループの ID。
        /// AppTableCombo.SetComboBox_IfaxGroup と同じ内容。
        /// </summary>
        /// <param name="cmb">設定するコントロール</param>
        /// <param name="dv">iFAX グループの表（t_ifax）のビュー。一覧は、このビューのテーブルから作る（ビューの絞り込みと並び順は使わない）</param>
        public static void SetComboBox_IfaxGroup(STableComboBox cmb, DBView dv)
        {
            cmb.DataSource = dv.DataTable;
            cmb.Sort = DBQuery.GetSql(t_ifax.FCD_iFax);

            cmb.ClearColumns();
            cmb.AddColumn(t_ifax.FCD_iFax, "コード", 80, HorizontalAlignment.Right);
            cmb.AddColumn(t_ifax.FIFX_Name, "名称", 220);

            cmb.DisplayMember = t_ifax.FIFX_Name;
            cmb.ValueMember = t_ifax.FID_iFax;

            cmb.DropDownWidth = 300;
            cmb.MaxDropDownItems = 12;
            cmb.ImeMode = ImeMode.Hiragana;
        }

        /// <summary>
        /// 組コードの一覧を設定する。選択後は名称を表示し、値は組コードの ID。
        /// AppTableCombo.SetComboBox_KumiCode と同じ内容。
        /// </summary>
        /// <param name="cmb">設定するコントロール</param>
        /// <param name="dv">組コードの表（t_kumicd）のビュー。一覧は、このビューのテーブルから作る（ビューの絞り込みと並び順は使わない）</param>
        public static void SetComboBox_KumiCode(STableComboBox cmb, DBView dv)
        {
            cmb.DataSource = dv.DataTable;
            cmb.Sort = DBQuery.GetSql(t_kumicd.FCD_KumiCode);

            cmb.ClearColumns();
            cmb.AddColumn(t_kumicd.FCD_KumiCode, "コード", 80, HorizontalAlignment.Right);
            cmb.AddColumn(t_kumicd.FKMC_Name, "名称", 220);

            cmb.DisplayMember = t_kumicd.FKMC_Name;
            cmb.ValueMember = t_kumicd.FID_KumiCode;

            cmb.DropDownWidth = 300;
            cmb.MaxDropDownItems = 12;
            cmb.ImeMode = ImeMode.Hiragana;
        }

        /// <summary>
        /// 会員の一覧を設定する。選択後は氏名を表示し、値は会員の ID。
        /// AppTableCombo.SetComboBox_Kaiin と同じ内容。
        /// </summary>
        /// <param name="cmb">設定するコントロール</param>
        /// <param name="dv">会員の表（t_kaiin）のビュー。一覧は、このビューのテーブルから作る（ビューの絞り込みと並び順は使わない）</param>
        public static void SetComboBox_Kaiin(STableComboBox cmb, DBView dv)
        {
            cmb.DataSource = dv.DataTable;
            cmb.Sort = DBQuery.GetSql(t_kaiin.FCD_Kaiin);

            cmb.ClearColumns();
            cmb.AddColumn(t_kaiin.FCD_Kaiin, "コード", 80, HorizontalAlignment.Right);
            cmb.AddColumn(t_kaiin.FKaiin_Name, "氏名", 150);
            cmb.AddColumn(t_kaiin.FKaiin_NameKana, "フリガナ", 210);

            cmb.DisplayMember = t_kaiin.FKaiin_Name;
            cmb.ValueMember = t_kaiin.FID_Kaiin;

            cmb.DropDownWidth = 440;
            cmb.MaxDropDownItems = 12;
            cmb.ImeMode = ImeMode.Hiragana;
        }

        /// <summary>
        /// 医療機関の一覧を設定する。選択後はコードを表示し、値は医療機関の ID。
        /// AppTableCombo.SetComboBox_IryoKikan と同じ内容。
        /// </summary>
        /// <param name="cmb">設定するコントロール</param>
        /// <param name="dv">医療機関の表（t_iryokikan）のビュー。一覧は、このビューのテーブルから作る（ビューの絞り込みと並び順は使わない）</param>
        public static void SetComboBox_IryoKikan(STableComboBox cmb, DBView dv)
        {
            cmb.DataSource = dv.DataTable;
            cmb.Sort = DBQuery.GetSql(t_iryokikan.FIRK_Code);

            cmb.ClearColumns();
            cmb.AddColumn(t_iryokikan.FIRK_Code, "コード", 80, HorizontalAlignment.Right);
            cmb.AddColumn(t_iryokikan.FIRK_Name, "名称", 220);

            cmb.DisplayMember = t_iryokikan.FIRK_Code;
            cmb.ValueMember = t_iryokikan.FID_Iryokikan;

            cmb.DropDownWidth = 300;
            cmb.MaxDropDownItems = 12;
            cmb.ImeMode = ImeMode.Hiragana;
        }

        /// <summary>
        /// 会員の一覧を設定する。選択後は会員コードを表示し、値は会員の ID。
        /// AppTableCombo.SetComboBox_KaiinCode と同じ内容（<see cref="SetComboBox_Kaiin"/> との違いは、選択後に表示する列だけ）。
        /// </summary>
        /// <param name="cmb">設定するコントロール</param>
        /// <param name="dv">会員の表（t_kaiin）のビュー。一覧は、このビューのテーブルから作る（ビューの絞り込みと並び順は使わない）</param>
        public static void SetComboBox_KaiinCode(STableComboBox cmb, DBView dv)
        {
            SetComboBox_Kaiin(cmb, dv);
            cmb.DisplayMember = t_kaiin.FCD_Kaiin;
        }

        /// <summary>
        /// 会費の一覧を設定する。選択後は名称を表示し、値は会費の ID。
        /// AppTableCombo.SetComboBox_Kaihi と同じ内容。ほかの一覧と違い、ビューの絞り込みと並び順を、そのまま使う
        /// （会費は、画面によって、一覧に出す範囲と並び順が違うため）。
        /// </summary>
        /// <param name="cmb">設定するコントロール</param>
        /// <param name="dv">会費の表（t_kaihi）のビュー。絞り込み（RowFilter）と並び順（Sort）を、一覧に使う</param>
        public static void SetComboBox_Kaihi(STableComboBox cmb, DBView dv)
        {
            cmb.DataSource = dv.DataTable;
            cmb.RowFilter = dv.RowFilter;
            cmb.Sort = dv.Sort;

            cmb.ClearColumns();
            cmb.AddColumn(t_kaihi.FCD_Kaihi, "コード", 80, HorizontalAlignment.Right);
            cmb.AddColumn(t_kaihi.FKaihi_Name, "名称", 220);

            cmb.DisplayMember = t_kaihi.FKaihi_Name;
            cmb.ValueMember = t_kaihi.FID_Kaihi;

            cmb.DropDownWidth = 300;
            cmb.MaxDropDownItems = 12;
            cmb.ImeMode = ImeMode.Hiragana;
        }

        /// <summary>
        /// 会費の一覧に表示する「会費区分」の列の名前。会費情報（<see cref="AppKaihi"/>）が、テーブルに追加している列。
        /// AppTableCombo.Fld_TypeKaihiName と同じ値であること。
        /// </summary>
        public const string Fld_TypeKaihiName = "Fld_TypeKaihiName";

        /// <summary>
        /// 会費の一覧を、会費区分の列つきで設定する。選択後は名称を表示し、値は会費の ID。
        /// AppTableCombo.SetComboBox_Kaihi2 と同じ内容。<see cref="SetComboBox_Kaihi"/> と同じく、ビューの絞り込みと並び順を、そのまま使う。
        /// </summary>
        /// <param name="cmb">設定するコントロール</param>
        /// <param name="dv">会費情報のビュー（AppGlobal.Kaihis.DbView から作ったもの。会費区分の列を持つ）。絞り込みと並び順を、一覧に使う</param>
        public static void SetComboBox_Kaihi2(STableComboBox cmb, DBView dv)
        {
            cmb.DataSource = dv.DataTable;
            cmb.RowFilter = dv.RowFilter;
            cmb.Sort = dv.Sort;

            cmb.ClearColumns();
            cmb.AddColumn(Fld_TypeKaihiName, "会費区分", 90);
            cmb.AddColumn(t_kaihi.FCD_Kaihi, "コード", 80, HorizontalAlignment.Right);
            cmb.AddColumn(t_kaihi.FKaihi_Name, "名称", 220);

            cmb.DisplayMember = t_kaihi.FKaihi_Name;
            cmb.ValueMember = t_kaihi.FID_Kaihi;

            cmb.DropDownWidth = 390;
            cmb.MaxDropDownItems = 12;
            cmb.ImeMode = ImeMode.Hiragana;
        }

        /// <summary>
        /// 医師会の一覧を設定する。選択後は名称を表示し、値は医師会の ID。
        /// AppTableCombo.SetComboBox_Ishikai と同じ内容。
        /// </summary>
        /// <param name="cmb">設定するコントロール</param>
        /// <param name="dv">医師会の表（t_ishikai）のビュー。一覧は、このビューのテーブルから作る（ビューの絞り込みと並び順は使わない）</param>
        public static void SetComboBox_Ishikai(STableComboBox cmb, DBView dv)
        {
            cmb.DataSource = dv.DataTable;
            cmb.Sort = DBQuery.GetSql(t_ishikai.FCD_Ishikai);

            cmb.ClearColumns();
            cmb.AddColumn(t_ishikai.FCD_Ishikai, "コード", 80, HorizontalAlignment.Right);
            cmb.AddColumn(t_ishikai.FISK_Name, "名称", 250);

            cmb.DisplayMember = t_ishikai.FISK_Name;
            cmb.ValueMember = t_ishikai.FID_Ishikai;

            cmb.DropDownWidth = 330;
            cmb.MaxDropDownItems = 12;
            cmb.ImeMode = ImeMode.Hiragana;
        }

        /// <summary>
        /// 学会の一覧を設定する。選択後は名称を表示し、値は学会の ID。表示名のある行（使っている行）だけを出す。
        /// AppTableCombo.SetComboBox_Gakkai と同じ内容。
        /// </summary>
        /// <param name="cmb">設定するコントロール</param>
        /// <param name="dv">学会の表（t_gakkai）のビュー。一覧は、このビューのテーブルから作る（ビューの絞り込みと並び順は使わない）</param>
        public static void SetComboBox_Gakkai(STableComboBox cmb, DBView dv)
        {
            cmb.DataSource = dv.DataTable;
            cmb.RowFilter = DBQuery.GetSql($"{t_gakkai.FGKAI_DispName} Is Not Null"); // 使用行
            cmb.Sort = DBQuery.GetSql(t_gakkai.FGKAI_Code);

            cmb.ClearColumns();
            cmb.AddColumn(t_gakkai.FGKAI_Code, "コード", 80, HorizontalAlignment.Right);
            cmb.AddColumn(t_gakkai.FGKAI_DispName, "名称", 230);

            cmb.DisplayMember = t_gakkai.FGKAI_DispName;
            cmb.ValueMember = t_gakkai.FID_Gakkai;

            cmb.DropDownWidth = 310;
            cmb.MaxDropDownItems = 12;
            cmb.ImeMode = ImeMode.Hiragana;
        }

        /// <summary>
        /// 施設・業務の一覧を設定する。選択後は名称を表示し、値は施設・業務の ID。表示名のある行（使っている行）だけを出す。
        /// AppTableCombo.SetComboBox_Shisetsugyomu と同じ内容。
        /// </summary>
        /// <param name="cmb">設定するコントロール</param>
        /// <param name="dv">施設・業務の表（t_shisetsugyomu）のビュー。一覧は、このビューのテーブルから作る（ビューの絞り込みと並び順は使わない）</param>
        public static void SetComboBox_Shisetsugyomu(STableComboBox cmb, DBView dv)
        {
            cmb.DataSource = dv.DataTable;
            cmb.RowFilter = DBQuery.GetSql($"{t_shisetsugyomu.FSGY_DispName} Is Not Null"); // 使用行
            cmb.Sort = DBQuery.GetSql(t_shisetsugyomu.FSGY_Code);

            cmb.ClearColumns();
            cmb.AddColumn(t_shisetsugyomu.FSGY_Code, "コード", 80, HorizontalAlignment.Right);
            cmb.AddColumn(t_shisetsugyomu.FSGY_DispName, "名称", 250);

            cmb.DisplayMember = t_shisetsugyomu.FSGY_DispName;
            cmb.ValueMember = t_shisetsugyomu.FID_ShisetsuGyomu;

            cmb.DropDownWidth = 330;
            cmb.MaxDropDownItems = 12;
            cmb.ImeMode = ImeMode.Hiragana;
        }
    }
}
