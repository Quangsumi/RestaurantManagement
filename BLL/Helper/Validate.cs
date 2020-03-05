using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL.Helper
{
    public static class Validate
    {
        public static bool IsValidText(TextBox txtInput)
            => (!String.IsNullOrWhiteSpace(txtInput.Text) 
            && !String.IsNullOrEmpty(txtInput.Text)) ? true : ShowInvalidTextError();

        private static bool ShowInvalidTextError()
        {
            MessageBox.Show("Textbox must be not empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        public static bool IsNumber(TextBox txtInput)
            => Double.TryParse(txtInput.Text, out double result) ? true : ShowNotANumberError();

        private static bool ShowNotANumberError()
        {
            MessageBox.Show("Price/Total Price must be number only!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        public static bool IsDigit(TextBox txtInput)
            => Int32.TryParse(txtInput.Text, out int result) ? true : ShowNotADigitError();

        private static bool ShowNotADigitError()
        {
            MessageBox.Show("Count/Discount must be digit only!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        public static bool IsOneAndZero(TextBox txtInput)
            => (IsOne(txtInput) || IsZero(txtInput)) ? true : ShowNotOneAndZeroError();

        private static bool IsOne(TextBox txtInput)
            => Int32.TryParse(txtInput.Text, out int result) && result == 1;

        private static bool IsZero(TextBox txtInput)
            => Int32.TryParse(txtInput.Text, out int result) && result == 0;

        private static bool ShowNotOneAndZeroError()
        {
            MessageBox.Show("Status/Count must be 1 or 0 only!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        public static bool IsValidID(TextBox txtInput)
            => Int32.TryParse(txtInput.Text, out int result) ? true : ShowInvalidIDError();

        private static bool ShowInvalidIDError()
        {
            MessageBox.Show("Invalid ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }
}
