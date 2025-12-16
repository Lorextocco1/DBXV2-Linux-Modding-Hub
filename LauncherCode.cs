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
        private Button btnMods;
        private Button btnExeMods;
        private Button btnExit;

        // Colori stile XV2
        Color xv2OrangeBorder = ColorTranslator.FromHtml("#FF6A00"); 
        Color xv2OrangeText = ColorTranslator.FromHtml("#FF8C00");
        Color xv2GoldHover = ColorTranslator.FromHtml("#FFD700");
        Color xv2DarkGlass = Color.FromArgb(180, 10, 10, 25); 

        // Margine sinistro per l'allineamento (Stile Nuovo Menu)
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
            this.BackColor = Color.FromArgb(10, 10, 20); // Colore di fallback

            // Icona del gioco
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
            int spacing = 65;

            // TASTO 1: GIOCA
            btnPlay = CreateXV2Button("[ 1 ]  GIOCA", startY);
            btnPlay.Click += (sender, e) => { 
                LanciaApp("bin", "DBXV2.exe"); 
                this.Close(); 
            };

            // TASTO 2: MODS INSTALLER
            btnMods = CreateXV2Button("[ 2 ]  MODS INSTALLER", startY + spacing);
            btnMods.Click += (sender, e) => { 
                LanciaApp("XV2INS", "xv2ins.exe"); 
            };

            // TASTO 3: EXE MODS LAUNCHER
            btnExeMods = CreateXV2Button("[ 3 ]  EXE MODS LAUNCHER", startY + spacing * 2);
            btnExeMods.Click += (sender, e) => { 
                ScegliEdEseguiExe();
            };

            // TASTO 4: ESCI
            btnExit = CreateXV2Button("[ 4 ]  ESCI", startY + spacing * 3);
            btnExit.Click += (sender, e) => { this.Close(); };

            // --- 5. FOOTER (Posizionato Perfettamente) ---
            Label footer = new Label();
            footer.Text = "XV2 MODDING HUB | PROTON AUTO-PATCHER";
            footer.Font = new Font("Segoe UI", 8, FontStyle.Regular);
            footer.ForeColor = Color.FromArgb(150, 255, 255, 255); // Bianco semi-trasparente
            footer.AutoSize = true;
            footer.BackColor = Color.Transparent;
            
            // Calcolo preciso: Larghezza Finestra - Larghezza Testo - 20px di margine destro
            // Altezza Finestra - 25px margine inferiore
            footer.Location = new Point(this.ClientSize.Width - footer.PreferredWidth - 20, this.ClientSize.Height - 25);
            
            this.Controls.Add(footer);
        }

        private Button CreateXV2Button(string text, int top)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Size = new Size(350, 55);
            btn.Location = new Point(leftMargin, top);
            btn.FlatStyle = FlatStyle.Flat;
            btn.Font = new Font("Segoe UI Black", 14, FontStyle.Bold);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(20, 0, 0, 0);
            btn.Cursor = Cursors.Hand;
            btn.BackColor = xv2DarkGlass;
            btn.ForeColor = xv2OrangeText;
            btn.FlatAppearance.BorderColor = xv2OrangeBorder;
            btn.FlatAppearance.BorderSize = 2;
            
            // Effetto Hover (Super Saiyan)
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

                    // --- AUTO-PATCHER LOGIC ---
                    // Se avviamo il gioco e c'è xinput1_3.dll, iniettiamo l'override.
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
