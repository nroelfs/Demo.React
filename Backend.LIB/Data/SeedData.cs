namespace Backend.LIB.Data;

public static class SeedData
{
    public static async void Initialize(IServiceProvider serviceProvider)
    {

        var roleManager = serviceProvider.GetRequiredService<RoleManager<BaseRole>>();
        string[] claimTypes = new string[] {  "User.Me" };

        string devRoleName = "Dev";
        var devRole = await roleManager.FindByNameAsync(devRoleName);
        if (devRole == null)
        {
            devRole = new BaseRole { Name = devRoleName };
            await roleManager.CreateAsync(devRole);

            foreach (var claimType in claimTypes)
            {
                await roleManager.AddClaimAsync(devRole, new System.Security.Claims.Claim(claimType, "true"));
            }
        }

        string userRoleName = "User";
        var userRole = await roleManager.FindByNameAsync(userRoleName);
        if (userRole == null)
        {
            userRole = new BaseRole { Name = userRoleName };
            await roleManager.CreateAsync(userRole);
            await roleManager.AddClaimAsync(userRole, new System.Security.Claims.Claim("User.Me", "true"));
        }
    }
}
