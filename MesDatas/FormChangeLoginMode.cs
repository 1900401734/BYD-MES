using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MesDatas
{
    public partial class FormChangeLoginMode : Form
    {
        private Form1 _parentForm;
        private bool _currentOfflineMode;
        private string _loginUser = "";
        private string _loginName = "";
        private string _loginPwd = "";
        private int _accessLevel = 0;
        private bool _loginSuccess = false;

        // 获取登录结果
        public bool LoginSuccess { get { return _loginSuccess; } }
        public string LoginUser { get { return _loginUser; } }
        public string LoginName { get { return _loginName; } }
        public string LoginPassword { get { return _loginPwd; } }
        public int AccessLevel { get { return _accessLevel; } }

        public FormChangeLoginMode(Form1 parentForm, bool currentOfflineMode)
        {
            InitializeComponent();
            _parentForm = parentForm;
            _currentOfflineMode = currentOfflineMode;
        }

        /*public ModeChangeLoginForm()
        {
            InitializeComponent();
        }*/

        private void ModeChangeLoginForm_Load(object sender, EventArgs e)
        {
            // 更新窗口标题
            lblTitle.Text = $"请输入账号密码以切换到{(_currentOfflineMode ? "在线" : "离线")}模式";

            // 设置回车键触发登录
            this.AcceptButton = btnLogin;

            // 聚焦用户名输入框
            txtUsername.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("请输入工号和密码！", "登录错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 从数据库加载用户数据
                MDBHelper mdb = new MDBHelper("D:\\BYD_Users\\Users_Data.MDB");
                DataTable userTable = mdb.Find($"SELECT * FROM Users WHERE 工号 = '{txtUsername.Text}' AND 用户密码 = '{txtPassword.Text}'");
                mdb.CloseConnection();

                if (userTable.Rows.Count > 0)
                {
                    DataRow user = userTable.Rows[0];
                    string userType = user["用户权限"].ToString();

                    // 检查用户是否有权切换到离线模式
                    if (!_currentOfflineMode && userType == "OP")
                    {
                        MessageBox.Show("操作员不能切换到离线模式！", "权限不足", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 存储登录信息
                    _loginUser = user["工号"].ToString();
                    _loginName = user["用户名"].ToString();
                    _loginPwd = user["用户密码"].ToString();

                    // 设置访问级别
                    switch (userType)
                    {
                        case "ADM": _accessLevel = 3; break;
                        case "PE": _accessLevel = 2; break;
                        case "DEV": _accessLevel = 4; break;
                        case "QE": _accessLevel = 5; break;
                        case "ME": _accessLevel = 6; break;
                        case "OP": _accessLevel = 1; break;
                        default: _accessLevel = 0; break;
                    }

                    _loginSuccess = true;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("用户名或密码错误！", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"登录验证过程中发生错误：{ex.Message}", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }

    }
}
