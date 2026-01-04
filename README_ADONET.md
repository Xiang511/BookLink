# Online Ordering System - ADO.NET 實作說明

## 專案概述
這是一個使用 **ADO.NET** 技術開發的線上訂購系統，使用 .NET Framework 4.7.2 和 SQL Server LocalDB。

## ADO.NET 實作位置

### 1. DatabaseHelper.cs
核心資料庫操作類別，包含所有 ADO.NET 相關功能：

#### 主要功能：
- **GetConnection()** - 建立並返回 SqlConnection 連線物件
- **ValidateUser()** - 使用 SqlCommand 驗證使用者登入
- **RegisterUser()** - 使用參數化查詢新增使用者（防止 SQL Injection）
- **GetProducts()** - 使用 SqlDataAdapter 填充 DataTable
- **GetUserOrders()** - 使用 JOIN 查詢取得使用者訂單
- **AddOrder()** - 使用交易式操作新增訂單
- **InitializeDatabase()** - 自動建立資料表結構

#### ADO.NET 核心物件使用：
```csharp
// 1. SqlConnection - 資料庫連線
using (SqlConnection conn = GetConnection())
{
    conn.Open();
    // 執行操作
}

// 2. SqlCommand - 執行 SQL 命令
using (SqlCommand cmd = new SqlCommand(query, conn))
{
    cmd.Parameters.AddWithValue("@Username", username);
    cmd.ExecuteNonQuery(); // INSERT, UPDATE, DELETE
    cmd.ExecuteScalar();   // SELECT COUNT(*)
}

// 3. SqlDataAdapter - 填充 DataTable
using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
{
    adapter.Fill(dataTable);
}
```

### 2. Form1.cs (登入表單)
- **Form1_Load** - 啟動時初始化資料庫
- **ValidateLogin()** - 呼叫 DatabaseHelper.ValidateUser() 驗證登入
- 使用 ADO.NET 進行即時資料庫驗證

### 3. RegisterForm.cs (註冊表單)
- **btnRegister_Click** - 註冊新使用者
- 使用 DatabaseHelper.UserExists() 檢查使用者是否存在
- 使用 DatabaseHelper.RegisterUser() 新增使用者資料

## 資料庫結構

### Users (使用者資料表)
| 欄位 | 型別 | 說明 |
|------|------|------|
| UserId | INT (PK) | 使用者 ID |
| Username | NVARCHAR(50) | 使用者名稱 (唯一) |
| Password | NVARCHAR(255) | 密碼 |
| Email | NVARCHAR(100) | 電子郵件 |
| CreatedDate | DATETIME | 建立日期 |

### Products (產品資料表)
| 欄位 | 型別 | 說明 |
|------|------|------|
| ProductId | INT (PK) | 產品 ID |
| ProductName | NVARCHAR(100) | 產品名稱 |
| Price | DECIMAL(10,2) | 價格 |
| Stock | INT | 庫存 |
| Description | NVARCHAR(500) | 說明 |
| CreatedDate | DATETIME | 建立日期 |

### Orders (訂單資料表)
| 欄位 | 型別 | 說明 |
|------|------|------|
| OrderId | INT (PK) | 訂單 ID |
| UserId | INT (FK) | 使用者 ID |
| ProductName | NVARCHAR(100) | 產品名稱 |
| Quantity | INT | 數量 |
| Price | DECIMAL(10,2) | 單價 |
| TotalAmount | DECIMAL(10,2) | 總金額 |
| OrderDate | DATETIME | 訂單日期 |
| Status | NVARCHAR(20) | 狀態 |

## 測試帳號
執行 `DatabaseScript.sql` 後可使用以下測試帳號：

| 帳號 | 密碼 |
|------|------|
| admin | admin123 |
| user1 | 123456 |
| test | test123 |

## 使用說明

### 第一次執行
1. 啟動程式時會自動建立資料庫檔案 `OnlineOrderingDB.mdf`
2. 資料庫表格會自動建立（透過 InitializeDatabase()）
3. 建議執行 `DatabaseScript.sql` 插入測試資料

### 註冊新帳號
1. 點擊登入頁面底部的「Don't have an account ? Register」按鈕
2. 填寫用戶名、密碼、確認密碼和電子郵件
3. 點擊「註冊」按鈕
4. 註冊成功後使用新帳號登入

### 登入系統
1. 輸入用戶名和密碼
2. 點擊「Login」按鈕
3. 登入成功後進入 Form2

## ADO.NET 最佳實踐

### 1. 使用參數化查詢防止 SQL Injection
```csharp
cmd.Parameters.AddWithValue("@Username", username);
```

### 2. 使用 using 語句自動釋放資源
```csharp
using (SqlConnection conn = GetConnection())
{
    // 自動呼叫 Dispose()
}
```

### 3. 異常處理
所有資料庫操作都包含 try-catch 區塊，捕捉並顯示錯誤訊息。

### 4. 連線字串管理
目前使用 LocalDB，連線字串定義在 DatabaseHelper.cs 中。

## 檔案結構
```
Online Ordering System/
├── DatabaseHelper.cs          (ADO.NET 核心類別)
├── Form1.cs                   (登入表單)
├── Form2.cs                   (主系統表單)
├── RegisterForm.cs            (註冊表單)
├── DatabaseScript.sql         (資料庫初始化腳本)
├── App.config                 (應用程式設定)
└── README_ADONET.md          (本文件)
```

## 技術規格
- **.NET Framework**: 4.7.2
- **資料庫**: SQL Server LocalDB
- **資料存取技術**: ADO.NET
- **UI Framework**: Windows Forms + MaterialSkin

## 未來擴展建議
1. 在 Form2 中實作產品列表顯示（使用 DataGridView + SqlDataAdapter）
2. 實作訂單管理功能（使用 SqlCommand 進行 CRUD 操作）
3. 實作購物車功能
4. 訂單歷史記錄查詢



## 開發者
Online Ordering System Development Team
