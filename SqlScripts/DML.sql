USE DBSALES

--User Role data
INSERT INTO User_Role(RoleName) VALUES
('Administrador'),
('Empleado'),
('Supervisor');

SELECT * FROM User_Role


--App User data
INSERT INTO App_User(CompleteName, Email, RoleId, Pass) VALUES
('Administrador', 'admin@mail.com', 1, '1234');

SELECT * FROM App_User


--Category data
INSERT INTO Category(CategoryName) VALUES
('Portátiles'),
('Pantallas'),
('Teclados'),
('Auriculares'),
('Almacenamiento'),
('Accesorios');

SELECT * FROM Category


--Product data
INSERT INTO Product(ProductName, CategoryId, Stock, Price) VALUES
('Samsung Book Pro', 1, 20, 2500),
('Lenovo IdeaPad', 1, 30, 2200),
('Asus Zenbook Duo', 1, 30, 3100),
('Teros Gaming te-2', 2, 25, 1050),
('Samsung Curve', 2, 15, 1400),
('Huawei Gamer', 2, 10, 1350),
('Seisen Gamer', 3, 10, 800),
('Antryx Gamer', 3, 10, 1000),
('Logitech', 3, 10, 1000),
('Logitech Gamer', 4, 15, 800),
('HyperX Gamer', 4, 20, 680),
('RedDragon RGB', 4, 25, 950),
('Kingston RGB 16GB', 5, 10, 200),
('Kingston RGB 32GB', 5, 20, 350),
('Kingston RGB 64GB', 5, 15, 600),
('Cooler Master', 6, 20, 200),
('Cooler Lenovo Mini', 6, 15, 200),
('Mouse HP', 6, 25, 75);

SELECT * FROM Product


--Menu data
INSERT INTO Menu(MenuName, Icon, MenuUrl) VALUES
('Dashboard', 'dashboard', '/pages/dashboard'),
('Usuario', 'group', '/pages/usuario'),
('Producto', 'collections_bookmark', '/pages/producto'),
('Venta', 'currency_exchange', '/pages/venta'),
('Historial Venta', 'edit_note', '/pages/historial_venta'),
('Reporte', 'receipt', '/pages/reporte');

SELECT * FROM Menu


--Menu Role Admin data
INSERT INTO RoleMenu(MenuId, RoleId) VALUES
(1, 1),
(2, 1),
(3, 1),
(4, 1),
(5, 1),
(6, 1);

--Menu Role Employee data
INSERT INTO RoleMenu(MenuId, RoleId) VALUES
(4, 2),
(5, 2);

--Menu Role Supervisor data
INSERT INTO RoleMenu(MenuId, RoleId) VALUES
(3, 3),
(4, 3),
(5, 3),
(6, 3);

SELECT * FROM RoleMenu


--Document Number data
INSERT INTO DocumentNumber(Last_Number) VALUES
(0);

SELECT * FROM DocumentNumber