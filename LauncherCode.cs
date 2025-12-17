using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.IO;

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
        private Button btnPlay;
        private Button btnInstallX2M; // NUOVO TASTO
        private Button btnMods;
        private Button btnExeMods;
        private Button btnExit;

        // Colori stile XV2
        Color xv2OrangeBorder = ColorTranslator.FromHtml("#FF6A00"); 
        Color xv2OrangeText = ColorTranslator.FromHtml("#FF8C00");
        Color xv2GoldHover = ColorTranslator.FromHtml("#FFD700");
        Color xv2DarkGlass = Color.FromArgb(180, 10, 10, 25); 

        // Margine sinistro (Ora usato solo per posizionare il contenitore, il testo è centrato)
        int leftMargin = 80; 

        public MainForm()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            
            // --- 1. SETUP FINESTRA ---
            this.Text = "Dragon Ball Xenoverse 2 - Linux Modding Hub";
            this.Size = new Size(960, 600);
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
            logoBox.Location = new Point(leftMargin, 20); // Un po' più in alto per fare spazio
            logoBox.SizeMode = PictureBoxSizeMode.Zoom;
            logoBox.BackColor = Color.Transparent; 

            string logoPath = Path.Combine(baseDir, "logo.png");
            if (File.Exists(logoPath)) {
                try { logoBox.Image = Image.FromFile(logoPath); } catch { }
            }
            this.Controls.Add(logoBox);

            // --- 4. BOTTONI ---
            // Abbiamo 5 bottoni ora, gestiamo bene lo spazio
            int startY = 190; 
            int spacing = 60; // Spazio leggermente ridotto per farcene stare 5

            // TASTO 1: GIOCA
            btnPlay = CreateXV2Button("GIOCA", startY);
            btnPlay.Click += (sender, e) => { 
                LanciaApp("bin", "DBXV2.exe"); 
                this.Close(); 
            };

            // TASTO 2: INSTALLA X2M (NUOVO - Funzione Rapida)
            btnInstallX2M = CreateXV2Button("INSTALLA X2M", startY + spacing);
            btnInstallX2M.Click += (sender, e) => { 
                InstallaModX2M();
            };

            // TASTO 3: MODS INSTALLER (Apre il tool completo)
            btnMods = CreateXV2Button("MODS INSTALLER (UI)", startY + spacing * 2);
            btnMods.Click += (sender, e) => { 
                LanciaApp("XV2INS", "xv2ins.exe"); 
            };

            // TASTO 4: EXE MODS LAUNCHER
            btnExeMods = CreateXV2Button("EXE MODS LAUNCHER", startY + spacing * 3);
            btnExeMods.Click += (sender, e) => { 
                ScegliEdEseguiExe();
            };

            // TASTO 5: ESCI
            btnExit = CreateXV2Button("ESCI", startY + spacing * 4);
            btnExit.Click += (sender, e) => { this.Close(); };

            // --- 5. FIRMA ---
            Label footer = new Label();
            footer.Text = "XV2 MODDING HUB | LOREXTHEGAMER";
            footer.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            footer.ForeColor = Color.Black; 
            footer.AutoSize = true;
            footer.BackColor = Color.Transparent;
            footer.Location = new Point(this.ClientSize.Width - footer.PreferredWidth - 20, this.ClientSize.Height - 25);
            this.Controls.Add(footer);
        }

        private Button CreateXV2Button(string text, int top)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Size = new Size(350, 50); // Leggermente più sottili
            btn.Location = new Point(leftMargin, top);
            btn.FlatStyle = FlatStyle.Flat;
            btn.Font = new Font("Segoe UI Black", 14, FontStyle.Bold);
            
            // MODIFICA RICHIESTA: Testo Centrato
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.Padding = new Padding(0); // Rimosso padding laterale
            
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

        // --- NUOVA FUNZIONE: INSTALLAZIONE DIRETTA X2M ---
        private void InstallaModX2M()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Seleziona la Mod (.x2m) da installare";
            ofd.Filter = "File X2M (.x2m)|*.x2m|Tutti i file (*.*)|*.*";
            ofd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try {
                    // Percorso dell'installer xv2ins.exe
                    string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                    string installerPath = Path.Combine(baseDir, "XV2INS", "xv2ins.exe");
                    string workingDir = Path.Combine(baseDir, "XV2INS");

                    if (File.Exists(installerPath)) {
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = installerPath;
                        startInfo.WorkingDirectory = workingDir;
                        
                        // Passiamo il percorso del file X2M come argomento tra virgolette
                        startInfo.Arguments = $"\"{ofd.FileName}\""; 
                        
                        Process.Start(startInfo);
                    } else {
                        MessageBox.Show("Impossibile trovare xv2ins.exe nella cartella XV2INS!", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                } catch (Exception ex) {
                    MessageBox.Show("Errore durante l'installazione: " + ex.Message);
                }
            }
        }

        private void ScegliEdEseguiExe()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Seleziona EXE (Mod Tool, Installer, ecc)";
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
