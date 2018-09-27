using System;
namespace Chance.MvvmCross.Plugins.UserInteraction
{
    public struct ConfirmWithCheckboxResponse
    {
        public ConfirmWithCheckboxResponse(bool confirmed, bool checkBoxChecked)
        {
            Confirmed = confirmed;
            CheckBoxChecked = checkBoxChecked;
        }

        public bool Confirmed { get; }

        public bool CheckBoxChecked { get; }
    }
}
