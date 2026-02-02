# 書店管理系統 (BookLink)

![GitHub last commit](https://img.shields.io/github/last-commit/Xiang511/BookLink?display_timestamp=committer&style=flat-square) ![GitHub commit activity](https://img.shields.io/github/commit-activity/y/Xiang511/BookLink?style=flat-square) ![GitHub Created At](https://img.shields.io/github/created-at/Xiang511/BookLink?style=flat-square) ![GitHub License](https://img.shields.io/github/license/Xiang511/BookLink?style=flat-square)

一個基於 Windows Forms 開發的書店管理系統，使用 .NET Framework 4.7.2 和 SQL Server 作為後端資料庫。

## Introduction

一個功能完整的書店管理系統，提供使用者註冊、登入、商品瀏覽、購物車管理、訂單處理等功能。

## Features

- **使用者認證**
  - 使用者註冊
  - 使用者登入/登出
  - 個人資料管理

- **商品管理**
  - 商品列表瀏覽
  - 商品詳細資訊
  - 商品搜尋與篩選

- **購物車功能**
  - 加入購物車
  - 修改商品數量
  - 移除商品
  - 查看購物車總計

- **訂單管理**
  - 建立訂單
  - 查看訂單歷史
  - 訂單狀態追蹤

- **報表功能**
  - 訂單報表
  - 銷售統計

## Tech stack

### 開發環境
- **.NET Framework**: 4.7.2
- **開發工具**: Visual Studio
- **資料庫**: SQL Server (LocalDB)

### 使用的套件
- **MaterialSkin.2** (v2.3.1) - Material Design UI 框架
- **FontAwesome.Sharp** (v6.6.0) - 圖示庫
- **AntdUI** (v2.2.14) - UI 元件庫

### 專案結構
```
Online-Ordering-System/
├── UserControl/           # 使用者控制項
│   ├── HomePanel.cs       # 首頁面板
│   ├── MarketPanel.cs     # 商城面板
│   ├── CartPanel.cs       # 購物車面板
│   ├── OrderPanel.cs      # 訂單面板
│   ├── Profile.cs         # 個人資料面板
│   └── Reports.cs         # 報表面板
├── Form1.cs               # 登入表單
├── Form3.cs               # 主要應用程式表單
├── RegisterForm.cs        # 註冊表單
├── ProductDetail.cs       # 商品詳情表單
├── ProductDetail_insert.cs # 商品新增表單
├── DatabaseHelper.cs      # 資料庫操作輔助類別
├── globalVal.cs           # 全域變數與設定
└── Program.cs             # 程式進入點
```

## Installation Guide

### 前置需求
1. Windows 作業系統
2. Visual Studio 2017 或更新版本
3. .NET Framework 4.7.2 或更新版本
4. SQL Server ( Developer edition )

### 安裝步驟

1. **設定資料庫**
1. 
   - 建立名為 `OnlineOrderingSystem` 的資料庫

   ```csharp
   scsb.DataSource = @".";                         // 伺服器地址
   scsb.InitialCatalog = "OnlineOrderingSystem";   // 資料庫名稱
   scsb.IntegratedSecurity = true;                 // Windows 驗證
   ```

2. **還原 NuGet 套件**
   - 在 Visual Studio 中開啟方案
   - 右鍵點選方案 → 還原 NuGet 套件

## License
本專案採用 Apache-2.0 授權條款
