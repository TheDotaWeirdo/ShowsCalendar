using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowsCalendar.Classes
{
	public enum MediaSortOptions
	{
		[Description("Relative Release Date")]
		Default,
		[Description("Year")]
		Year,
		[Description("Name")]
		Name,
		[Description("Genre")]
		Genre
	};
}