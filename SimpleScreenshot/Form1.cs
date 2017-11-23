using System.Windows.Forms;

namespace SimpleScreenshot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            HotkeyEvent1 = TriggerFormShow;
        }

        #region HotkeyReg
        
        private delegate void HotkeyEvent();
        private readonly HotkeyEvent HotkeyEvent1;
        
        private const int HotKeyID = 1; //热键ID（自定义）
        
        protected override void WndProc(ref Message msg)
        {
            base.WndProc(ref msg);
            switch (msg.Msg)
            {
                case 0x312://窗口消息：热键
                    var tmpWParam = msg.WParam.ToInt32();
                    if (tmpWParam == HotKeyID)
                    {
                        BeginInvoke(HotkeyEvent1);
                    }
                    break;
                case 0x1://窗口消息：创建
                    Hotkey.RegHotKey(Handle, HotKeyID, Hotkey.KeyModifiers.Ctrl | Hotkey.KeyModifiers.Shift, Keys.W);//注册热键
                    break;
                case 0x2://窗口消息：销毁
                    Hotkey.UnRegHotKey(Handle, HotKeyID);//销毁热键
                    break;
            }
        }

        private void TriggerFormShow()
        {
            Visible = !Visible;
        }

        #endregion
    }
}
