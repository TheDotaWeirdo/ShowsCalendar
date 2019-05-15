using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowsCalendar.Classes
{
	public struct TorrentSortingData
	{
		public string Name { get; set; }
		public int Res { get; set; }
		public int SeedOrder { get; set; }
		public int Size { get; set; }
		public int Sound { get; set; }
		public string Subs { get; set; }
		public TorrentTile Tile { get; set; }
		public double Health { get; set; }
	}
}