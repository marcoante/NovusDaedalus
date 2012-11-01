using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace Novus_Daedalus.Model
{
    // Classe per la conversione dei messaggi di errore dei controlli sull'input dell'utente
    class ErrorsToMessageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
      System.Globalization.CultureInfo culture)
        {
            var sb = new StringBuilder();
            var errors = value as ReadOnlyCollection<ValidationError>;
            if (errors != null)
            {
                foreach (var e in errors.Where(e => e.ErrorContent != null))
                { sb.AppendLine(e.ErrorContent.ToString()); }
            }

            return sb.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter,
          System.Globalization.CultureInfo culture)
        { throw new NotImplementedException(); }
    }
}