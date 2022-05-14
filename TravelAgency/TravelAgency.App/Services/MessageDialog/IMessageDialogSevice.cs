using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.App.Services.MessageDialog
{
    public interface IMessageDialogService
    {
        MessageDialogResult Show(
            string title,
            string caption,
            MessageDialogButtonConfiguration buttonConfiguration,
            MessageDialogResult defaultResult);
    }

}
