USE [EtradeDb]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (1, N'Telefon', N'Telefon Ürünleri')
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (2, N'Camera', N'Kamera Ürünleri')
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (3, N'Laptop', N'Laptop Ürünleri')
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (4, N'Diğer Elektronik Eşyalar', N'Diğer Elektronik Eşyalar Ürünleri')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Stock], [Price], [IsApproved], [IsHome], [Image]) VALUES (1, N'Iphone 14 Pro Max', 1, 15, CAST(30000.00 AS Decimal(18, 2)), 1, 1, N'iphone.jpg')
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Stock], [Price], [IsApproved], [IsHome], [Image]) VALUES (2, N'Canon EOS 60D', 2, 10, CAST(10000.00 AS Decimal(18, 2)), 1, 1, N'canon.jpg')
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Stock], [Price], [IsApproved], [IsHome], [Image]) VALUES (3, N'MSI Katana GF76', 3, 30, CAST(35000.00 AS Decimal(18, 2)), 1, 1, N'msi.jpg')
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Stock], [Price], [IsApproved], [IsHome], [Image]) VALUES (4, N'Nikon D3100', 2, 20, CAST(15000.00 AS Decimal(18, 2)), 1, 0, N'nikon.jpg')
SET IDENTITY_INSERT [dbo].[Products] OFF
GO

