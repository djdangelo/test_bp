using System.Security.Claims;
using test_bp.Features.Accounts.DTOs;
using test_bp.Features.Accounts.Helper;
using test_bp.Features.Accounts.Interface;
using test_bp.Features.SystemSetting.Interface;
using test_bp.Infrastructure.Security.Extensions;
using test_bp.Shared.Messaging;

namespace test_bp.Features.Accounts.Service
{
    public class AccountService(
        IAccountRepository accountRepository, 
        ISystemSettingRepository systemSettingRepository)
    {
        public async Task<ApiResponse<AccountsData>> CreateAccount(
            AccountsData account, 
            ClaimsPrincipal claimsPrincipal)
        {
            int userId = claimsPrincipal.GetUserId();
            int companyId = claimsPrincipal.GetAgencyId();

            if (userId == 0 || companyId == 0)
            {
                return ApiResponse<AccountsData>.Unauthorized("Token inválido, corrupto o sin permisos.");
            }

            var minimunBalanceSetting = await systemSettingRepository.GetByKeyAsync("MINIMUN_BALANCE");

            if (minimunBalanceSetting == null || !double.TryParse(minimunBalanceSetting.Value, out double minimunBalance))
            {
                return ApiResponse<AccountsData>.BadRequest("No se pudo obtener la configuración de saldo mínimo.");
            }

            if (!AccountHelper.ValidateMinimunBalance(minimunBalance, account.CurrentBalance))
            {
                return ApiResponse<AccountsData>.BadRequest($"El saldo actual debe ser mayor o igual a {minimunBalance}.");
            }

            var accountDomain = account.ToDomain();

            await accountRepository.CreateAccount(accountDomain);
            await accountRepository.SaveAsync();

            return ApiResponse<AccountsData>.Ok(accountDomain.ToAccountsData());
        }
    }
}
