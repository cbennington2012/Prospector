Drop Procedure If Exists spGetTransactions;

Delimiter $$

Create Procedure spGetTransactions
(
	In StartDate DateTime,
	In EndDate DateTime
)
Begin

	Select
		Transactions.Id As Id,
		Transactions.TransactionType As TransactionType,
		Transactions.Code As Code,
		Transactions.Date As Date,
		Transactions.Shares as Shares,
		Transactions.Price As Price,
		Transactions.Tax As Tax,
		Transactions.Commission As Commission,
		Transactions.Levy As Levy,
		Transactions.Percentage As Percentage,
		Transactions.SellTransactionId As SellTransactionId
	From
		Transactions
	Where
		Transactions.Date Between StartDate And EndDate
	Order By
		Transactions.Date Desc;

End$$

Delimiter ;