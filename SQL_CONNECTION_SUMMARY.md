# SQL Server 連線設定總結

## ? 已完成的工作

### 1. 更新 App.config
- ? 添加 `<connectionStrings>` 區段
- ? 提供多種連線字串範例（已註解）
- ? 支援 Windows 驗證和 SQL Server 驗證

### 2. 更新 DatabaseHelper.cs
- ? 自動從 App.config 讀取連線字串
- ? 如果讀取失敗，自動使用 LocalDB 作為備用
- ? 新增 `TestConnection()` 方法測試連線
- ? 新增 `DatabaseExists()` 方法檢查資料庫是否存在
- ? 新增 `GetConnectionStringInfo()` 方法顯示連線資訊（隱藏密碼）

### 3. 更新 Form1.cs
- ? 程式啟動時自動檢查資料庫連線
- ? 如果連線失敗，提示使用者測試連線
- ? 支援 Enter 鍵快速登入

### 4. 建立說明文件
- ? `SQLSERVER_CONNECTION_GUIDE.md` - 完整的連線設定指南
- ? `QUICK_SETUP.md` - 5 分鐘快速設定指南
- ? `DatabaseScript.sql` - 資料庫初始化腳本

---

## ?? 目前的連線設定選項

您的 `App.config` 現在包含以下連線選項（請根據需要取消註解）：

### 選項 1: SQL Server 驗證（預設啟用）
```xml
<add name="OnlineOrderingDB" 
     connectionString="Data Source=YOUR_SERVER_NAME;Initial Catalog=OnlineOrderingDB;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;Integrated Security=False" 
     providerName="System.Data.SqlClient" />
```

### 選項 2: Windows 驗證（建議）
```xml
<add name="OnlineOrderingDB" 
     connectionString="Data Source=YOUR_SERVER_NAME;Initial Catalog=OnlineOrderingDB;Integrated Security=True" 
     providerName="System.Data.SqlClient" />
```

### 選項 3: SQL Server Express
```xml
<add name="OnlineOrderingDB" 
     connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=OnlineOrderingDB;Integrated Security=True" 
     providerName="System.Data.SqlClient" />
```

### 選項 4: 遠端 SQL Server
```xml
<add name="OnlineOrderingDB" 
     connectionString="Data Source=192.168.1.100,1433;Initial Catalog=OnlineOrderingDB;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;Integrated Security=False" 
     providerName="System.Data.SqlClient" />
```

---

## ?? 如何使用

### 步驟 1: 選擇連線方式
在 `App.config` 中，找到 `<connectionStrings>` 區段，選擇適合的連線字串。

### 步驟 2: 修改連線參數
根據您的環境修改以下參數：
- `YOUR_SERVER_NAME` → 您的 SQL Server 名稱（例如：`localhost`, `.\SQLEXPRESS`, `192.168.1.100`）
- `YOUR_USERNAME` → SQL Server 登入帳號
- `YOUR_PASSWORD` → SQL Server 登入密碼

### 步驟 3: 建立資料庫
在 SQL Server Management Studio 中執行：
```sql
CREATE DATABASE OnlineOrderingDB;
```

### 步驟 4: 測試連線
啟動應用程式，系統會自動測試連線並提供錯誤訊息（如果有）。

---

## ?? 快速設定範例

### 本機開發（最簡單）
如果您在本機安裝了 SQL Server Express：

1. 修改 App.config：
```xml
<add name="OnlineOrderingDB" 
     connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=OnlineOrderingDB;Integrated Security=True" 
     providerName="System.Data.SqlClient" />
```

2. 在 SSMS 中建立資料庫：
```sql
CREATE DATABASE OnlineOrderingDB;
```

3. 執行應用程式 ?

---

## ?? 連線測試功能

### 自動測試
程式啟動時會自動檢查資料庫連線：
- ? 連線成功 → 自動初始化資料表
- ? 連線失敗 → 顯示錯誤訊息並詢問是否測試連線

### 手動測試
在 Form1 中，點擊連線測試按鈕（如果有）會顯示：
- 伺服器名稱
- 資料庫名稱
- 連線狀態

### 錯誤訊息
連線失敗時會顯示：
- 詳細錯誤訊息
- 目前使用的連線字串（密碼已隱藏）

---

## ?? ADO.NET 架構

```
App.config
    ↓ (讀取連線字串)
DatabaseHelper.cs
    ↓ (提供資料庫操作)
    ├── GetConnection() → SqlConnection
    ├── ValidateUser() → SqlCommand
    ├── RegisterUser() → SqlCommand (參數化)
    ├── GetProducts() → SqlDataAdapter
    ├── GetUserOrders() → SqlDataAdapter + JOIN
    └── TestConnection() → 測試連線
```

---

## ?? 專案檔案結構

```
Online Ordering System/
├── App.config                        ? 連線字串設定
├── DatabaseHelper.cs                 ? ADO.NET 核心類別
├── Form1.cs                          ? 登入表單（使用 ADO.NET）
├── Form2.cs                          - 主系統表單
├── RegisterForm.cs                   ? 註冊表單（使用 ADO.NET）
├── DatabaseScript.sql                - 資料庫初始化腳本
├── SQLSERVER_CONNECTION_GUIDE.md     ? 完整連線設定指南
├── QUICK_SETUP.md                    ? 快速設定指南
└── README_ADONET.md                  - ADO.NET 實作說明
```

? = SQL Server 連線相關檔案

---

## ?? 安全性提醒

### ?? 重要注意事項

1. **不要將密碼提交到 Git**
   ```xml
   <!-- 錯誤示範 -->
   Password=MyRealPassword123
   
   <!-- 正確做法：使用環境變數或加密 -->
   Password=%DB_PASSWORD%
   ```

2. **使用 Windows 驗證（如果可能）**
   - 更安全
   - 不需要在設定檔中儲存密碼
   - 使用 `Integrated Security=True`

3. **限制資料庫權限**
   - 不要使用 `sa` 帳號
   - 建立專用的應用程式帳號
   - 只授予必要的權限（SELECT, INSERT, UPDATE, DELETE）

---

## ?? 參考文件

| 文件 | 用途 |
|------|------|
| `QUICK_SETUP.md` | 5 分鐘快速設定 |
| `SQLSERVER_CONNECTION_GUIDE.md` | 完整的連線設定指南 |
| `README_ADONET.md` | ADO.NET 實作說明 |
| `DatabaseScript.sql` | 資料庫初始化腳本 |

---

## ?? 下一步

### 選項 A: 使用 LocalDB（無需設定）
1. 不修改 App.config
2. 直接執行程式
3. 系統會自動使用 LocalDB

### 選項 B: 使用本機 SQL Server Express
1. 參考 `QUICK_SETUP.md`
2. 修改 App.config
3. 建立資料庫
4. 執行程式

### 選項 C: 使用遠端 SQL Server
1. 參考 `SQLSERVER_CONNECTION_GUIDE.md`
2. 設定 SQL Server 遠端連線
3. 修改 App.config
4. 測試連線
5. 執行程式

---

## ? 功能特色

- ? 自動從 App.config 讀取連線字串
- ? 支援多種資料庫連線方式
- ? 自動檢測資料庫連線狀態
- ? 友善的錯誤訊息提示
- ? 連線測試功能
- ? 自動初始化資料表
- ? 參數化查詢防止 SQL Injection
- ? 使用 using 語句自動釋放資源

---

## ?? 需要協助？

### 連線問題
?? 查看 `SQLSERVER_CONNECTION_GUIDE.md` 的「常見問題」章節

### 快速設定
?? 查看 `QUICK_SETUP.md`

### ADO.NET 使用
?? 查看 `README_ADONET.md`

---

**版本**: 1.0  
**最後更新**: 2025  
**適用於**: .NET Framework 4.7.2 + SQL Server 2012+
