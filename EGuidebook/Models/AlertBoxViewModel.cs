using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EGuidebook.Models
{
    public enum EnumAlertBoxViewModel
    {
        SUCCESS = 0,
        INFO = 1,
        WARNING = 2,
        ERROR = 3
    }

    public class AlertBoxViewModel
    {
        public string HtmlID { get; private set; }
        public string Text { get; private set; }
        public EnumAlertBoxViewModel Type { get; private set; }

        public string CSSClass
        {
            get
            {
                string strRetValue = "";
                switch(this.Type)
                {
                    case EnumAlertBoxViewModel.SUCCESS:
                        strRetValue = "success";
                        break;
                    case EnumAlertBoxViewModel.INFO:
                        strRetValue = "info";
                        break;
                    case EnumAlertBoxViewModel.WARNING:
                        strRetValue = "warning";
                        break;
                    case EnumAlertBoxViewModel.ERROR:
                        strRetValue = "danger";
                        break;
                }

                return strRetValue;
            }
        }

        public string Header
        {
            get
            {
                string strRetValue = "";
                switch (this.Type)
                {
                    case EnumAlertBoxViewModel.SUCCESS:
                        strRetValue = "Sukces!";
                        break;
                    case EnumAlertBoxViewModel.INFO:
                        strRetValue = "Informacja!";
                        break;
                    case EnumAlertBoxViewModel.WARNING:
                        strRetValue = "Ostrzeżenie!";
                        break;
                    case EnumAlertBoxViewModel.ERROR:
                        strRetValue = "Błąd!";
                        break;
                }

                return strRetValue;

            }
        }

        public AlertBoxViewModel(string strHtmlID, string strText, EnumAlertBoxViewModel Type)
        {
            this.HtmlID = strHtmlID;
            this.Text = strText;
            this.Type = Type;
        }
    }
}