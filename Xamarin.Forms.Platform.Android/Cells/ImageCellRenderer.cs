using System.ComponentModel;
using Android.Content;
using Android.Views;
using Android.OS;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Platform.Android
{
	public class ImageCellRenderer : TextCellRenderer
	{
		protected override global::Android.Views.View GetCellCore(Cell item, global::Android.Views.View convertView, ViewGroup parent, Context context)
		{
			var result = (BaseCellView)base.GetCellCore(item, convertView, parent, context);

			UpdateImage();
			UpdateFlowDirection();

			return result;
		}

		protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			base.OnCellPropertyChanged(sender, args);
			if (args.PropertyName == ImageCell.ImageSourceProperty.PropertyName)
				UpdateImage();
			else if (args.PropertyName == VisualElement.FlowDirectionProperty.PropertyName)
				UpdateFlowDirection();
		}

		void UpdateImage()
		{
			var cell = (ImageCell)Cell;
			if (cell.ImageSource != null)
			{
				View.SetImageVisible(true);
				View.SetImageSource(cell.ImageSource);
			}
			else
				View.SetImageVisible(false);
		}

		void UpdateFlowDirection()
		{
			if (VisualElementController == null || (int)Build.VERSION.SdkInt < 17)
				return;

			if (VisualElementController.EffectiveFlowDirection.IsRightToLeft())
				View.LayoutDirection = LayoutDirection.Rtl;
			else if (VisualElementController.EffectiveFlowDirection.IsLeftToRight())
				View.LayoutDirection = LayoutDirection.Ltr;
		}
	}
}