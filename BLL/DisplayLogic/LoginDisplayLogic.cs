using BLL.DataLogic;
using BLL.Helper;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL.DisplayLogic
{
    public class LoginDisplayLogic
    {
        AccountDataLogic _accountDataLogic = new AccountDataLogic();

        TextBox _txtLoginUsername;
        TextBox _txtLoginPassword;
        
        MenuStrip _mnsMenu;
        SplitContainer _splMain;
        Panel _pnlLogin;

        tblAccount _loginAccount;

        public LoginDisplayLogic() { }

        public LoginDisplayLogic(TextBox txtLoginUsername, TextBox txtLoginPassword, MenuStrip mnsMenu, SplitContainer splMain, Panel pnlLogin)
        {
            _txtLoginUsername = txtLoginUsername;
            _txtLoginPassword = txtLoginPassword;
            _mnsMenu = mnsMenu;
            _splMain = splMain;
            _pnlLogin = pnlLogin;
        }

        public void ClickBtnLogin()
        {
            tblAccount inputAcc = new tblAccount() { Username = _txtLoginUsername.Text, Password = _txtLoginPassword.Text };
            
            _loginAccount = _accountDataLogic.FindAccount(inputAcc);
            
            ClearControlsContent();
            CheckAccount();
        }

        private void ClearControlsContent()
        {
            _txtLoginUsername.Text = "";
            _txtLoginPassword.Text = "";
        }

        private void CheckAccount()
        {
            if (!ValidateObj.IsAccountNull(_loginAccount))
            {
                if (_loginAccount.Type == 1)
                    EnableAdminMenuItems(true);
                else
                    EnableAdminMenuItems(false);
            }
        }

        private void EnableAdminMenuItems(bool flag)
        {
            _mnsMenu.Enabled = true;
            _mnsMenu.Items[1].Text = _loginAccount.DisplayName;
            _mnsMenu.Items[2].Enabled = flag;
            (_mnsMenu.Items[1] as ToolStripMenuItem).DropDownItems[1].Enabled = flag;
            _splMain.BringToFront();
        }

        public void ClickTsmiLogout()
        {
            _pnlLogin.BringToFront();
            _mnsMenu.Items[1].Text = "Account";
            _mnsMenu.Enabled = false;
        }

        public void ClickDisplayProfile(TextBox txtProfileID, TextBox txtProfileUsername, TextBox txtProfileDisplayName, TextBox txtProfileType)
        {
            txtProfileID.Text = _loginAccount.ID.ToString();
            txtProfileUsername.Text = _loginAccount.Username;
            txtProfileDisplayName.Text = _loginAccount.DisplayName;
            txtProfileType.Text = _loginAccount.Type == 1 ? "Administrative account" : "Normal account";
        }
    }
}
