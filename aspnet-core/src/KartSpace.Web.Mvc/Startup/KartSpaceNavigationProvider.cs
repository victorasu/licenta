using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using KartSpace.Authorization;
using KartSpace.Sessions.Dto;

namespace KartSpace.Web.Startup
{
    /// <summary>
    /// This class defines menus for the application.
    /// </summary>
    public class KartSpaceNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Home,
                        L("HomePage"),
                        url: "",
                        icon: "fas fa-home",
                        requiresAuthentication: true
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Tenants,
                        L("Tenants"),
                        url: "Tenants",
                        icon: "fas fa-building",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Tenants)
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Users,
                        L("Users"),
                        url: "Users",
                        icon: "fas fa-users",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Users)
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Roles,
                        L("Roles"),
                        url: "Roles",
                        icon: "fas fa-theater-masks",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Roles)
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Events,
                        L("Events"),
                        url: "Events",
                        icon: "fas fa-calendar-alt",
                        requiresAuthentication: true
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Merchandise,
                        L("Merchandise"),
                        url: "Merchandise",
                        icon: "fas fa-store",
                        requiresAuthentication: true
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Purchases,
                        L("Purchases"),
                        url: "Purchases",
                        icon: "fas fa-shopping-cart",
                        requiresAuthentication: true
                    )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, KartSpaceConsts.LocalizationSourceName);
        }
    }
}