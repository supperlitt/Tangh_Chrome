using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tangh_Chrome;

namespace WinTest
{
    public partial class MainFrm : Form
    {
        private ChromeHelper chrome = null;

        public MainFrm()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void btnSetKey_Click(object sender, EventArgs e)
        {

        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            #region chrome1
            this.chrome = new ChromeHelper("https://www.baidu.com/");
            this.chrome.NotifyResult += chrome_NotifyResult;
            chrome.InitHelper();
            List<string> list = new List<string>();
            list.AddRange(this.txtMonitorKey.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList());

            chrome.SetHtmlUrl(list);
            var browser1 = chrome.Init();
            AddBrowserToPanel(browser1, panel1);

            #endregion
        }

        private void chrome_NotifyResult(string obj)
        {
            this.ShowMsg(obj);
        }

        private void AddBrowserToPanel(Control browser, Panel panel)
        {
            this.Invoke(new Action<Panel>(p =>
            {
                if (p.Controls.Count > 0)
                {
                    p.Controls.RemoveAt(0);
                }

                p.Controls.Add(browser);
                p.Update();
            }), panel);
        }

        private void btnNav_Click(object sender, EventArgs e)
        {
            this.chrome.JumpUrl(this.txtUrl.Text);
        }

        private void ShowMsg(string msg)
        {
            this.Invoke(new Action<TextBox>(p =>
            {
                if (p.TextLength > 300000)
                {
                    p.Clear();
                }

                p.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" + msg + "\r\n\r\n");
            }), this.txtResult);
        }
    }
}
