-- ============================================
-- 線上訂購系統資料庫初始化腳本
-- ============================================
use OnlineOrderingSystem
-- 建立 Users 資料表
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) UNIQUE NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100),
	role NVARCHAR(100),
    CreatedDate DATETIME DEFAULT GETDATE()
);

-- 建立 Products 資料表
CREATE TABLE Products (
    ProductId INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(100) NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    Stock INT DEFAULT 0,
    Description NVARCHAR(500),
    CreatedDate DATETIME DEFAULT GETDATE()
);

-- 建立 Orders 資料表
CREATE TABLE Orders (
    OrderId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    ProductName NVARCHAR(100) NOT NULL,
    Quantity INT NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    TotalAmount DECIMAL(10,2) NOT NULL,
    OrderDate DATETIME DEFAULT GETDATE(),
    Status NVARCHAR(20) DEFAULT N'待處理',
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

-- 插入測試使用者資料
INSERT INTO Users (Username, Password, Email,role) VALUES 
(N'admin', N'admin123', N'admin@example.com ',N'admin'),
(N'user1', N'123456', N'user1@example.com',N'member'),
(N'test', N'test123', N'test@example.com',N'member');

-- 插入測試產品資料
INSERT INTO Products (ProductName, Price, Stock, Description) VALUES 
(N'珍珠奶茶', 50.00, 100, N'經典台灣珍珠奶茶'),
(N'芒果冰沙', 65.00, 80, N'新鮮芒果製作的冰沙'),
(N'抹茶拿鐵', 60.00, 90, N'日式抹茶拿鐵'),
(N'經典美式咖啡', 45.00, 120, N'濃郁美式咖啡'),
(N'草莓奶昔', 70.00, 75, N'新鮮草莓奶昔'),
(N'8冰綠', 70.00, 75, N'新鮮8冰綠');

-- 插入測試訂單資料
INSERT INTO Orders (UserId, ProductName, Quantity, Price, TotalAmount, Status) VALUES 
(1, N'珍珠奶茶', 2, 50.00, 100.00, N'已完成'),
(1, N'芒果冰沙', 1, 65.00, 65.00, N'處理中'),
(2, N'抹茶拿鐵', 3, 60.00, 180.00, N'已完成'),
(2, N'經典美式咖啡', 2, 45.00, 90.00, N'待處理'),
(2, N'經典美式咖啡', 2, 45.00, 90.00, N'待處理');

-- 查詢測試
SELECT * FROM Users;
SELECT * FROM Products;
SELECT * FROM Orders;
SELECT COUNT(*) FROM Orders o INNER JOIN Users u ON o.UserId = u.UserId WHERE u.Username = 'admin'