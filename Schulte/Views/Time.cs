using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Schulte.Views
{
	public class Time : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public Time()
		{
			Seconds = 0;
			Minutes = 0;
			Text = "00:00";
		}

		public Time(int seconds, int minutes)
		{
			Seconds = seconds;
			Minutes = minutes;
			Text = "00:00";
		}
		
		public void ResetTime()
		{
			Seconds = 0;
			Minutes = 0;
			Text = "00:00";
		}
		public bool IsReset()
		{
			return ((Seconds == 0) && (Minutes == 0));
		}
		public int Seconds { get; set; }
		public int Minutes { get; set; }
		public string Text { get; set; }

		public void AddSecond()
		{
			++Seconds;
			if (Seconds % 60 == 0)
			{
				Minutes++;
				Seconds = 0;
				Text = (Minutes < 10 ? $"0{Minutes}" : $"{Minutes}") + ":00";
			}
			else
				Text = (Minutes < 10 ? $"0{Minutes}" : $"{Minutes}") + ':'
					+ (Seconds < 10 ? $"0{Seconds}" : $"{Seconds}");

		}

		public override string ToString()
		{
			return Text;
		}
		public static bool operator <=(Time left, Time right)
		{
			if (left.Minutes < right.Minutes)
				return true;
			else if (left.Minutes == right.Minutes)
				return left.Seconds <= right.Seconds;
			else
				return false;

		}
		public static bool operator >=(Time left, Time right)
		{
			if (left.Minutes > right.Minutes)
				return true;
			else if (left.Minutes == right.Minutes)
				return left.Seconds >= right.Seconds;
			else
				return false;

		}

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

	}
}