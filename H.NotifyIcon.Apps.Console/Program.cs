using System.Drawing;
using System.Runtime.InteropServices;
using H.NotifyIcon.Core;


using var iconStream = H.Resources.Red_ico.AsStream();
using var icon = new Icon(iconStream);
using var trayIcon = new TrayIconWithContextMenu
{
    Icon = icon.Handle,
    ToolTip = "ToolTip",
};


void OnLeftClick(object? sender, MessageWindow.MouseEventReceivedEventArgs args)
{
    [DllImport("User32.dll", CharSet = CharSet.Unicode)]
    // https://learn.microsoft.com/de-de/windows/win32/api/winuser/nf-winuser-messagebox
    static extern int MessageBox(IntPtr h, string m, string c, int type);
    var MB_OK = 0x00000000;
    var MB_ICONERROR = 0x00000010;

    if (args.MouseEvent != 0 && args.MouseEvent == MouseEvent.IconLeftMouseDown)
    {
        _ = MessageBox(0, "Your Message", "Error", MB_OK | MB_ICONERROR);
    }
}

trayIcon.MessageWindow.MouseEventReceived += OnLeftClick;

trayIcon.ContextMenu = new PopupMenu
{
    Items =
    {
        new PopupMenuItem("Show Message", (_, _) => ShowMessage(trayIcon, "message")),
        new PopupMenuItem("Show Info", (_, _) => ShowInfo(trayIcon, "info")),
        new PopupMenuItem("Show Warning", (_, _) => ShowWarning(trayIcon, "warning")),
        new PopupMenuItem("Show Error", (_, _) => ShowError(trayIcon, "error")),
        new PopupMenuItem("Show Custom", (_, _) => ShowCustom(trayIcon, "custom", icon)),
        new PopupMenuSeparator(),
        new PopupMenuItem("Exit", (_, _) =>
        {
            trayIcon.Dispose();
            Environment.Exit(0);
        }),
    },
};

trayIcon.Create();

while (true) {}

static void ShowMessage(TrayIcon trayIcon, string message)
{
    trayIcon.ShowNotification(
        title: nameof(NotificationIcon.None),
        message: message,
        icon: NotificationIcon.None);
    Console.WriteLine(nameof(trayIcon.ShowNotification));
}

static void ShowInfo(TrayIcon trayIcon, string message)
{
    trayIcon.ShowNotification(
        title: nameof(NotificationIcon.Info),
        message: message,
        icon: NotificationIcon.Info);
    Console.WriteLine(nameof(trayIcon.ShowNotification));
}

static void ShowWarning(TrayIcon trayIcon, string message)
{
    trayIcon.ShowNotification(
        title: nameof(NotificationIcon.Warning),
        message: message,
        icon: NotificationIcon.Warning);
    Console.WriteLine(nameof(trayIcon.ShowNotification));
}

static void ShowError(TrayIcon trayIcon, string message)
{
    trayIcon.ShowNotification(
        title: nameof(NotificationIcon.Error),
        message: message,
        icon: NotificationIcon.Error);
    Console.WriteLine(nameof(trayIcon.ShowNotification));
}

static void ShowCustom(TrayIcon trayIcon, string message, Icon icon)
{
    trayIcon.ShowNotification(
        title: "Custom",
        message: message,
        customIconHandle: icon.Handle);
    Console.WriteLine(nameof(trayIcon.ShowNotification));
}
