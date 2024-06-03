using System;
using System.Drawing;
using System.Windows.Forms;

#nullable enable

static class _
{
    [STAThread]
    static void Main(string[] args)
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new TrayIconApp());
    }
}

class TrayIconApp: ApplicationContext
{
    private NotifyIcon notifyIcon;

    public TrayIconApp()
    {
        ContextMenuStrip menu = new ContextMenuStrip();
        notifyIcon = new NotifyIcon()
        {
            Text = "TrayIcon",
            Visible = true,
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath)!,
            ContextMenuStrip = menu
        };

        menu.Items.Add("Item 1", null, (_, _) => {});
        menu.Items.Add("Item 2", null, (_, _) => {});
        menu.Items.Add(new ToolStripSeparator());
        menu.Items.Add("Quit", null, (_, _) => {notifyIcon.Visible = false; Application.Exit();});

        notifyIcon.Click += leftClickEventHandler;
        ThreadExit += (_, _) => notifyIcon.Visible = false;
    }

    private void leftClickEventHandler(object? sender, EventArgs e)
    {
        MouseEventArgs mouseEventArgs = (MouseEventArgs)e;

        if (mouseEventArgs.Button == MouseButtons.Left)
        {
            MessageBox.Show(new Form() { TopMost = true }, "Message", "Title", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
