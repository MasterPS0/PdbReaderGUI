# PDB Reader PRO
<img width="1233" height="642" alt="Unti7gtled-1" src="https://github.com/user-attachments/assets/c12f4c03-d3f5-4daa-ad50-afb23438115c" />

## 📌 Overview

**PDB Reader PRO** is a Windows desktop tool built with C# (WinForms) designed to analyze `.pdb` files and extract embedded readable data such as:

* Content IDs / Names
* PNG image links
* JSON metadata links
* PKG download links

The tool also provides a visual preview of images and allows exporting extracted data.

---

## ✨ Features

* 📂 Open and read `.pdb` files (binary supported)
* 🔍 Extract readable text from binary data
* 🌐 Detect and filter:

  * `.png`
  * `.json`
  * `.pkg`
* 🖼 Display image preview (128x128)
* 📊 Organized table view
* 💾 Export results:

  * TXT file
  * JSON file
* Clear and reload data easily
*  Clean and professional UI (light theme)

---

##  How It Works

The tool reads the `.pdb` file as raw binary data and:

1. Extracts readable ASCII strings
2. Uses Regular Expressions (Regex) to find:

   * URLs
   * File paths
   * Content IDs
3. Filters results by file extensions:

   * `.png`
   * `.json`
   * `.pkg`
4. Displays results in a structured table

---

### 2. Steps

1. Click **Browse** → Select `.pdb` file
2. Click **Extract** → Analyze file
3. View results in the table:

   * Name / Content ID
   * PNG link
   * JSON link
   * PKG link
4. Select any row to preview image
5. Export results:

   * Click **Save TXT**
   * Click **Save JSON**

---

## 📁 Output Examples

### TXT Output

```txt
====================================
Name / Content ID: IP9100-PPSA01325_00-PREINMASTER00000
PNG: https://image.api.playstation.com/...
JSON: https://sgst.prod.dl.playstation.net/...
PKG: http://gst.prod.dl.playstation.net/...
```

---

### JSON Output

```json
[
  {
    "Name": "IP9100-PPSA01325_00-PREINMASTER00000",
    "PNG": "https://image.api.playstation.com/...",
    "JSON": "https://sgst.prod.dl.playstation.net/...",
    "PKG": "http://gst.prod.dl.playstation.net/..."
  }
]
```

---

##  Requirements

* Windows OS
* .NET 6 / 7 / 8 SDK
* Internet connection (for image preview)


##  Notes

* `.pdb` files are treated as raw binary — results depend on embedded readable data
* Some links may be partial or relative paths
* Image preview requires valid online URLs


##  Author

Developed for advanced file analysis and reverse engineering purposes.

---

##  License

Free for personal and research use.
