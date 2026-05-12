# PDB Reader PRO
<img width="767" height="493" alt="image" src="https://github.com/user-attachments/assets/226976b7-6cce-4ebb-bc6f-b85b17acd439" />


## 📌 Overview

PDB Reader PRO is a professional Windows desktop tool built with C# WinForms for analyzing `.pdb` files and extracting embedded readable data such as:

* PNG image links
* JSON metadata links
* PKG download links
* Content IDs / Game IDs
* Readable text strings

The application supports automatic extraction, image preview, context menu integration, and file association for `.pdb` files.

---

# ✨ Features

## ✅ File Features

* Open `.pdb` files directly
* Double-click `.pdb` support
* Right-click context menu integration
* Automatic extraction after opening file
* Binary string extraction

---

## ✅ Extracted Data

The tool automatically detects and extracts:

* `.png`
* `.json`
* `.pkg`
* Content IDs
* URLs
* Readable ASCII text

---

## ✅ UI Features

* Professional light UI
* Embedded application icon
* Image preview panel
* Search box
* DataGrid table viewer
* Details viewer
* Fixed professional layout
* No maximize button

---

## ✅ Export Features

Export extracted data to:

* TXT
* JSON

---

# 📂 Included Files

| File                      | Description                             |
| ------------------------- | --------------------------------------- |
| `PdbReaderGUI_PRO.exe`    | Main application                        |
| `register_pdb.reg`        | Register `.pdb` extension with the tool |
| `remove_register_pdb.reg` | Remove `.pdb` file association          |
| `add_context_menu.reg`    | Add right-click context menu            |
| `remove_context_menu.reg` | Remove right-click context menu         |

---

# 🖼 Preview

## Main Interface

* Left panel:

  * Image preview
  * File details

* Right panel:

  * Extracted table data

* Top toolbar:

  * Browse
  * Save TXT
  * Save JSON
  * Clear

---

# 🚀 How It Works

The application:

1. Reads `.pdb` file as raw binary
2. Extracts readable strings
3. Uses Regex pattern matching
4. Detects:

   * PNG URLs
   * JSON URLs
   * PKG URLs
   * Content IDs
5. Displays results automatically

---

# 📂 Supported Extensions

| Type | Supported |
| ---- | --------- |
| PDB  | ✅         |
| PNG  | ✅         |
| JSON | ✅         |
| PKG  | ✅         |

---

# 🔍 Search System

The built-in search system allows searching inside:

* URLs
* Content IDs
* JSON links
* PKG links

Press `Enter` after typing.

---

# 💾 Export Example

## TXT Export

```txt
====================================
Name / Content ID: JP0741-CUSA25180_00-JINKIT...
PNG: https://image.api.playstation.com/...
JSON: http://gs2.ww.prod.dl.playstation.net/...
PKG: /JP0741-CUSA25180_00-JINKIT.pkg
```

---

## JSON Export

```json
[
  {
    "Name": "JP0741-CUSA25180_00-JINKIT",
    "PNG": "https://image.api.playstation.com/...",
    "JSON": "http://gs2.ww.prod.dl.playstation.net/...",
    "PKG": "/JP0741-CUSA25180_00-JINKIT.pkg"
  }
]
```

---

# ⚙️ Requirements

* Windows 10 / 11
* .NET 8 Runtime
* Internet connection for image preview

---

# 📦 Build

## Run

```bash
dotnet run
```

## Publish EXE

```bash
dotnet publish -c Release -r win-x64 --self-contained true
```

---

# 🔗 Windows Integration

## Register `.pdb` Files

Run:

```txt
register_pdb.reg
```

This enables:

* Double-click `.pdb` support
* Open directly with PDB Reader PRO

---

## Remove `.pdb` Association

Run:

```txt
remove_register_pdb.reg
```

---

## Add Right-Click Context Menu

Run:

```txt
add_context_menu.reg
```

Adds:

```txt
Open with PDB Reader PRO
```

to the Windows context menu.

---

## Remove Context Menu

Run:

```txt
remove_context_menu.reg
```

---

# 🛠 Technologies Used

* C#
* .NET 8 WinForms
* Regex
* DataGridView
* System.Text.Json
* WebClient
* Windows Registry

---

# 📌 Future Updates

Planned features:

* Drag & Drop support
* Dark Mode
* Multi-file extraction
* Excel export
* Faster async image loading
* Modern UI redesign
* Offline image cache

---

# 👨‍💻 Author

PDB Reader PRO was created for advanced file analysis and reverse engineering workflows.

---

# 📄 License

Free for educational and research purposes.

