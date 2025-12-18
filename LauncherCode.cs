using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Linq; 

namespace XenoverseLauncher
{
    // Classe per i bottoni trasparenti
    public class TransparentButton : Button
    {
        public TransparentButton()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
        }
    }

    public class MainForm : Form
    {
        private PictureBox logoBox;
        // Definizione dei 7 pulsanti
        private Button btnPlay;
        private Button btnSite;
        private Button btnInstallX2M;
        private Button btnMods;
        private Button btnExeMods;
        private Button btnModsFolder; // Questo tasto ora è "intelligente" (usa 7-Zip se c'è)
        private Button btnExit;

        // Colori stile XV2
        Color xv2OrangeBorder = ColorTranslator.FromHtml("#FF6A00"); 
        Color xv2OrangeText = ColorTranslator.FromHtml("#FF8C00");
        Color xv2GoldHover = ColorTranslator.FromHtml("#FFD700");
        Color xv2DarkGlass = Color.FromArgb(180, 10, 10, 25); 

        // Margine sinistro (aumentato per il layout HD 16:9)
        int leftMargin = 100; 

        public MainForm()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            
            // --- 1. SETUP FINESTRA (16:9 HD) ---
            this.Text = "Dragon Ball Xenoverse 2 - Linux Modding Hub v2.6.0";
            this.Size = new Size(1280, 720); // Risoluzione Widescreen
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(10, 10, 20); 

            // Icona
            try {
                string gameExe = Path.Combine(baseDir, "bin", "DBXV2.exe");
                if (File.Exists(gameExe)) this.Icon = Icon.ExtractAssociatedIcon(gameExe);
            } catch {}

            // --- 2. SFONDO WALLPAPER ---
            string wallPath = "";
            if (File.Exists(Path.Combine(baseDir, "wallpaper.jpg"))) 
                wallPath = Path.Combine(baseDir, "wallpaper.jpg");
            else if (File.Exists(Path.Combine(baseDir, "wallpaper.png")))
                wallPath = Path.Combine(baseDir, "wallpaper.png");

            if (!string.IsNullOrEmpty(wallPath)) {
                this.BackgroundImage = Image.FromFile(wallPath);
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }

            // --- 3. LOGO ---
            logoBox = new PictureBox();
            logoBox.Size = new Size(450, 160);
            logoBox.Location = new Point(leftMargin, 30); 
            logoBox.SizeMode = PictureBoxSizeMode.Zoom;
            logoBox.BackColor = Color.Transparent; 

            string logoPath = Path.Combine(baseDir, "logo.png");
            if (File.Exists(logoPath)) {
                try { logoBox.Image = Image.FromFile(logoPath); } catch { }
            }
            this.Controls.Add(logoBox);

            // --- 4. BOTTONI ---
            int startY = 210; 
            int spacing = 58;

            // 1. GIOCA
            btnPlay = CreateXV2Button("GIOCA", startY);
            btnPlay.Click += (sender, e) => { 
                LanciaApp("bin", "DBXV2.exe"); 
                this.Close(); 
            };

            // 2. SITO XENOVERSE MODS (Browser Intelligente)
            btnSite = CreateXV2Button("SITO XENOVERSE MODS", startY + spacing);
            btnSite.Click += (sender, e) => { 
                ApriLink("https://videogamemods.com/xenoverse/mods/");
            };

            // 3. INSTALLA X2M
            btnInstallX2M = CreateXV2Button("INSTALLA X2M", startY + spacing * 2);
            btnInstallX2M.Click += (sender, e) => { 
                InstallaModX2M();
            };

            // 4. MODS INSTALLER (UI)
            btnMods = CreateXV2Button("MODS INSTALLER (UI)", startY + spacing * 3);
            btnMods.Click += (sender, e) => { 
                LanciaApp("XV2INS", "xv2ins.exe"); 
            };

            // 5. EXE MODS LAUNCHER
            btnExeMods = CreateXV2Button("EXE MODS LAUNCHER", startY + spacing * 4);
            btnExeMods.Click += (sender, e) => { 
                ScegliEdEseguiExe();
            };

            // 6. CARTELLA MODS (Integrazione 7-Zip)
            btnModsFolder = CreateXV2Button("CARTELLA MODS", startY + spacing * 5);
            btnModsFolder.Click += (sender, e) => { 
                ApriCartellaModsCon7Zip();
            };

            // 7. ESCI
            btnExit = CreateXV2Button("ESCI", startY + spacing * 6);
            btnExit.Click += (sender, e) => { this.Close(); };

            // --- 5. FIRMA ---
            Label footer = new Label();
            footer.Text = "XV2 MODDING HUB | LOREXTHEGAMER";
            footer.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            footer.ForeColor = Color.Black; 
            footer.AutoSize = true;
            footer.BackColor = Color.Transparent;
            footer.Location = new Point(this.ClientSize.Width - footer.PreferredWidth - 20, this.ClientSize.Height - 30);
            this.Controls.Add(footer);
        }

        // --- FUNZIONE CREAZIONE BOTTONI ---
        private Button CreateXV2Button(string text, int top)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Size = new Size(350, 50);
            btn.Location = new Point(leftMargin, top);
            btn.FlatStyle = FlatStyle.Flat;
            btn.Font = new Font("Segoe UI Black", 14, FontStyle.Bold);
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.Padding = new Padding(0);
            btn.Cursor = Cursors.Hand;
            btn.BackColor = xv2DarkGlass;
            btn.ForeColor = xv2OrangeText;
            btn.FlatAppearance.BorderColor = xv2OrangeBorder;
            btn.FlatAppearance.BorderSize = 2;
            
            btn.MouseEnter += (s, e) => { 
                btn.BackColor = xv2GoldHover;
                btn.ForeColor = Color.FromArgb(20, 20, 20);
                btn.FlatAppearance.BorderColor = Color.White; 
            };
            btn.MouseLeave += (s, e) => { 
                btn.BackColor = xv2DarkGlass;
                btn.ForeColor = xv2OrangeText;
                btn.FlatAppearance.BorderColor = xv2OrangeBorder;
            };

            this.Controls.Add(btn);
            return btn;
        }

        // --- FUNZIONE APRI CARTELLA INTELLIGENTE (7-Zip o Explorer) ---
        private void ApriCartellaModsCon7Zip()
        {
            string modsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mods");
            
            // Crea la cartella se non esiste
            if (!Directory.Exists(modsPath)) Directory.CreateDirectory(modsPath);

            // 1. Cerca 7-Zip Portable
            string sevenZipPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Apps", "7-Zip", "7zFM.exe");

            if (File.Exists(sevenZipPath))
            {
                // Se 7-Zip esiste, apri quello puntando alla cartella Mods
                try {
                    Process.Start(sevenZipPath, $"\"{modsPath}\"");
                    return; // Successo, usciamo
                } catch {}
            }

            // 2. Fallback: Se non c'è 7-Zip, usa il comando CMD per aprire la cartella normale
            try {
                Process.Start("cmd.exe", $"/c start \"\" \"{modsPath}\"");
            } catch {
                 MessageBox.Show("Impossibile aprire la cartella Mods.");
            }
        }

        // --- FUNZIONE APRI LINK INTELLIGENTE (Browser Portable o Sistema) ---
        private void ApriLink(string url)
        {
            try
            {
                // Cerca browser portatile nella cartella "Browser"
                string browserFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Browser");
                if (Directory.Exists(browserFolder))
                {
                    string[] exes = Directory.GetFiles(browserFolder, "*.exe");
                    if (exes.Length > 0)
                    {
                        Process.Start(exes[0], url);
                        return;
                    }
                }
            }
            catch {}

            // Fallback CMD
            try {
                Process.Start("cmd.exe", $"/c start \"\" \"{url}\"");
            }
            catch {
                // Fallback estremo Linux
                try {
                    Process.Start("Z:\\usr\\bin\\xdg-open", url);
                }
                catch {
                    MessageBox.Show("Nessun browser trovato.\nScarica Mypal 68 e mettilo nella cartella 'Browser'.");
                }
            }
        }

        // --- FUNZIONE INSTALLAZIONE X2M ---
        private void InstallaModX2M()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Seleziona Mod (.x2m)";
            ofd.Filter = "File X2M (.x2m)|*.x2m|Tutti i file (*.*)|*.*";
            ofd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try {
                    string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                    string installerPath = Path.Combine(baseDir, "XV2INS", "xv2ins.exe");
                    string workingDir = Path.Combine(baseDir, "XV2INS");

                    if (File.Exists(installerPath)) {
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = installerPath;
                        startInfo.WorkingDirectory = workingDir;
                        startInfo.Arguments = $"\"{ofd.FileName}\""; 
                        Process.Start(startInfo);
                    } else {
                        MessageBox.Show("Manca xv2ins.exe in XV2INS!", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                } catch (Exception ex) {
                    MessageBox.Show("Errore: " + ex.Message);
                }
            }
        }

        // --- FUNZIONE LANCIO EXE GENERICO ---
        private void ScegliEdEseguiExe()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Seleziona EXE";
            ofd.Filter = "Eseguibili (.exe)|*.exe|Tutti i file (*.*)|*.*";
            ofd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory; 

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = ofd.FileName;
                    startInfo.WorkingDirectory = Path.GetDirectoryName(ofd.FileName); 
                    Process.Start(startInfo);
                } catch (Exception ex) {
                    MessageBox.Show("Errore: " + ex.Message);
                }
            }
        }

        // --- FUNZIONE LANCIO GIOCO E APP ---
        private void LanciaApp(string folder, string exeName)
        {
            try {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string fullPath = Path.Combine(baseDir, folder, exeName);
                string workingDir = Path.Combine(baseDir, folder);

                if (File.Exists(fullPath)) {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = fullPath;
                    startInfo.WorkingDirectory = workingDir;
                    startInfo.UseShellExecute = false; 

                    // Auto-Patcher Logic per le mod
                    if (exeName.ToLower().Contains("dbxv2.exe")) 
                    {
                        string patcherDll = Path.Combine(workingDir, "xinput1_3.dll");
                        if (File.Exists(patcherDll))
                        {
                            startInfo.EnvironmentVariables["WINEDLLOVERRIDES"] = "xinput1_3=n,b";
                        }
                    }

                    Process.Start(startInfo);
                } else {
                    MessageBox.Show("File mancante: " + fullPath, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } catch (Exception ex) {
                MessageBox.Show("Errore di avvio: " + ex.Message);
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
