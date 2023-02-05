using SharpShell.Attributes;
using SharpShell.SharpContextMenu;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsExplorerExtensions
{
    [ComVisible(true)]
    [COMServerAssociation(AssociationType.AllFilesAndFolders)]
    public class CopyFolderLocationExt : SharpContextMenu
    {
       
        protected override bool CanShowMenu()
        {
            return true;
        }

        protected override ContextMenuStrip CreateMenu()
        {
            try
            {
                var contextMenu = new ContextMenuStrip();
                var menuItem = new ToolStripMenuItem("复制文件夹路径");
                menuItem.Image = Resources.AppStrings.Duplicate3;
                menuItem.Click += (sender, args) =>
                {
                    var file = SelectedItemPaths.First();
                    if (File.Exists(file))
                    {
                        Clipboard.SetText(new FileInfo(file)?.DirectoryName??"");
                    }
                    else
                    {
                        Clipboard.SetText(file);
                    }
                };
                contextMenu.Items.Add(menuItem);

                //  Return the menu.
                return contextMenu;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
        }

    }
}

