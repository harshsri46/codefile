using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class TransactionHandler
{
    private string connectionString;

    public TransactionHandler(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public void PerformTransaction(AccountDetails accountDetails, List<CreditHistory> creditHistories, List<DebitHistory> debitHistories)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            SqlTransaction transaction = conn.BeginTransaction();

            try
            {
                // Insert into Account_Details
                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Account_Details (accountId, Account_type, Account_Number, Available_Balance) " +
                    "VALUES (@accountId, @Account_type, @Account_Number, @Available_Balance)", conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@accountId", accountDetails.AccountId);
                    cmd.Parameters.AddWithValue("@Account_type", accountDetails.AccountType);
                    cmd.Parameters.AddWithValue("@Account_Number", accountDetails.AccountNumber);
                    cmd.Parameters.AddWithValue("@Available_Balance", accountDetails.AvailableBalance);
                    cmd.ExecuteNonQuery();
                }

                // Insert into Credit_History
                foreach (var credit in creditHistories)
                {
                    using (SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Credit_History (id, accountnumber, Balance_Credit, creation_Time) " +
                        "VALUES (@id, @accountnumber, @Balance_Credit, @creation_Time)", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@id", credit.Id);
                        cmd.Parameters.AddWithValue("@accountnumber", credit.AccountNumber);
                        cmd.Parameters.AddWithValue("@Balance_Credit", credit.BalanceCredit);
                        cmd.Parameters.AddWithValue("@creation_Time", credit.CreationTime);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Insert into Debit_History
                foreach (var debit in debitHistories)
                {
                    using (SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Debit_History (id, accountnumber, Balance_Debit, creation_Time) " +
                        "VALUES (@id, @accountnumber, @Balance_Debit, @creation_Time)", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@id", debit.Id);
                        cmd.Parameters.AddWithValue("@accountnumber", debit.AccountNumber);
                        cmd.Parameters.AddWithValue("@Balance_Debit", debit.BalanceDebit);
                        cmd.Parameters.AddWithValue("@creation_Time", debit.CreationTime);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Commit the transaction
                transaction.Commit();
                Console.WriteLine("Transaction completed successfully");
            }
            catch (Exception ex)
            {
                // Roll back the transaction in case of error
                transaction.Rollback();
                Console.WriteLine("Transaction failed: " + ex.Message);
            }
        }
    }
}

public class AccountDetails
{
    public int AccountId { get; set; }
    public string AccountType { get; set; }
    public string AccountNumber { get; set; }
    public decimal AvailableBalance { get; set; }
}

public class CreditHistory
{
    public int Id { get; set; }
    public string AccountNumber { get; set; }
    public decimal BalanceCredit { get; set; }
    public DateTime CreationTime { get; set; }
}

public class DebitHistory
{
    public int Id { get; set; }
    public string AccountNumber { get; set; }
    public decimal BalanceDebit { get; set; }
    public DateTime CreationTime { get; set; }
}

// Example usage
public class Program
{
    public static void Main()
    {
        string connectionString = "YourConnectionStringHere";

        var accountDetails = new AccountDetails
        {
            AccountId = 1,
            AccountType = "Savings",
            AccountNumber = "123456789",
            AvailableBalance = 1000.0m
        };

        var creditHistories = new List<CreditHistory>
        {
            new CreditHistory { Id = 1, AccountNumber = "123456789", BalanceCredit = 200.0m, CreationTime = DateTime.Now },
            new CreditHistory { Id = 2, AccountNumber = "123456789", BalanceCredit = 150.0m, CreationTime = DateTime.Now }
        };

        var debitHistories = new List<DebitHistory>
        {
            new DebitHistory { Id = 1, AccountNumber = "123456789", BalanceDebit = 50.0m, CreationTime = DateTime.Now },
            new DebitHistory { Id = 2, AccountNumber = "123456789", BalanceDebit = 30.0m, CreationTime = DateTime.Now }
        };

        var transactionHandler = new TransactionHandler(connectionString);
        transactionHandler.PerformTransaction(accountDetails, creditHistories, debitHistories);
    }
}
