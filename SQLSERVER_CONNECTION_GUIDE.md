# SQL Server 連線設定指南

## ?? 目錄
1. [連線字串選項](#連線字串選項)
2. [本機 SQL Server 設定](#本機-sql-server-設定)
3. [遠端 SQL Server 設定](#遠端-sql-server-設定)
4. [連線測試](#連線測試)
5. [常見問題](#常見問題)

---

## 連線字串選項

### 選項 1: Windows 驗證 (建議使用)
```xml
<add name="OnlineOrderingDB" 
     connectionString="Data Source=YOUR_SERVER_NAME;Initial Catalog=OnlineOrderingDB;Integrated Security=True" 
     providerName="System.Data.SqlClient" />
```

**優點：**
- ? 更安全（不需要在設定檔中儲存密碼）
- ? 使用 Windows 帳號自動登入
- ? 適合內部網路環境

**適用情境：**
- 本機開發
- 公司內部網路
- 信任的網域環境

---

### 選項 2: SQL Server 驗證
```xml
<add name="OnlineOrderingDB" 
     connectionString="Data Source=YOUR_SERVER_NAME;Initial Catalog=OnlineOrderingDB;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;Integrated Security=False" 
     providerName="System.Data.SqlClient" />
```

**需要設定：**
- `YOUR_SERVER_NAME`: SQL Server 伺服器名稱
- `YOUR_USERNAME`: SQL Server 登入帳號
- `YOUR_PASSWORD`: SQL Server 登入密碼

**適用情境：**
- 遠端資料庫連線
- 無法使用 Windows 驗證的環境
- 雲端資料庫（如 Azure SQL）

---

### 選項 3: SQL Server Express (本機)
```xml
<add name="OnlineOrderingDB" 
     connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=OnlineOrderingDB;Integrated Security=True" 
     providerName="System.Data.SqlClient" />
```

**說明：**
- `.\SQLEXPRESS`: 表示本機的 SQL Server Express 執行個體
- 適合本機開發測試

---

### 選項 4: 指定 IP 和 Port（遠端連線）
```xml
<add name="OnlineOrderingDB" 
     connectionString="Data Source=192.168.1.100,1433;Initial Catalog=OnlineOrderingDB;User Id=sa;Password=yourpassword;Integrated Security=False" 
     providerName="System.Data.SqlClient" />
```

**參數說明：**
- `192.168.1.100`: SQL Server IP 位址
- `1433`: SQL Server 預設 Port（可自訂）

---

## 本機 SQL Server 設定

### 步驟 1: 確認 SQL Server 已安裝
1. 開啟 **SQL Server Management Studio (SSMS)**
2. 連線到本機 SQL Server
3. 記下伺服器名稱，例如：
   - `localhost`
   - `(local)`
   - `.\SQLEXPRESS`
   - `電腦名稱\SQLEXPRESS`

### 步驟 2: 建立資料庫
執行以下 SQL 指令建立資料庫：
```sql
CREATE DATABASE OnlineOrderingDB;
GO

USE OnlineOrderingDB;
GO
```

### 步驟 3: 執行初始化腳本
在 SSMS 中執行 `DatabaseScript.sql` 檔案以建立資料表和測試資料。

### 步驟 4: 更新 App.config
在 `App.config` 中設定連線字串：
```xml
<add name="OnlineOrderingDB" 
     connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=OnlineOrderingDB;Integrated Security=True" 
     providerName="System.Data.SqlClient" />
```

---

## 遠端 SQL Server 設定

### 步驟 1: 啟用 SQL Server 遠端連線

#### 1.1 開啟 SQL Server Configuration Manager
1. 開啟 **SQL Server Configuration Manager**
2. 展開 **SQL Server Network Configuration**
3. 點選 **Protocols for MSSQLSERVER**（或您的執行個體名稱）

#### 1.2 啟用 TCP/IP
1. 在右側找到 **TCP/IP**
2. 右鍵點選 → **Enable**（啟用）
3. 重新啟動 SQL Server 服務

#### 1.3 確認 Port 設定
1. 雙擊 **TCP/IP**
2. 切換到 **IP Addresses** 標籤
3. 找到 **IPAll** 區段
4. 確認 **TCP Port** 設定為 `1433`（或記下自訂的 Port）

### 步驟 2: 設定防火牆規則
開啟 Windows 防火牆，新增輸入規則：
```
規則名稱：SQL Server
Port：1433
通訊協定：TCP
動作：允許連線
```

### 步驟 3: 建立 SQL Server 帳號
在 SSMS 中執行：
```sql
-- 建立登入帳號
CREATE LOGIN [your_username] WITH PASSWORD = 'your_password';
GO

-- 將帳號加入到資料庫
USE OnlineOrderingDB;
GO
CREATE USER [your_username] FOR LOGIN [your_username];
GO

-- 授予權限
ALTER ROLE db_owner ADD MEMBER [your_username];
GO
```

### 步驟 4: 測試遠端連線
使用 SSMS 從其他電腦測試連線：
```
伺服器名稱：192.168.1.100,1433
驗證：SQL Server 驗證
登入：your_username
密碼：your_password
```

---

## 連線測試

### 方法 1: 在程式中測試
在 `Form1_Load` 事件中加入：
```csharp
private void Form1_Load(object sender, EventArgs e)
{
    // 測試資料庫連線
    if (DatabaseHelper.TestConnection())
    {
        // 初始化資料庫表格
        DatabaseHelper.InitializeDatabase();
    }
    else
    {
        MessageBox.Show("無法連線到資料庫，請檢查連線設定", "警告", 
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}
```

### 方法 2: 使用 SSMS 測試
1. 開啟 SQL Server Management Studio
2. 使用 App.config 中的連線資訊連線
3. 成功連線即表示設定正確

### 方法 3: 使用 PowerShell 測試
```powershell
# 測試本機連線
Test-NetConnection -ComputerName localhost -Port 1433

# 測試遠端連線
Test-NetConnection -ComputerName 192.168.1.100 -Port 1433
```

---

## 常見問題

### ? 錯誤: "無法開啟登入所要求的資料庫"
**解決方法：**
1. 確認資料庫 `OnlineOrderingDB` 是否已建立
2. 在 SSMS 中執行：
   ```sql
   CREATE DATABASE OnlineOrderingDB;
   ```

---

### ? 錯誤: "使用者 XXX 登入失敗"
**解決方法：**
1. 檢查帳號密碼是否正確
2. 確認 SQL Server 驗證模式已啟用：
   - SSMS → 伺服器屬性 → 安全性
   - 選擇 **SQL Server 和 Windows 驗證模式**
   - 重新啟動 SQL Server 服務

---

### ? 錯誤: "在與 SQL Server 建立連線時發生網路相關或執行個體特定的錯誤"
**解決方法：**
1. 檢查 SQL Server 服務是否啟動
2. 確認 TCP/IP 通訊協定已啟用
3. 檢查防火牆規則
4. 確認伺服器名稱正確

---

### ? 錯誤: "Timeout expired"
**解決方法：**
在連線字串中增加 Timeout 設定：
```xml
connectionString="Data Source=YOUR_SERVER;Initial Catalog=OnlineOrderingDB;
                 User Id=YOUR_USER;Password=YOUR_PASSWORD;
                 Connect Timeout=30;Integrated Security=False"
```

---

### ? LocalDB 檔案鎖定問題
**解決方法：**
1. 關閉 Visual Studio
2. 刪除 `bin\Debug` 資料夾中的 `.mdf` 和 `.ldf` 檔案
3. 重新啟動應用程式

---

## ?? 連線字串參數說明

| 參數 | 說明 | 範例 |
|------|------|------|
| Data Source | 伺服器名稱或 IP | `localhost`, `.\SQLEXPRESS`, `192.168.1.100,1433` |
| Initial Catalog | 資料庫名稱 | `OnlineOrderingDB` |
| User Id | SQL Server 登入帳號 | `sa`, `admin` |
| Password | SQL Server 登入密碼 | `YourPassword123` |
| Integrated Security | 使用 Windows 驗證 | `True` 或 `False` |
| Connect Timeout | 連線逾時秒數 | `30` |
| MultipleActiveResultSets | 啟用 MARS | `True` |
| Encrypt | 加密連線 | `True` 或 `False` |
| TrustServerCertificate | 信任伺服器憑證 | `True` 或 `False` |

---

## ?? 安全性建議

1. **不要將密碼寫在 App.config 中**
   - 使用 Windows 驗證
   - 或使用加密的連線字串

2. **使用強密碼**
   - 至少 8 個字元
   - 包含大小寫、數字和特殊符號

3. **限制資料庫權限**
   - 不要使用 `sa` 帳號
   - 建立專用的應用程式帳號
   - 只授予必要的權限

4. **啟用 SSL 加密**
   ```xml
   Encrypt=True;TrustServerCertificate=False
   ```

---

## ?? 相關資源

- [SQL Server 連線字串文件](https://docs.microsoft.com/zh-tw/dotnet/api/system.data.sqlclient.sqlconnection.connectionstring)
- [ADO.NET 官方文件](https://docs.microsoft.com/zh-tw/dotnet/framework/data/adonet/)
- [SQL Server Configuration Manager 使用指南](https://docs.microsoft.com/zh-tw/sql/relational-databases/sql-server-configuration-manager)

---

## ?? 快速設定範例

### 本機開發（推薦）
```xml
<add name="OnlineOrderingDB" 
     connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=OnlineOrderingDB;Integrated Security=True" 
     providerName="System.Data.SqlClient" />
```

### 內部網路伺服器
```xml
<add name="OnlineOrderingDB" 
     connectionString="Data Source=192.168.1.100;Initial Catalog=OnlineOrderingDB;Integrated Security=True" 
     providerName="System.Data.SqlClient" />
```

### 雲端資料庫（Azure SQL）
```xml
<add name="OnlineOrderingDB" 
     connectionString="Server=tcp:yourserver.database.windows.net,1433;Initial Catalog=OnlineOrderingDB;User ID=yourusername@yourserver;Password=yourpassword;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;" 
     providerName="System.Data.SqlClient" />
```

---

**需要協助？** 請參考專案中的 `README_ADONET.md` 檔案或聯繫開發團隊。
