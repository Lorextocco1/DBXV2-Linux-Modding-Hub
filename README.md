# Dragon Ball Xenoverse 2 - Linux Modding Hub & Auto-Patcher 🐉

Un Launcher nativo C# progettato per **Steam Deck, ROG Ally e Linux** che semplifica drasticamente il modding di Xenoverse 2.
Risolve il problema delle DLL Override e offre un'interfaccia grafica per gestire installer e tool esterni.

## ⚡ Caratteristiche Principali

* **🛡️ Auto-Patcher Integrato:** Dimentica `WINEDLLOVERRIDES="xinput1_3=n,b"`. Il launcher rileva automaticamente se hai installato XV2Patcher (`xinput1_3.dll`) e inietta l'override necessario solo quando serve. Zero configurazione su Steam.
* **📂 Modding Hub:** Pulsanti rapidi per avviare il gioco, l'installer delle mod (`xv2ins.exe`) o qualsiasi tool esterno (`.exe`) tramite un file browser integrato, senza chiudere il launcher.
* **🎨 Design Ufficiale:** Interfaccia ispirata ai menu di gioco con effetti sonori visivi (hover gold) e supporto per wallpaper personalizzati.
* **🐧 Native Performance:** Essendo compilato nativamente dentro Proton, è leggerissimo e non crasha come i vecchi script `.bat`.

## 🚀 Installazione Facile

1.  Scarica i file di questo progetto (`LauncherCode.cs`, `CostruisciApp.bat`, `logo.png`, `wallpaper.jpg`).
2.  Copiali nella **cartella principale** di Dragon Ball Xenoverse 2 (dove si trova `bin/DBXV2.exe`).
3.  **Primo Avvio (Compilazione):**
    * Su Steam, aggiungi `CostruisciApp.bat` come gioco non di Steam.
    * Imposta compatibilità: **Proton Experimental**.
    * Avvialo una volta.
4.  Verrà creato un nuovo file **`DBXV2Launcher.exe`**.
5.  **Configurazione Finale:**
    * Su Steam, cambia il collegamento per puntare a `DBXV2Launcher.exe`.
    * **IMPORTANTE:** Nel campo "Inizia in" (Start In), assicurati che sia selezionata la cartella principale del gioco, altrimenti le immagini non verranno caricate.
    * Cancella eventuali opzioni di avvio vecchie.

## 🛠️ Requisiti
* Una copia di Dragon Ball Xenoverse 2.
* Linux / SteamOS (Steam Deck) / BazziteOS (ROG Ally).
* Per le mod: XV2Patcher e XV2 Installer (da scaricare separatamente).

Buon divertimento Super Saiyan! 🔥
