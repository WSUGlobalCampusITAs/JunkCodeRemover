using System;
using System.Globalization;
using System.Windows.Data;

namespace CJC.Wpf.Converters
{
	class StringFormatConverter : IMultiValueConverter
	{
		#region IMultiValueConverter Members

		public object Convert( object[] values, Type targetType, object parameter, CultureInfo culture )
		{
			return string.Format( parameter.ToString(), values );
		}

		public object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture )
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}