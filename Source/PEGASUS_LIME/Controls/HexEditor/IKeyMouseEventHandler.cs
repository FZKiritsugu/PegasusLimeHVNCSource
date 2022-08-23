using System;
using System.Windows.Forms;

namespace PEGASUS_LIME.Controls.HexEditor
{
	public interface IKeyMouseEventHandler
	{
		void OnKeyPress(KeyPressEventArgs e);

		void OnKeyDown(KeyEventArgs e);

		void OnKeyUp(KeyEventArgs e);

		void OnMouseDown(MouseEventArgs e);

		void OnMouseDragged(MouseEventArgs e);

		void OnMouseUp(MouseEventArgs e);

		void OnMouseDoubleClick(MouseEventArgs e);

		void OnGotFocus(EventArgs e);
	}
}