using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4.ViewModel
{
    public class ThemeService
    {
        public static event EventHandler<string> ThemeChanged;

        public static void ChangeTheme(string style)
        {
            ThemeChanged?.Invoke(null, style);
        }
    }
}
