# Dragon Ball Xenoverse 2 - Linux Modding Hub 🐧🔥

> "Lo aspettavate, lo avete cercato per lungo tempo voi di Linux (anzi, **noi** di Linux) e finalmente è qui: l'hub definitivo per giocare a Dragon Ball Xenoverse 2 con le mod su Linux!"

Un Launcher nativo dal design semplice ed intuitivo. In pochi click avrete tutto sotto controllo.

## 🌟 Perché questo Hub?

Non dovete fare altro che:
1. Mettere l'installer delle Mod (`xv2ins.exe`) nella cartella del gioco.
2. Inserire questo progetto nella directory base di Dragon Ball Xenoverse 2.
3. Creare il file `.exe` tramite `CostruisciApp.bat`.
4. Aggiungerlo a Steam come gioco non di steam... **e siete pronti!**

### 🎮 Le Funzionalità

* **GIOCA & AUTO-PATCHER:**
    Quando clicchi *Gioca*, non solo il gioco si apre, ma il tool **inserisce automaticamente quella maledetta stringa chilometrica** (`WINEDLLOVERRIDES`) nelle opzioni di avvio! Non dovete configurare niente.

* **INSTALLA X2M:**
    Funzione rapida per installare una mod `.x2m` al volo senza dover aprire l'interfaccia completa dell'installer. Selezioni il file e via!

* **MODS INSTALLER (UI):**
    Apre l'interfaccia completa di XV2 Installer per gestire, rimuovere o ordinare le mod.

* **EXE MODS LAUNCHER:**
    Configurate le aure e altri pack che hanno bisogno di eseguibili esterni, tutto senza chiudere il launcher.

---

## 🚀 Istruzioni di Installazione (Passo-Passo)

1.  Scarica i file di questo progetto.
2.  Copiali nella **cartella principale** di Dragon Ball Xenoverse 2 (dove c'è `bin/DBXV2.exe`).
3.  **Primo Avvio:**
    * Su Steam, aggiungi `CostruisciApp.bat` come gioco non di Steam.
    * Imposta compatibilità: **Proton Experimental**.
    * Avvialo una volta per generare `DBXV2Launcher.exe`.
4.  **Configurazione Finale:**
    * Punta il collegamento di Steam a `DBXV2Launcher.exe`.
    * **IMPORTANTE:** Imposta il campo "Inizia in" (Start In) sulla cartella principale del gioco.

### ⚠️ Nota sulla Stringa Manuale
Se l'automazione non dovesse funzionare sul vostro sistema specifico, ecco la stringa da copiare nelle Opzioni di Avvio di Steam:
`WINEDLLOVERRIDES="xinput1_3=n,b" %command%`

Buon divertimento con il vostro Xenoverse 2 customizzato su Linux! 🔥
