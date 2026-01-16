USE [CustomerOrderViewer]
GO

/****** Object:  View [dbo].[CustomerOrderDetail]    Script Date: 16/01/2026 13:36:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[CustomerOrderDetail] AS
	SELECT
		t1.CustomerOrderId,
		T2.CustomerId,
		t3.ItemId,
		t2.FirstName,
		t2.LastName,
		t3.[Description],
		t3.Price
	FROM
		DBO.CustomerOrder t1
	INNER JOIN
		dbo.Customer t2 ON t2.CustomerId = t1.CustomerId
	INNER JOIN
		dbo.Item t3 ON t3.ItemId = t1.ItemId
GO


