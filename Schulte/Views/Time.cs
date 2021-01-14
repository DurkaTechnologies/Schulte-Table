using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Schulte.Views
{
	public class Time
	{
		private int seconds;
		private int minutes;

		private readonly int MaxSecondValue = 59;
		private readonly int MinSecondValue = 0;
		private readonly int MaxMinuteValue = 59;
		private readonly int MinMinuteValue = 0;

		public Time()
		{
			ResetTime();
		}

		public Time(int seconds, int minutes)
		{
			Seconds = seconds;
			Minutes = minutes;
		}

		public Time(TimeSpan time)
		{
			Seconds = time.Seconds;
			Minutes = time.Minutes;
		}

		public void ResetTime()
		{
			Seconds = MinSecondValue;
			Minutes = MinMinuteValue;
		}

		public bool IsReset => (Seconds == MinSecondValue) && (Minutes == MinMinuteValue);

		public int Seconds 
		{ 
			get => seconds; 
			set
			{
				if (value >= MinSecondValue && value <= MaxSecondValue)
				{
					seconds = value;
					UpdateTimeString();
				}
			}
		}

		public int Minutes 
		{
			get => minutes;
			set
			{
				if (value >= MinMinuteValue && value <= MaxMinuteValue)
				{
					minutes = value;
					UpdateTimeString();
				}
			}
		}

		public string Text { get; set; }

		public void AddSecond()
		{
			if (Seconds == MaxSecondValue)
			{
				if (Minutes != MaxMinuteValue)
					Minutes++;
				else
					Minutes = MinMinuteValue;
				Seconds = MinSecondValue;

			}
			else
				Seconds++;
		}

		public void MinusSecond()
		{
			if (Seconds == MinSecondValue)
			{
				if (Minutes != MinMinuteValue)
				{
					Minutes--;
					Seconds = MaxSecondValue;
				}
			}
			else
				Seconds--;
		}

		public bool TimeIsReset => (Seconds == 0) && (Minutes == 0);

		public void SetTime(int minutes, int seconds)
		{
			Seconds = seconds;
			Minutes = minutes;

		}

		public void SetTime(TimeSpan time)
		{
			Seconds = time.Seconds;
			Minutes = time.Minutes;
		}

		public override string ToString() => Text;

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

		private void UpdateTimeString()
		{
			if (Seconds == MinSecondValue)
				Text = (Minutes < 10 ? $"0{Minutes}" : $"{Minutes}") + ":00";
			else
				Text = (Minutes < 10 ? $"0{Minutes}" : $"{Minutes}") + ':'
					+ (Seconds < 10 ? $"0{Seconds}" : $"{Seconds}");
		}
	}
}