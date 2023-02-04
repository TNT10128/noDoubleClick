using Gma.System.MouseKeyHook;
using System.Windows.Forms;

namespace NoDoubleClick
{
    class MouseListener
    {
        private static readonly MouseButtons[] s_mouseButtonsToSuppress =
        {
            MouseButtons.Left,
            MouseButtons.Right
        };

        private readonly IKeyboardMouseEvents _mouseKeyHook = Hook.GlobalEvents();

        public void Subscribe() => _mouseKeyHook.MouseDownExt += OnMouseDown!;

        public void Unsubscribe()
        {
            _mouseKeyHook.MouseDownExt -= OnMouseDown!;
            _mouseKeyHook.Dispose();
        }

        private static void OnMouseDown(object sender, MouseEventExtArgs e)
        {
            if (!s_mouseButtonsToSuppress.Contains(e.Button)) 
            {
                return;
            }
            var doubleClickPreventer = e.Button == MouseButtons.Left 
                ? Program.LeftButtonDoubleClickPreventer : Program.RightButtonDoubleClickPreventer;
            if (doubleClickPreventer.IsDoubleClick(e))
            {
                e.Handled = true;
                doubleClickPreventer.LogClickSuppressedMessage();
            }
            doubleClickPreventer.LastClickTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }
    }
}
