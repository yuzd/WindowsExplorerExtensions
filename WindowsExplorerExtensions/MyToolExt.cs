using SharpShell.Attributes;
using SharpShell.SharpContextMenu;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowsExplorerExtensions.Resources;

namespace WindowsExplorerExtensions
{
    [ComVisible(true)]
    [COMServerAssociation(AssociationType.AllFiles)]
    public class MyToolExt : SharpContextMenu
    {
        private ContextMenuStrip menu = new ContextMenuStrip();

        protected override bool CanShowMenu()
        {
            if (SelectedItemPaths.Count() == 1)
            {
                this.UpdateMenu();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void UpdateMenu()
        {
            menu.Dispose();
            menu = CreateMenu();
        }

        protected override ContextMenuStrip CreateMenu()
        {
            menu.Items.Clear();
            var file = SelectedItemPaths.First();
            FileAttributes attr = File.GetAttributes(file);
            this.MenuFiles(file, attr.HasFlag(FileAttributes.Directory));
            return menu;
        }

        private void MenuFiles(string file, bool isFolder)
        {
            var icon = isFolder ? AppStrings.Folder_icon : AppStrings.file_icon;
            ToolStripMenuItem mainMenu = new ToolStripMenuItem
            {
                Text = AppStrings.MyToolExt_MenuFiles_ToolBoxName,
                Image = icon
            };

            var assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var configFile = Path.Combine(assemblyFolder, "menu.txt");
            var datas = File.ReadAllLines(configFile);
            if (datas.Length < 1)
            {
                return;
            }

            var menu_configs = new List<Config>();
            foreach (var data in datas)
            {
                var sps = data.Split(new string[] { "-_-" }, StringSplitOptions.None);
                if (sps.Length != 4)
                {
                    continue;
                }

                var cfg = new Config
                {
                    type = sps[0],
                    name = sps[1],
                    excutePath = sps[2],
                    args = sps[3],
                };

                if (!cfg.CheckArgs())
                {
                    continue;
                }

                if (cfg.type == "folder" && isFolder)
                {
                    menu_configs.Add(cfg);
                }
                else if (cfg.type == "*")
                {
                    menu_configs.Add(cfg);
                }
                else if (Path.GetExtension(file) == cfg.type)
                {
                    menu_configs.Add(cfg);
                }
            }

            foreach (var menuConfig in menu_configs)
            {
                var submenu = new ToolStripMenuItem
                {
                    Text = menuConfig.name,
                    Image = icon
                };
                submenu.Click += (sender, args) => Excute(file, menuConfig);
                mainMenu.DropDownItems.Add(submenu);
            }
            menu.Items.Clear();
            menu.Items.Add(mainMenu);
        }

        private void Excute(string path, Config cfg)
        {
            var args = cfg.args.Replace("{path}", path);
            var command = $"\"{cfg.excutePath}\" {args}";
            RunCommand(command);
        }
        private static void RunCommand(string commandToRun)
        {
            Process process = null;
            try
            {
                var processStartInfo = new ProcessStartInfo()
                {
                    FileName = "cmd",
                    RedirectStandardOutput = false,
                    RedirectStandardInput = true,
                    RedirectStandardError = false,
                    CreateNoWindow = false,
                    UseShellExecute = false,
                    Verb = "runas",
                    WorkingDirectory = Directory.GetDirectoryRoot(Directory.GetCurrentDirectory())
                };
                process = Process.Start(processStartInfo);
                //录入命令
                process.StandardInput.WriteLine($"{commandToRun}");
                process.WaitForExit();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                try
                {
                    process?.Dispose();
                }
                catch (Exception)
                {
                    //ignore
                }
            }
        }
    }

    class Config
    {
        public string type { get; set; }
        public string name { get; set; }
        public string excutePath { get; set; }
        public string args { get; set; }

        public bool CheckArgs()
        {
            if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(excutePath) ||
                string.IsNullOrEmpty(args))
            {
                return false;
            }

            return true;
        }
    }
}

