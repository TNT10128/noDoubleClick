using Gma.System.MouseKeyHook;
using Microsoft.Extensions.Logging;
using System.Windows.Forms;

namespace NoDoubleClick
{
    internal class DoubleClickPreventer
    {
        public long LastClickTime = 0;
        private readonly MouseButtons _mouseButtonType;

        public DoubleClickPreventer(MouseButtons mouseButtonType) => _mouseButtonType = mouseButtonType;

        public bool IsDoubleClick(MouseEventExtArgs e) =>
            e.Button == _mouseButtonType && (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - LastClickTime) < 50;

        public void LogClickSuppressedMessage() => 
            Program.Logger.LogInformation(
                "A double click of the {button} button was suppressed.", _mouseButtonType.ToString().ToLower()
            );
    }
}
