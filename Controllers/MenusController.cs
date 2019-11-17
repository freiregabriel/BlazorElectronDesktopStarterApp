using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorElectronDesktopStarterApp.Controllers
{
    public class MenusController
    {
        public static void SetMenu()
        {
            if (HybridSupport.IsElectronActive)
            {
                var menu = new MenuItem[]
                {
                    new MenuItem { Label = "File", Submenu = new MenuItem[]
                        {
                            new MenuItem { Label = "Quit", Accelerator = "CmdOrCtrl+Q", Role = MenuRole.close }
                        }
                    },
                    new MenuItem { Label = "View", Submenu = new MenuItem[]
                        {
                            new MenuItem { Label = "Reload", Accelerator = "CmdOrCtrl+R", Click = () =>
                                {
                                    var mainWindowId = Electron.WindowManager.BrowserWindows.ToList().First().Id;
                                    Electron.WindowManager.BrowserWindows.ToList().ForEach(browserWindow =>
                                    {
                                        if (browserWindow.Id != mainWindowId)
                                        {
                                            browserWindow.Close();
                                        }
                                        else
                                        {
                                            browserWindow.Reload();
                                        }
                                    });
                                } 
                            },
                            new MenuItem { Type = MenuType.separator },
                            new MenuItem { Label = "Open Developer Tools", Accelerator = "CmdOrCtrl+I", Click = () => Electron.WindowManager.BrowserWindows.First().WebContents.OpenDevTools() }
                        }
                    }
                };

                Electron.Menu.SetApplicationMenu(menu);
            }
        }
    }
}
