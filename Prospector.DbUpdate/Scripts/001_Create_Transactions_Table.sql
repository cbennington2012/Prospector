Create Table Transactions
(
	Id NVarChar(50),
	TransactionType NVarChar(5),
	Code NVarChar(50),
	Date DateTime,
	Shares int,
	Price Decimal(10, 2),
	Tax Decimal(10, 2),
	Commission Decimal(10, 2),
	Levy Decimal(10, 2),
	Percentage Decimal(10, 2),
	SellTransactionId NVarChar(50) Null
)