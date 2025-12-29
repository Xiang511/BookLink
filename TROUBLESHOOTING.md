# SQL Server 連線問題診斷指南

## ?? 您遇到的錯誤
```
System.Data.SqlClient.SqlException
```

這個錯誤通常表示無法連線到資料庫。讓我們一步步排查問題。

---

## ? 診斷步驟

### 步驟 1: 檢查 SQL Server 是否已安裝

#### 方法 1: 使用 PowerShell
在 PowerShell 中執行：
```powershell
Get-Service | Where-Object {$_.Name -like "*SQL*"}
```

**預期結果：**
```
Status   Name               DisplayName
------   ----               -----------
Running  MSSQL$SQLEXPRESS   SQL Server (SQLEXPRESS)
Running  SQLBrowser         SQL Server Browser
```

如果看到 `Stopped`，請執行：
```powershell
Start-Service MSSQL$SQLEXPRESS
```

#### 方法 2: 使用服務管理員
1. 按 `Win + R`
2. 輸入 `services.msc`
3. 尋找 `SQL Server (SQLEXPRESS)`
4. 確認狀態為「執行中」

---

### 步驟 2: 確認 SQL Server Express 已安裝

如果沒有安裝，請下載：
- [SQL Server Express 2019](https://www.microsoft.com/zh-tw/download/details.aspx?id=101064)
- [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/zh-tw/sql/ssms/download-sql-server-management-studio-ssms)

---

### 步驟 3: 測試資料庫連線

#### 使用 SSMS 測試
1. 開啟 SQL Server Management Studio
2. 連線設定：
   - **伺服器類型**: Database Engine
   - **伺服器名稱**: `.\SQLEXPRESS` 或 `(local)\SQLEXPRESS`
   - **驗證**: Windows 驗證
3. 點擊「連線」

**如果連線成功** ?  
→ 您的 SQL Server 運作正常，請繼續步驟 4

**如果連線失敗** ?  
→ 請檢查 SQL Server 是否正在執行

---

### 步驟 4: 建立資料庫

在 SSMS 中執行以下 SQL：
```sql
-- 建立資料庫
CREATE DATABASE OnlineOrderingDB;
GO

-- 確認資料庫已建立
USE OnlineOrderingDB;
GO

-- 顯示訊息
SELECT 'Database created successfully!' AS Message;
```

---

### 步驟 5: 更新 App.config

確認您的 `App.config` 設定正確：

```xml
<connectionStrings>
    <add name="OnlineOrderingDB" 
         connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=OnlineOrderingDB;Integrated Security=True;Connect Timeout=30" 
         providerName="System.Data.SqlClient" />
</connectionStrings>
```

---

### 步驟 6: 執行應用程式

重新執行應用程式，現在應該會看到更詳細的錯誤訊息。

---

## ?? 常見錯誤代碼

| 錯誤代碼 | 說明 | 解決方法 |
|---------|------|---------|
| **53** | 無法連線到 SQL Server | ? 確認 SQL Server 服務已啟動<br>? 檢查伺服器名稱是否正確 |
| **26** | 找不到指定的伺服器/執行個體 | ? 確認執行個體名稱 `SQLEXPRESS` 是否正確<br>? 嘗試使用 `(local)\SQLEXPRESS` |
| **4060** | 無法開啟資料庫 | ? 在 SSMS 中執行 `CREATE DATABASE OnlineOrderingDB;` |
| **18456** | 登入失敗 | ? 檢查帳號密碼<br>? 使用 Windows 驗證 |

---

## ??? 快速修復方案

### 方案 A: 使用 SQL Server Express（推薦）

1. **確認 SQL Server Express 已安裝並啟動**
2. **App.config 設定：**
   ```xml
   <add name="OnlineOrderingDB" 
        connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=OnlineOrderingDB;Integrated Security=True" 
        providerName="System.Data.SqlClient" />
   ```
3. **在 SSMS 中建立資料庫：**
   ```sql
   CREATE DATABASE OnlineOrderingDB;
   ```
4. **執行應用程式**

---

### 方案 B: 使用 LocalDB（最簡單，無需額外設定）

如果 SQL Server Express 有問題，可以改回 LocalDB：

1. **註解掉 App.config 中的連線字串**（讓程式使用預設值）
   ```xml
   <!-- <add name="OnlineOrderingDB" ... /> -->
   ```

2. **程式會自動使用 LocalDB**
   - 不需要建立資料庫
   - 資料庫檔案會自動建立在程式目錄

---

## ?? 檢查清單

請依序檢查：

- [ ] SQL Server Express 已安裝
- [ ] SQL Server (SQLEXPRESS) 服務正在執行
- [ ] 可以使用 SSMS 連線到 `.\SQLEXPRESS`
- [ ] OnlineOrderingDB 資料庫已建立
- [ ] App.config 連線字串正確
- [ ] 應用程式已重新編譯

---

## ?? 測試連線的 PowerShell 腳本

儲存為 `test-sql-connection.ps1`：

```powershell
# 測試 SQL Server 連線
$serverName = ".\SQLEXPRESS"
$databaseName = "OnlineOrderingDB"
$connectionString = "Data Source=$serverName;Initial Catalog=$databaseName;Integrated Security=True;Connect Timeout=5"

try {
    $connection = New-Object System.Data.SqlClient.SqlConnection
    $connection.ConnectionString = $connectionString
    $connection.Open()
    
    Write-Host "? 連線成功！" -ForegroundColor Green
    Write-Host "伺服器: $($connection.DataSource)"
    Write-Host "資料庫: $($connection.Database)"
    
    $connection.Close()
} catch {
    Write-Host "? 連線失敗！" -ForegroundColor Red
    Write-Host "錯誤訊息: $($_.Exception.Message)"
    
    # 檢查 SQL Server 服務
    Write-Host "`n檢查 SQL Server 服務狀態..."
    Get-Service | Where-Object {$_.Name -like "*SQL*"} | Format-Table Name, Status, DisplayName
}
```

執行：
```powershell
.\test-sql-connection.ps1
```

---

## ?? 仍然無法解決？

請提供以下資訊：

1. **錯誤訊息截圖**（重新執行應用程式後的完整錯誤訊息）
2. **SQL Server 服務狀態**（執行上面的 PowerShell 命令）
3. **SSMS 連線結果**（能否連線到 `.\SQLEXPRESS`）
4. **App.config 內容**

---

## ?? 相關文件

- [QUICK_SETUP.md](QUICK_SETUP.md) - 快速設定指南
- [SQLSERVER_CONNECTION_GUIDE.md](SQLSERVER_CONNECTION_GUIDE.md) - 完整連線指南
- [README_ADONET.md](README_ADONET.md) - ADO.NET 實作說明

---

**更新日期**: 2025  
**適用於**: .NET Framework 4.7.2 + SQL Server 2012+
