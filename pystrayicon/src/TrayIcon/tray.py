from os.path import realpath
from pathlib import Path
from PIL import Image
from pystray import Icon, Menu, MenuItem # type: ignore


class TrayIcon:
    def __init__(self) -> None:
        self.icon_image = Image.open(Path(realpath(__file__)).parent / "Resources" / "Red.ico")
        self.icon = Icon(
            name='Tray Icon',
            icon=self.icon_image,
            title='TrayIcon',
            menu=Menu(
                MenuItem(
                    'Item 1',
                    lambda icon, item: icon.notify('Hello World!'),
                    default=True
                ),        
                MenuItem(
                    'Item 2',
                    None
                ),
                MenuItem(
                    'Exit',
                    self.exit
                )
            )
        )

    def exit(self):
        self.icon.stop()

    def run(self):
        self.icon.run()


def run():
    TrayIcon().run()
