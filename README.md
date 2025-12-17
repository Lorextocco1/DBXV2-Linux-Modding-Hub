# Dragon Ball Xenoverse 2 - Linux Modding Hub v2.5.0 🐧🔥

> "L'hub definitivo per gestire, installare e scaricare mod di Dragon Ball Xenoverse 2 su Linux e Steam Deck. Ora in Widescreen HD!"

## 🌟 Novità v2.5.0: HD & Smart Browser
* **Widescreen 16:9:** Il Launcher ora gira a 1280x720 nativi, perfetto per eliminare le bande nere su Steam Deck e monitor moderni.
* **Smart Browser:** Supporto per browser portatili (Mypal) per scaricare mod senza crash direttamente dal tool.

---

## 🚀 Installazione (Passo-Passo)

### 1. Preparazione File
1.  Scarica i file di questo repository.
2.  Copiali nella **cartella principale** di Dragon Ball Xenoverse 2 (dove c'è `bin/DBXV2.exe`).

### 2. Setup del Browser (FACOLTATIVO)
*Questa procedura serve **solo** se vuoi usare il tasto "SITO XENOVERSE MODS" integrato nel Launcher per scaricare mod direttamente nella cartella giusta. Se non lo fai, tutte le altre funzioni (Gioca, Installa Mod, Patcher) funzioneranno comunque perfettamente!*

1.  Vai a questo indirizzo ufficiale:
    👉 **[Download Mypal 68 (GitHub)](https://github.com/Feodor2/Mypal68/releases/tag/74.1.4)**
2.  Scarica il file `.zip` per sistemi a 64 bit.
    * *Nome file da cercare:* `mypal-68.xx.x-US.win64.zip`
3.  Crea una cartella chiamata `Browser` nella directory principale del gioco.
4.  Estrai tutto il contenuto dello zip lì dentro.
    * ⚠️ **NOTA:** Non rinominare nulla! Il Launcher troverà automaticamente il file `mypal.exe`.

### 3. Creazione Launcher su Steam
1.  Su Steam (in modalità Desktop), aggiungi il file `CostruisciApp.bat` come "Gioco non di Steam".
2.  Imposta compatibilità: **Proton Experimental**.
3.  Avvialo una volta per generare `DBXV2Launcher.exe`.
4.  Ora punta il collegamento di Steam direttamente al nuovo `DBXV2Launcher.exe`.
    * **IMPORTANTE:** Nelle proprietà di Steam, assicurati che il campo **"Inizia in" (Start In)** sia impostato sulla cartella del gioco.

---

## ⚙️ Configurazione Anti-Crash (Solo se usi il Browser)

Se hai deciso di installare il browser facoltativo, segui questi passaggi **una sola volta** per evitare crash grafici su Steam Deck/Linux:

1.  Apri il browser dal Launcher (Tasto "SITO").
2.  Clicca sull'icona **HOME** (🏠) in alto a sinistra (i menu a tendina in alto a destra potrebbero non funzionare su Linux).
3.  Clicca sull'icona **IMPOSTAZIONI** (⚙️ o ☀️) nella pagina.
4.  Cerca **`hardware`** nella barra di ricerca.
5.  **Togli la spunta** a:
    * *"Use recommended performance settings"*
    * *"Use hardware acceleration when available"*
6.  (Consigliato) In **General -> Downloads**, imposta la cartella di salvataggio su `Dragon Ball Xenoverse 2/Mods` per avere le mod pronte all'uso!

---

## 🎮 Le Funzionalità dell'Hub

* **GIOCA:**
    Avvia il gioco iniettando automaticamente i fix necessari (`WINEDLLOVERRIDES`). Zero configurazione richiesta.

* **SITO XENOVERSE MODS:**
    Se configurato, apre il browser integrato su *VideoGameMods.com* per download diretti.

* **INSTALLA X2M:**
    Funzione rapida "One-Click". Selezioni un file `.x2m` e lo installi al volo.

* **MODS INSTALLER (UI):**
    Apre l'interfaccia completa di XV2 Installer per gestire le mod installate.

* **EXE MODS LAUNCHER:**
    Avvia tool esterni (es. configuratori di aure, save editor) senza uscire dall'Hub.

* **CARTELLA MODS:**
    Apre direttamente la cartella delle mod nel file manager.

---

## ⚠️ Crediti e Note
* Creato da: **LorexTheGamer**
* Testato su: **Steam Deck (SteamOS)** e **Linux Desktop**
* Versione: **2.5.0**
