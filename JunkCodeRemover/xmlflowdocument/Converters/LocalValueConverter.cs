using System;
using System.Globalization;
using System.Windows.Data;

namespace CJC.Wpf.Converters
{
	public class LocalValueConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
		{
			return ( value is DateTime )
				? ( (DateTime)value ).ToLocalTime()
				: value;
		}

		public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}