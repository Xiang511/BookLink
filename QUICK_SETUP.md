# 快速設定：連接到 Microsoft SQL Server

## ?? 5 分鐘快速設定

### 步驟 1??: 確認您的 SQL Server 資訊
找出以下資訊：
- 伺服器名稱/IP
- 資料庫名稱：`OnlineOrderingDB`
- 驗證方式：Windows 驗證 或 SQL Server 驗證

---

### 步驟 2??: 修改 App.config

開啟 `App.config` 檔案，找到 `<connectionStrings>` 區段。

#### ?? 情境 A：本機 SQL Server Express（最常見）
```xml
<add name="OnlineOrderingDB" 
     connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=OnlineOrderingDB;Integrated Security=True" 
     providerName="System.Data.SqlClient" />
```

#### ?? 情境 B：本機 SQL Server（完整版）
```xml
<add name="OnlineOrderingDB" 
     connectionString="Data Source=localhost;Initial Catalog=OnlineOrderingDB;Integrated Security=True" 
     providerName="System.Data.SqlClient" />
```

#### ?? 情境 C：遠端 SQL Server（使用 SQL 驗證）
```xml
<add name="OnlineOrderingDB" 
     connectionString="Data Source=192.168.1.100;Initial Catalog=OnlineOrderingDB;User Id=sa;Password=yourpassword;Integrated Security=False" 
     providerName="System.Data.SqlClient" />
```

> ?? 記得替換 `yourpassword` 為實際密碼

---

### 步驟 3??: 建立資料庫

#### 方法 A：在 SQL Server Management Studio (SSMS) 中執行
```sql
CREATE DATABASE OnlineOrderingDB;
```

#### 方法 B：讓程式自動建立
程式啟動時會自動建立所需的資料表。

---

### 步驟 4??: 執行初始化腳本（可選）

如果想要測試資料，在 SSMS 中執行 `DatabaseScript.sql`：
1. 開啟 SSMS
2. 連接到您的 SQL Server
3. 開啟 `DatabaseScript.sql`
4. 點擊「執行」

這將建立測試帳號：
- 帳號：`admin` / 密碼：`admin123`
- 帳號：`user1` / 密碼：`123456`

---

### 步驟 5??: 測試連線

1. 啟動應用程式
2. 如果出現連線錯誤提示，點擊「是」測試連線
3. 或點擊登入頁面的連線資訊（如果有的話）

---

## ?? 常見設定範例

### 本機電腦的 SQL Server
```xml
<!-- 方式 1: 使用電腦名稱 -->
<add name="OnlineOrderingDB" 
     connectionString="Data Source=DESKTOP-ABC123\SQLEXPRESS;Initial Catalog=OnlineOrderingDB;Integrated Security=True" 
     providerName="System.Data.SqlClient" />

<!-- 方式 2: 使用 localhost -->
<add name="OnlineOrderingDB" 
     connectionString="Data Source=localhost\SQLEXPRESS;Initial Catalog=OnlineOrderingDB;Integrated Security=True" 
     providerName="System.Data.SqlClient" />

<!-- 方式 3: 使用點號 -->
<add name="OnlineOrderingDB" 
     connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=OnlineOrderingDB;Integrated Security=True" 
     providerName="System.Data.SqlClient" />
```

### 區域網路的 SQL Server
```xml
<add name="OnlineOrderingDB" 
     connectionString="Data Source=192.168.1.100;Initial Catalog=OnlineOrderingDB;Integrated Security=True" 
     providerName="System.Data.SqlClient" />
```

---

## ? 如何找到我的伺服器名稱？

### 方法 1: 在 SSMS 中查看
1. 開啟 SQL Server Management Studio
2. 連線視窗中的「伺服器名稱」下拉選單會顯示可用的伺服器

### 方法 2: 使用 PowerShell
```powershell
Get-Service | Where-Object {$_.Name -like "*SQL*" -and $_.Status -eq "Running"}
```

### 方法 3: 使用 SQL Server Configuration Manager
1. 開啟 SQL Server Configuration Manager
2. 查看 SQL Server 服務的名稱

---

## ?? 遇到問題？

### 問題 1: "無法連線到伺服器"
**檢查清單：**
- [ ] SQL Server 服務是否已啟動？
- [ ] 伺服器名稱是否正確？
- [ ] 網路是否可達？（ping 測試）
- [ ] 防火牆是否阻擋 Port 1433？

### 問題 2: "登入失敗"
**檢查清單：**
- [ ] 使用 Windows 驗證時，您的 Windows 帳號是否有權限？
- [ ] 使用 SQL 驗證時，帳號密碼是否正確？
- [ ] SQL Server 是否啟用混合驗證模式？

### 問題 3: "找不到資料庫"
**解決方法：**
```sql
-- 在 SSMS 中執行
CREATE DATABASE OnlineOrderingDB;
```

---

## ?? 需要更多協助？

查閱完整文件：`SQLSERVER_CONNECTION_GUIDE.md`

---

## ? 設定完成檢查表

- [ ] 已修改 App.config 中的連線字串
- [ ] 已建立 OnlineOrderingDB 資料庫
- [ ] 已測試連線成功
- [ ] 可以正常啟動應用程式
- [ ] 可以註冊新使用者
- [ ] 可以成功登入

完成以上步驟後，您就可以開始使用系統了！??
