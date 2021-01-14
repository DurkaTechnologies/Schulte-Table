using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schulte.Views
{
	public static class StartTimeResolver
	{
		public static TimeSpan EasyModeTime => new TimeSpan(0, 0, 45);
		public static TimeSpan MiddleModeTime => new TimeSpan(0, 0, 25);
		public static TimeSpan HardModeTime => new TimeSpan(0, 0, 5);
	}
}
