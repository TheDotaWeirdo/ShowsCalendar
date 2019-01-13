using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace TVShowsCalendar.Classes
{
	public class ShowAssociation
	{
		public int ShowId { get; private set; }
		public List<DirectoryInfo> LinkedFolders { get; private set; }

		public ShowAssociation(int show, List<DirectoryInfo> linkedFolders)
		{
			ShowId = show;
			LinkedFolders = linkedFolders;
		}
	}
}
