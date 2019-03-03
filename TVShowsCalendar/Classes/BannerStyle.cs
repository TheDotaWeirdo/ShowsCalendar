using Extensions;
using System.Drawing;

namespace ShowsCalendar.Classes
{
	public enum BannerStyle
	{
		Active,
		Green,
		Yellow,
		Red
	}

	public static class BannerStyleExtensions
	{
		public static Color BackColor(this BannerStyle style)
		{
			switch (style)
			{
				case BannerStyle.Active:
					return FormDesign.Design.ActiveForeColor;
				default:
					return FormDesign.Design.Type.If(FormDesignType.Light, FormDesign.Design.ForeColor, FormDesign.Design.MenuColor);
			}
		}

		public static Color ForeColor(this BannerStyle style)
		{
			switch (style)
			{
				case BannerStyle.Active:
					return FormDesign.Design.ActiveColor;
				case BannerStyle.Green:
					return FormDesign.Design.GreenColor;
				case BannerStyle.Yellow:
					return FormDesign.Design.YellowColor;
				case BannerStyle.Red:
					return FormDesign.Design.RedColor;
				default:
					return FormDesign.Design.ActiveColor;
			}
		}
	}
}
