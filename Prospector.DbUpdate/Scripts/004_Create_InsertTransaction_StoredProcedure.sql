Drop Procedure If Exists spInsertTransaction;

Delimiter $$

Create Procedure spInsertTransaction
(
	In Id NVarChar(50),
	In TransactionType NVarChar(5),
	In Code NVarChar(50),
	In Date DateTime,
	In Shares int,
	In Price Decimal(10, 2),
	In Tax Decimal(10, 2),
	In Commission Decimal(10, 2),
	In Levy Decimal(10, 2),
	In Percentage Decimal(10, 2),
	In SellTransactionId NVarChar(50)
)
Begin

	Insert Into Transactions (
		Id,
		TransactionType,
		Code,
		Date,
		Shares,
		Price,
		Tax,
		Commission,
		Levy,
		Percentage,
		SellTransactionId
	)
	Values (
		Id,
		TransactionType,
		Code,
		Date,
		Shares,
		Price,
		Tax,
		Commission,
		Levy,
		Percentage,
		SellTransactionId
	);

End$$

Delimiter ;