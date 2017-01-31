CREATE TABLE [dbo].[OrdersProducts] (
    [OrderId]   INT NOT NULL,
    [ProductId] INT NOT NULL,
    CONSTRAINT [PK_OrdersProducts] PRIMARY KEY CLUSTERED ([OrderId] ASC, [ProductId] ASC),
    CONSTRAINT [FK_OrdersProducts_ToOrders] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([Id]),
    CONSTRAINT [FK_OrdersProducts_ToProducts] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id])
);

