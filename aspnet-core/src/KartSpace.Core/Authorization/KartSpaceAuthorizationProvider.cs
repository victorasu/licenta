﻿using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace KartSpace.Authorization
{
    public class KartSpaceAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_Events_Management, L("EventsManagement"));
            context.CreatePermission(PermissionNames.Pages_Merch_Management, L("MerchManagement"));
            context.CreatePermission(PermissionNames.Pages_Purchases_Management, L("PurchasesManagement"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, KartSpaceConsts.LocalizationSourceName);
        }
    }
}
