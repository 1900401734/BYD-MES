using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MesDatas
{
    public static class FwelcomeExtension
    {
        /// <summary>
        /// Configures the welcome form for mode switching
        /// </summary>
        public static void ConfigureForModeSwitch(this Fwelcome loginForm, bool currentOfflineMode)
        {
            /*// Set the form to switch to the opposite mode
            loginForm.cboLoginMode.SelectedIndex = currentOfflineMode ? 0 : 1; // Switch to opposite mode
            loginForm.cboLoginMethod.SelectedIndex = 0; // Default to password login

            // Update window title to indicate mode change
            loginForm.Text = currentOfflineMode ? "切换到在线模式" : "切换到离线模式";

            // Disable changing the login mode (since we're explicitly switching)
            loginForm.cboLoginMode.Enabled = false;

            // Optional: Hide the exit button to prevent accidental application closure
            if (loginForm.Controls.Find("Button2", true).FirstOrDefault() is Button btnClose)
            {
                btnClose.Visible = false;
            }

            // Add instructions label
            Label lblInstructions = new Label();
            lblInstructions.Text = "请输入账号密码以" + (currentOfflineMode ? "切换到在线模式" : "切换到离线模式");
            lblInstructions.AutoSize = true;
            lblInstructions.ForeColor = Color.Blue;
            lblInstructions.Location = new Point(loginForm.Width / 2 - lblInstructions.Width / 2,
                                                loginForm.Controls["tbxUserID"].Location.Y - 30);
            loginForm.Controls.Add(lblInstructions);
            lblInstructions.BringToFront();*/

            // Wait for the form to fully load
            loginForm.Load += (sender, e) =>
            {
                try
                {
                    // Set the form to switch to the opposite mode
                    // Only set these after the form is fully loaded
                    if (loginForm.cboLoginMode.Items.Count > 0)
                    {
                        loginForm.cboLoginMode.SelectedIndex = currentOfflineMode ? 0 : 1; // Switch to opposite mode
                    }

                    if (loginForm.cboLoginMethod.Items.Count > 0)
                    {
                        loginForm.cboLoginMethod.SelectedIndex = 0; // Default to password login
                    }

                    // Update window title to indicate mode change
                    loginForm.Text = currentOfflineMode ? "切换到在线模式" : "切换到离线模式";

                    // Disable changing the login mode (since we're explicitly switching)
                    loginForm.cboLoginMode.Enabled = false;

                    // Optional: Hide the exit button to prevent accidental application closure
                    if (loginForm.Controls.Find("Button2", true).FirstOrDefault() is Button btnClose)
                    {
                        btnClose.Visible = false;
                    }

                    // Add instructions label
                    Label lblInstructions = new Label();
                    lblInstructions.Text = "请输入账号密码以" + (currentOfflineMode ? "切换到在线模式" : "切换到离线模式");
                    lblInstructions.AutoSize = true;
                    lblInstructions.ForeColor = Color.Blue;

                    // Make sure the textbox exists before referencing it
                    if (loginForm.Controls["tbxUserID"] != null)
                    {
                        lblInstructions.Location = new Point(
                            loginForm.Width / 2 - lblInstructions.Width / 2,
                            loginForm.Controls["tbxUserID"].Location.Y - 30);
                    }
                    else
                    {
                        // Fallback position
                        lblInstructions.Location = new Point(
                            loginForm.Width / 2 - lblInstructions.Width / 2,
                            100);
                    }

                    loginForm.Controls.Add(lblInstructions);
                    lblInstructions.BringToFront();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"配置登录窗口时出错：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
        }
    }
}
