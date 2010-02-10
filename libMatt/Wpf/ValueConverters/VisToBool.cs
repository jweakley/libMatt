﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Globalization;


namespace libMatt.Wpf {

	/// <summary>
	/// Invertible BoolToVisibility converter courtesy of Dr. WPF,
	/// http://social.msdn.microsoft.com/Forums/en-US/wpf/thread/f2154f63-ccb5-4d6d-8c01-81f9da9ab347/
	/// </summary>
	public class VisToBool : IValueConverter {

		private bool _inverted = false;
		public bool Inverted {
			get { return _inverted; }
			set { _inverted = value; }
		}

		private bool _not = false;
		public bool Not {
			get { return _not; }
			set { _not = value; }
		}

		private object VisibilityToBool(object value) {
			if (!(value is Visibility))
				return DependencyProperty.UnsetValue;

			return (((Visibility)value) == Visibility.Visible) ^ Not;
		}

		private object BoolToVisibility(object value) {
			if (!(value is bool))
				return DependencyProperty.UnsetValue;

			return ((bool)value ^ Not) ? Visibility.Visible
				: Visibility.Collapsed;
		}

		public object Convert(object value, Type targetType,
				object parameter, CultureInfo culture) {

			return Inverted ? BoolToVisibility(value)
				: VisibilityToBool(value);
		}

		public object ConvertBack(object value, Type targetType,
				object parameter, CultureInfo culture) {

			return Inverted ? VisibilityToBool(value)
				: BoolToVisibility(value);
		}
	}

}
