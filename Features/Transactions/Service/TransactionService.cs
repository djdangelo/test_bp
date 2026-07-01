using System.Security.Claims;
using test_bp.Features.Accounts.Interface;
using test_bp.Features.SystemSetting.Interface;
using test_bp.Features.Transactions.DTOs;
using test_bp.Features.Transactions.Helper;
using test_bp.Features.Transactions.Interface;
using test_bp.Infrastructure.Security.Extensions;
using test_bp.Shared.Messaging;

namespace test_bp.Features.Transactions.Service
{
    public class TransactionService(
        ITransactionsRepository transactionRepository,
        ILogger<TransactionService> logger,
        ISystemSettingRepository systemSettingRepository,
        IAccountRepository accountRepository
    )
    {
        public async Task<ApiResponse<string>> CreateTransaction(
                CreateTransaction transaction, ClaimsPrincipal claimsPrincipal
            )
        {
            int userId = claimsPrincipal.GetUserId();
            int companyId = claimsPrincipal.GetAgencyId();

            logger.LogInformation("Intento de creación de Transacción por usuario ID {UserId} en compañía ID {CompanyId}", userId, companyId);

            if (userId == 0 || companyId == 0)
            {
                logger.LogWarning("Creación de cliente fallido: token inválido o claims vacíos (UserId={UserId}, CompanyId={CompanyId})", userId, companyId);
                return ApiResponse<string>.Unauthorized("Token inválido, corrupto o sin permisos.");
            }

            var numWithdrawals = await transactionRepository.GetNumWithdrawalsAsync(transaction.IdAccount, DateTime.UtcNow.Date);

            var maxWithdrawals = await systemSettingRepository.GetByKeyAsync("MAXIMUM_WITHDRAW");

            if (transaction.Type == Shared.Settings.TransactionType.Withdrawal && numWithdrawals >= int.Parse(maxWithdrawals!.Value))
            {
                logger.LogWarning("Creación de transacción fallida: se ha alcanzado el límite máximo de retiros para la cuenta ID {AccountId}", transaction.IdAccount);
                return ApiResponse<string>.BadRequest("Se ha alcanzado el límite máximo de retiros para esta cuenta.");
            }

            var account = await accountRepository.GetAccountByIdAsync(transaction.IdAccount);

            if (account == null)
            {
                logger.LogWarning("Creación de transacción fallida: la cuenta ID {AccountId} no existe", transaction.IdAccount);
                return ApiResponse<string>.BadRequest("La cuenta especificada no existe.");
            }

            var dataTransaction = TransactionHelper.toTransaction(transaction.IdAccount, transaction.Amount, transaction.Type, userId);

            await transactionRepository.CreateTransaction(dataTransaction);
            await transactionRepository.SaveAsync();

            logger.LogInformation("Transacción creada exitosamente: ID {TransactionId}, Tipo {TransactionType}, Monto {TransactionAmount}", dataTransaction.Id, dataTransaction.Type, dataTransaction.Amount);

            account.Update(TransactionHelper.CalculateNewBalance(account.CurrentBalance, transaction.Amount, transaction.Type), account.CurrentBalance, true, userId);
            
            accountRepository.UpdateAccount(account);

            await accountRepository.SaveAsync();

            return ApiResponse<string>.Ok("Transacción creada exitosamente.");
        }
    }
}
